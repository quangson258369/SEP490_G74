using Azure;
using DataAccess.Entity;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Dao.ServiceDao;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly HttpClient client = null;
        private string MedicalRecordAPI = "";

        public MedicalRecordController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        // really done
        public async Task<IActionResult> Index(int page)
        {
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/ListMedicalRecordPaging?page="+page;
            HttpResponseMessage response = await client.GetAsync(MedicalRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<MedicalRecordDaoOutputDto> listProducts = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strData, option);

            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/GetCountOfListMR");
            strData = await response.Content.ReadAsStringAsync();
            int countListService = System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);

            ViewBag.CurrentPage = page;
            ViewBag.TotalItemCount = countListService;
            return View(listProducts);
        }

        public async Task<IActionResult> Add(int pid)
        {
            // call list service type
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Service/ListServiceType");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<ServiceType> serviceTypes = System.Text.Json.JsonSerializer.Deserialize<List<ServiceType>>(strData, options);

            // call list medical record
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListMedicalRecord");
            string strMR = await response.Content.ReadAsStringAsync();
            List<MedicalRecordDaoOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strMR, options);

            // call get patiet information
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + pid);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);

            MedicalRecordDaoOutputDto mr = listMRs.Last();
            int examCode = 1;
            if (mr != null && listMRs.Count > 0) examCode = mr.MedicalRecordId + 1;
            if (listMRs.Count == 0) examCode = 1;


            ViewBag.Today = DateTime.Now.ToShortDateString();
            ViewBag.MrId = examCode;
            ViewBag.Patient = p;
            return View(serviceTypes);
        }

        // depend on authentication
        public async Task<IActionResult> AddMedicalRecord()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + Request.Form["pid"]);
            string str = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);
            if (p.ResultCd == 0)
            {
                return RedirectToAction("MedicalRecord/Add?ServiceType=");
            }
            else
            {
                MedicalRecordDao dao = new MedicalRecordDao();

                int mrid = int.Parse(Request.Form["mrid"]);

                // lay list service da chon
                var model = JsonConvert.DeserializeObject<List<MyViewModel>>(Request.Form["jsonData"]);
                List<ServiceMedicalRecord> list = new List<ServiceMedicalRecord>();
                string servicechoose = "";
                foreach (var item in model)
                {
                    servicechoose += item.Name + ",";
                    list.Add(new ServiceMedicalRecord() { ServiceId = item.Sid, MedicalRecordId = mrid, DoctorId = int.Parse(item.doctorId) });
                }
                // edit benh nhan truoc
                PatientModify patient = new PatientModify()
                {
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    ServiceDetailName = servicechoose.Substring(0, servicechoose == null ? 0 : servicechoose.Length - 1),
                    Height = byte.Parse((Request.Form["height"].ToString() == null || Request.Form["height"].ToString().Length == 0) ? "0" : Request.Form["height"].ToString()),
                    Weight = byte.Parse((Request.Form["weight"].ToString() == null || Request.Form["weight"].ToString().Length == 0) ? "0" : Request.Form["weight"].ToString()),
                    BloodPressure = byte.Parse((Request.Form["bloodpress"].ToString() == null || Request.Form["bloodpress"].ToString().Length == 0) ? "0" : Request.Form["bloodpress"].ToString()),
                    BloodGroup = Request.Form["bloodgr"],
                    Contact = new ContactPatientDTO
                    {
                        CId = p.Contacts.CId,
                        Address = Request.Form["address"].ToString(),
                        Dob = DateTime.ParseExact(Request.Form["dob"].ToString(), "yyyy-MM-dd", null),
                        Gender = Request.Form["gender"] == "male" ? true : false,
                        Name = Request.Form["fullname"].ToString(),
                        PatientId = int.Parse(Request.Form["pid"].ToString()),
                        Phone = Request.Form["phone"].ToString()
                    }
                };


                // edit patient
                response = await client.PutAsJsonAsync("https://localhost:7249/api/Patient/UpdatePatient", patient);
                string strPatient = await response.Content.ReadAsStringAsync();
                string rowEffected = System.Text.Json.JsonSerializer.Deserialize<string>(strPatient, options);


                // them medical record
                string AddAPI = "https://localhost:7249/api/MedicalRecord/AddMedicalRecord";
                var addMR = new MedicalRecordModify()
                {
                    ExamCode = "1",
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    MedicalRecordDate = DateTime.Now,
                    ExamReason = Request.Form["reason"],
                    MedicalRecordId = mrid,
                    DoctorId = 1
                };
                response = await client.PostAsJsonAsync(AddAPI, addMR);
                string strData = await response.Content.ReadAsStringAsync();
                string row = System.Text.Json.JsonSerializer.Deserialize<string>(strData, options);

                // them dich vu su dung vao db

                foreach (var sm in list)
                {
                    dao.AddServiceMR(sm);
                }
                return RedirectToAction("Index", "MedicalRecord");
            }
        }

        // really done
        public async Task<IActionResult> MedicalRecordDetail(int id)
        {
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/GetMedicalRecord/" + id;
            HttpResponseMessage response = await client.GetAsync(MedicalRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            MedicalRecordDaoOutputDto medicalRecordDetail = System.Text.Json.JsonSerializer.Deserialize<MedicalRecordDaoOutputDto>(strData, option);

            // thieu get list danh sach dich vu kham
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListServiceUses/" + medicalRecordDetail.MedicalRecordId);
            string strService = await response.Content.ReadAsStringAsync();
            List<ServiceMRDTO> service = System.Text.Json.JsonSerializer.Deserialize<List<ServiceMRDTO>>(strService, option);

            // thong tin patient
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + medicalRecordDetail.PatientId);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, option);
            if (service == null) service = new List<ServiceMRDTO>();
            ViewBag.Service = service;
            ViewBag.Patient = p;

            return View(medicalRecordDetail);
        }



        public async Task<IActionResult> Edit(int id)
        {
            // call list service type
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Service/ListServiceType");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<ServiceType> serviceTypes = System.Text.Json.JsonSerializer.Deserialize<List<ServiceType>>(strData, options);

            // call medical record by id
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/GetMedicalRecord/" + id;
            response = await client.GetAsync(MedicalRecordAPI);
            string strMR = await response.Content.ReadAsStringAsync();
            MedicalRecordDaoOutputDto mredit = System.Text.Json.JsonSerializer.Deserialize<MedicalRecordDaoOutputDto>(strMR, options);

            // thieu get list danh sach dich vu kham
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListServiceUses/" + id);
            string strService = await response.Content.ReadAsStringAsync();
            List<ServiceMRDTO> service = System.Text.Json.JsonSerializer.Deserialize<List<ServiceMRDTO>>(strService, options);

            // call get patiet information
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + mredit.PatientId);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);

            //----------------------------------------------------------------------------------------------------------------------
            // lay list service da chon
            //var model = JsonConvert.DeserializeObject<List<MyViewModel>>(Request.Form["jsond"]);

            //ViewBag.selectedService = service;
            ViewBag.selectedService = JsonConvert.SerializeObject(service);
            ViewBag.MR = mredit;
            ViewBag.Patient = p;
            return View(serviceTypes);
        }

        [HttpPost]
        public async Task<IActionResult> EditMedicalRecord()
        {
            MedicalRecordDao dao = new MedicalRecordDao();

            int mrid = int.Parse(Request.Form["mrid"]);

            // lay list service da chon
            var model = JsonConvert.DeserializeObject<List<MyViewModel>>(Request.Form["jsonD"]);
            List<ServiceMedicalRecord> list = new List<ServiceMedicalRecord>();
            string servicechoose = "";
            foreach (var item in model)
            {
                servicechoose += item.Name + ",";
                list.Add(new ServiceMedicalRecord() { ServiceId = item.Sid, MedicalRecordId = mrid, DoctorId = int.Parse(item.doctorId) });
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // call get patiet information
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + int.Parse(Request.Form["pid"].ToString()));
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);

            if (p != null)
            {
                // edit benh nhan truoc
                PatientModify patient = new PatientModify()
                {
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    ServiceDetailName = "chuan bi bo",
                    Height = byte.Parse((Request.Form["height"].ToString() == null || Request.Form["height"].ToString().Length == 0) ? "0": Request.Form["height"].ToString()),
                    Weight = byte.Parse((Request.Form["weight"].ToString() == null || Request.Form["weight"].ToString().Length == 0) ? "0" : Request.Form["weight"].ToString()),
                    BloodPressure = byte.Parse((Request.Form["bloodpressure"].ToString() == null || Request.Form["bloodpressure"].ToString().Length == 0) ? "0" : Request.Form["bloodpressure"].ToString()),
                    BloodGroup = Request.Form["bloodgr"],
                    Contact = new ContactPatientDTO
                    {
                        CId = p.Contacts.CId,
                        Address = Request.Form["address"].ToString(),
                        Dob = DateTime.ParseExact(Request.Form["dob"].ToString(), "yyyy-MM-dd", null),
                        Gender = Request.Form["gender"] == "male" ? true : false,
                        Name = Request.Form["fullname"].ToString(),
                        PatientId = int.Parse(Request.Form["pid"].ToString()),
                        Phone = Request.Form["phone"].ToString()
                    }
                };

                // edit patient
                response = await client.PutAsJsonAsync("https://localhost:7249/api/Patient/UpdatePatient", patient);
                string strPatient = await response.Content.ReadAsStringAsync();
                string rowEffected = System.Text.Json.JsonSerializer.Deserialize<string>(strPatient, options);
            }

            // edit medical record
            string AddAPI = "https://localhost:7249/api/MedicalRecord/UpdateMedicalRecord";
            var addMR = new MedicalRecordModify()
            {
                ExamCode = "1",
                PatientId = int.Parse(Request.Form["pid"].ToString()),
                MedicalRecordDate = DateTime.Now,
                ExamReason = Request.Form["reason"],
                MedicalRecordId = mrid,
                DoctorId = 1
            };
            response = await client.PutAsJsonAsync(AddAPI, addMR);
            string strData = await response.Content.ReadAsStringAsync();
            string row = System.Text.Json.JsonSerializer.Deserialize<string>(strData, options);
            // them dich vu su dung vao db

            foreach (var sm in list)
            {
                dao.AddServiceMR(sm);
            }

            return RedirectToAction("Index", "MedicalRecord");
        }

        public async Task<IActionResult> Delete(int id)
        {
            // call list service type
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:7249/api/MedicalRecord/DeleteMedicalRecord?id=" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string row = System.Text.Json.JsonSerializer.Deserialize<string>(strData, options);
            return RedirectToAction("Index", "MedicalRecord");
        }


    }

    internal class MyViewModel
    {
        public int Sid { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }

        public string doctorId { get; set; }
    }
}
