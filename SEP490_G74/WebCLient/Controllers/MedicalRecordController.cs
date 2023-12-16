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
        public async Task<IActionResult> Index(int page = 1)
        {
            var data = await GetMedicalRecordsAsync(page);
            ViewBag.CurrentPage = page;
            ViewBag.TotalItemCount = await GetTotalMedicalRecordCountAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string term, int page = 1)
        {
            var result = await SearchAndPagingAsync(term, page);
            return PartialView("_MedicalRecordTablePartial", result);
        }

        private async Task<List<MedicalRecordOutputDto>> SearchAndPagingAsync(string term, int page)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7249/api/MedicalRecord/SearchMedicalRecord?str={term}&page={page}");
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordOutputDto>>(strData, option);
        }

        private async Task<int> GetTotalMedicalRecordCountAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/GetCountOfListMR");
            var strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);
        }

        private async Task<List<MedicalRecordOutputDto>> GetMedicalRecordsAsync(int page)
        {
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/ListMedicalRecordPaging?page=" + page;
            HttpResponseMessage response = await client.GetAsync(MedicalRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordOutputDto>>(strData, option);
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
            List<MedicalRecordOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordOutputDto>>(strMR, options);

            // call get patiet information
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + pid);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);

            MedicalRecordOutputDto mrexam = listMRs.OrderByDescending(x => x.ExamCode).FirstOrDefault();
            MedicalRecordOutputDto mr = listMRs.Last();
            int mrid = 1;
            if (mr != null && listMRs.Count > 0) mrid = mr.MedicalRecordId + 1;
            if (listMRs.Count == 0) mrid = 1;
            string examCode = "1";
            if (mrexam != null && listMRs.Count > 0) examCode = (int.Parse(mrexam.ExamCode)+ 1).ToString();
            if (listMRs.Count == 0) examCode = "1";


            ViewBag.Today = DateTime.Now;
            ViewBag.MrId = mrid;
            ViewBag.ExamCode = examCode;
            ViewBag.Patient = p;
            return View(serviceTypes);
        }

        public async Task<IActionResult> AddReExam(string examid)
        {
            // call list service type
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Service/ListServiceType");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<ServiceType> serviceTypes = System.Text.Json.JsonSerializer.Deserialize<List<ServiceType>>(strData, options);

            // call list medical record
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListMedicalRecord");
            string strMR = await response.Content.ReadAsStringAsync();
            List<MedicalRecordOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordOutputDto>>(strMR, options);
            MedicalRecordOutputDto mr = listMRs.Last();
            int examCod = 1;
            if (mr != null && listMRs.Count > 0) examCod = mr.MedicalRecordId + 1;
            if (listMRs.Count == 0) examCod = 1;
            ViewBag.MrId = examCod;
            ViewBag.Today = DateTime.Now;
            ViewBag.ExamID = examid;
            //get ra list medical record tung kham
            List<MedicalRecordOutputDto> medicalRecords = listMRs.Where(s => s.ExamCode == examid).ToList();
            // lấy thông tin bệnh án đã khám gần nhất theo cái khung dịch vụ đấy
            MedicalRecordOutputDto medical = medicalRecords.Last();
            // Lấy thông tin bênh nhân tái khám
            int pid = medical.PatientId;
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + pid);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO patient = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);
            ViewBag.Patient = patient;
            // get list ds dich vu kham gan nhat
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListServiceUses/" + medical.MedicalRecordId);
            string strService = await response.Content.ReadAsStringAsync();
            List<ServiceMRDTO> service = System.Text.Json.JsonSerializer.Deserialize<List<ServiceMRDTO>>(strService, options);
            ViewBag.selectedService = JsonConvert.SerializeObject(service);

            return View(serviceTypes);
        }

        // depend on authentication
        public async Task<IActionResult> AddMedicalRecord()
        {
            string uid = HttpContext.Session.GetString("USERID");
            uid = (uid == null || uid.Length == 0) ? "1" : uid;
            await Console.Out.WriteLineAsync(uid.ToString());
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Member/GetDoctorId?idUser=" + uid);
            string strDoctor = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            int doctorid = System.Text.Json.JsonSerializer.Deserialize<int>(strDoctor, options);

            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + Request.Form["pid"]);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);
            if (p.ResultCd == 0)
            {
                return RedirectToAction("Add","Patient");
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
                    ServiceDetailName = (servicechoose == null || servicechoose.Length == 0)?"Chưa Chọn Dịch Vụ Khám": servicechoose.Substring(0, servicechoose == null ? 0 : servicechoose.Length - 1),
                    Height = byte.Parse((Request.Form["height"].ToString() == null || Request.Form["height"].ToString().Length == 0) ? "0" : Request.Form["height"].ToString()),
                    Weight = byte.Parse((Request.Form["weight"].ToString() == null || Request.Form["weight"].ToString().Length == 0) ? "0" : Request.Form["weight"].ToString()),
                    BloodPressure = byte.Parse((Request.Form["bloodpress"].ToString() == null || Request.Form["bloodpress"].ToString().Length == 0) ? "0" : Request.Form["bloodpress"].ToString()),
                    BloodGroup = Request.Form["bloodgr"],
                    
                };
                

                // edit patient
                response = await client.PutAsJsonAsync("https://localhost:7249/api/Patient/UpdatePatient", patient);
                string strPatient = await response.Content.ReadAsStringAsync();
                string rowEffected = System.Text.Json.JsonSerializer.Deserialize<string>(strPatient, options);

                ContactPatientDTO contact = new ContactPatientDTO
                {
                    CId = p.Contacts.CId,
                    Address = Request.Form["address"].ToString(),
                    Dob = DateTime.ParseExact(Request.Form["dob"].ToString(), "yyyy-MM-dd", null),
                    Gender = Request.Form["gender"] == "male" ? true : false,
                    Name = Request.Form["fullname"].ToString(),
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    Phone = Request.Form["phone"].ToString()
                };
                // edit contact patient
                response = await client.PutAsJsonAsync("https://localhost:7249/api/Patient/UpdateContactPatient", contact);
                string strContactPatient = await response.Content.ReadAsStringAsync();
                string rowEffectip = System.Text.Json.JsonSerializer.Deserialize<string>(strContactPatient, options);
                await Console.Out.WriteLineAsync(rowEffectip);

                // them medical record
                string AddAPI = "https://localhost:7249/api/MedicalRecord/AddMedicalRecord";
                var addMR = new MedicalRecordModify()
                {
                    ExamCode = Request.Form["examcode"].ToString(),
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    MedicalRecordDate = DateTime.Now,
                    ExamReason = Request.Form["reason"],
                    MedicalRecordId = mrid,
                    DoctorId = doctorid
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
            MedicalRecordOutputDto medicalRecordDetail = System.Text.Json.JsonSerializer.Deserialize<MedicalRecordOutputDto>(strData, option);

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
            MedicalRecordOutputDto mredit = System.Text.Json.JsonSerializer.Deserialize<MedicalRecordOutputDto>(strMR, options);

            // get list danh sach dich vu kham
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListServiceUses/" + id);
            string strService = await response.Content.ReadAsStringAsync();
            List<ServiceMRDTO> service = System.Text.Json.JsonSerializer.Deserialize<List<ServiceMRDTO>>(strService, options);

            // call get patient information
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + mredit.PatientId);
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);

            //----------------------------------------------------------------------------------------------------------------------
            
            ViewBag.selectedService = JsonConvert.SerializeObject(service);
            ViewBag.MR = mredit;
            ViewBag.Patient = p;
            return View(serviceTypes);
        }

        [HttpPost]
        public async Task<IActionResult> EditMedicalRecord()
        {
            MedicalRecordDao dao = new MedicalRecordDao();
            string uid = HttpContext.Session.GetString("USERID");
            uid = (uid == null || uid.Length == 0) ? "1" : uid;
            int mrid = int.Parse(Request.Form["mrid"]);
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Member/GetDoctorId?idUser=" + uid);
            string strDoctor = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            int doctorid = System.Text.Json.JsonSerializer.Deserialize<int>(strDoctor, options);


            // lay list service da chon
            var model = JsonConvert.DeserializeObject<List<MyViewModel>>(Request.Form["jsonD"]);
            List<ServiceMedicalRecord> list = new List<ServiceMedicalRecord>();
            string servicechoose = "";
            foreach (var item in model)
            {
                servicechoose += item.Name + ",";
                list.Add(new ServiceMedicalRecord() { ServiceId = item.Sid, MedicalRecordId = mrid, DoctorId = int.Parse(item.doctorId) });
            }
            // call get patiet information
            response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/" + int.Parse(Request.Form["pid"].ToString()));
            string str = await response.Content.ReadAsStringAsync();
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);

            if (p != null)
            {
                // edit benh nhan truoc
                PatientModify patient = new PatientModify()
                {
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    ServiceDetailName = (servicechoose == null || servicechoose.Length == 0) ? "Chưa Chọn Dịch Vụ Khám" : servicechoose.Substring(0, servicechoose == null ? 0 : servicechoose.Length - 1),
                    Height = byte.Parse((Request.Form["height"].ToString() == null || Request.Form["height"].ToString().Length == 0) ? "0": Request.Form["height"].ToString()),
                    Weight = byte.Parse((Request.Form["weight"].ToString() == null || Request.Form["weight"].ToString().Length == 0) ? "0" : Request.Form["weight"].ToString()),
                    BloodPressure = byte.Parse((Request.Form["bloodpressure"].ToString() == null || Request.Form["bloodpressure"].ToString().Length == 0) ? "0" : Request.Form["bloodpressure"].ToString()),
                    BloodGroup = Request.Form["bloodgr"]
                };

                // edit patient
                response = await client.PutAsJsonAsync("https://localhost:7249/api/Patient/UpdatePatient", patient);
                string strPatient = await response.Content.ReadAsStringAsync();
                string rowEffected = System.Text.Json.JsonSerializer.Deserialize<string>(strPatient, options);
                await Console.Out.WriteLineAsync(rowEffected);

                ContactPatientDTO contact = new ContactPatientDTO
                {
                    CId = p.Contacts.CId,
                    Address = Request.Form["address"].ToString(),
                    Dob = DateTime.ParseExact(Request.Form["dob"].ToString(), "yyyy-MM-dd", null),
                    Gender = Request.Form["gender"] == "male" ? true : false,
                    Name = Request.Form["fullname"].ToString(),
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    Phone = Request.Form["phone"].ToString()
                };
                // edit contact patient
                response = await client.PutAsJsonAsync("https://localhost:7249/api/Patient/UpdateContactPatient", contact);
                string strContactPatient = await response.Content.ReadAsStringAsync();
                string rowEffectip = System.Text.Json.JsonSerializer.Deserialize<string>(strContactPatient, options);
                await Console.Out.WriteLineAsync(rowEffectip);
            }

            // edit medical record
            string AddAPI = "https://localhost:7249/api/MedicalRecord/UpdateMedicalRecord";
            var addMR = new MedicalRecordModify()
            {
                ExamCode = Request.Form["examcode"].ToString(),
                PatientId = int.Parse(Request.Form["pid"].ToString()),
                MedicalRecordDate = DateTime.Now,
                ExamReason = Request.Form["reason"],
                MedicalRecordId = mrid,
                DoctorId = doctorid
            };
            response = await client.PutAsJsonAsync(AddAPI, addMR);
            string strData = await response.Content.ReadAsStringAsync();
            string row = System.Text.Json.JsonSerializer.Deserialize<string>(strData, options);
            // chinh sua dich vu su dung trong db

            dao.EditServiceMR(list,mrid);

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
        public string? Name { get; set; }
        public decimal price { get; set; }

        public string? doctorId { get; set; }
    }
}
