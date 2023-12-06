﻿using Azure;
using DataAccess.Entity;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/ListMedicalRecord";
            HttpResponseMessage response = await client.GetAsync(MedicalRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<MedicalRecordDaoOutputDto> listProducts = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strData, option);
            return View(listProducts);
        }

        public async Task<IActionResult> Add(int ServiceType)
        {
            // call list service type
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Service/ListServiceType");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<ServiceType> serviceTypes = System.Text.Json.JsonSerializer.Deserialize<List<ServiceType>>(strData, options);

            // call list patient
            response = await client.GetAsync("https://localhost:7249/api/Patient/ListPatient");
            string strPatient = await response.Content.ReadAsStringAsync();
            List<PatientDTO> patients = System.Text.Json.JsonSerializer.Deserialize<List<PatientDTO>>(strPatient, options);

            // call list medical record
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListMedicalRecord");
            string strMR = await response.Content.ReadAsStringAsync();
            List<MedicalRecordDaoOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strMR, options);


            
            // get last patient
            PatientDTO patient = patients.Last();
            // set id for new patient
            int pid = 1;
            if (patient != null && patients.Count > 0) pid = patient.PatientId + 1;
            if (patients.Count == 0) pid = 1;

            MedicalRecordDaoOutputDto mr = listMRs.Last();
            int examCode = 1;
            if (mr != null && listMRs.Count > 0) examCode = mr.MedicalRecordId + 1;
            if (listMRs.Count == 0) examCode = 1;


            ViewBag.Now = DateTime.Now;
            ViewBag.MrId = examCode;
            ViewBag.Pid = pid;
            ViewBag.ServiceType = ServiceType;
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
                HcsContext context = new HcsContext();
                MedicalRecordDao dao = new MedicalRecordDao();
                var contacts = context.Contacts.ToList();
                // call list medical record
                MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/ListMedicalRecord";
                response = await client.GetAsync(MedicalRecordAPI);
                string strMR = await response.Content.ReadAsStringAsync();
                List<MedicalRecordDaoOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strMR, options);

                MedicalRecordDaoOutputDto mr = listMRs.Last();
                int mrid = 1;
                if (mr != null && listMRs.Count > 0) mrid = mr.MedicalRecordId + 1;
                if (listMRs.Count == 0) mrid = 1;

                // lay list service da chon
                var model = JsonConvert.DeserializeObject<List<MyViewModel>>(Request.Form["jsonData"]);
                List<ServiceMedicalRecord> list = new List<ServiceMedicalRecord>();
                string servicechoose = "";
                foreach (var item in model)
                {
                    servicechoose += item.Name + ",";
                    list.Add(new ServiceMedicalRecord() { ServiceId = item.Sid,MedicalRecordId = mrid});
                }
                

                // them benh nhan truoc
                PatientModify patient = new PatientModify()
                {
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    ServiceDetailName = servicechoose.Substring(0, servicechoose == null ? 0 : servicechoose.Length - 1),
                    Contact = new ContactPatientDTO
                    {
                        CId = contacts.Count + 1,
                        Address = Request.Form["address"].ToString(),
                        Dob = DateTime.ParseExact(Request.Form["dob"].ToString(), "yyyy-MM-dd", null),
                        Gender = Request.Form["gender"] == "male" ? true : false,
                        Name = Request.Form["fullname"].ToString(),
                        PatientId = int.Parse(Request.Form["pid"].ToString()),
                        Phone = Request.Form["phone"].ToString()
                    }
                };

                options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                // add patient
                response = await client.PostAsJsonAsync("https://localhost:7249/api/Patient/AddPatient", patient);
                string strPatient = await response.Content.ReadAsStringAsync();
                string rowEffected = System.Text.Json.JsonSerializer.Deserialize<string>(strPatient, options);

                
                // them medical record
                string AddAPI = "https://localhost:7249/api/MedicalRecord/AddMedicalRecord";
                var addMR = new MedicalRecordModify()
                {
                    ExamCode = Request.Form["examcode"].ToString(),
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

            ViewBag.Service = service;
            ViewBag.Patient = p;

            return View(medicalRecordDetail);
        }

        // copy to add and fix
        
        public async Task<IActionResult> Edit(int id, int ServiceType)
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
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/GetMedicalRecord/"+id;
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
            ViewBag.ServiceType = ServiceType;
            return View(serviceTypes);
        }

        [HttpPost]
        public async Task<IActionResult> EditHaa([FromBody] JObject jsonData)
        {
            foreach (var property in jsonData.Properties())
            {
                var key = property.Name;
                var value = property.Value;
                // Process key-value pair as needed
                await Console.Out.WriteLineAsync(key+"-"+value);
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            // call list service type
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Service/ListServiceType");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
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
