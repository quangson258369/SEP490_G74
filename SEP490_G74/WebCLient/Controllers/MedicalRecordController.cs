﻿using Azure;
using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/ListMedicalRecord";
            response = await client.GetAsync(MedicalRecordAPI);
            string strMR = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<MedicalRecordDaoOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strMR, option);
            
            // call list search service by service type
            response = await client.GetAsync("https://localhost:7249/api/Service/SearchService?typeId="+ServiceType);
            string strSearch = await response.Content.ReadAsStringAsync();
            List<ServiceDTO> list = System.Text.Json.JsonSerializer.Deserialize<List<ServiceDTO>>(strSearch, options);
            //----------------------------------------------------------------------------------------------------------------------

            // get last patient
            PatientDTO patient = patients.Last();
            // set id for new patient
            int pid = 1;
            if (patient != null && patients.Count > 0) pid = patient.PatientId + 1;
            if (patients.Count == 0) pid = 1;

            MedicalRecordDaoOutputDto mr = listMRs.Last();
            string examCode = "1";
            if (mr != null && listMRs.Count > 0) examCode = (int.Parse( mr.ExamCode)+1).ToString();
            if (listMRs.Count == 0) examCode = "1";

            ViewBag.ExamCode = examCode;
            ViewBag.Pid = pid;
            ViewBag.Service = list;
            ViewBag.ServiceType = ServiceType;
            return View(serviceTypes);
        }

        public async Task<IActionResult> AddMedicalRecord()
        {
            await Console.Out.WriteLineAsync(Request.Form["pid"]);
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Patient/GetPatient/"+ Request.Form["pid"]);
            string str = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            PatientDTO p = System.Text.Json.JsonSerializer.Deserialize<PatientDTO>(str, options);
            if (p.ResultCd == 0) {
                return RedirectToAction("MedicalRecord/Add?ServiceType=");
            }
            else
            {
                HcsContext context = new HcsContext();
                var contacts =context.Contacts.ToList();

                // lay list service da chon
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyViewModel>>(Request.Form["jsonData"]);
                string servicechoose = "";
                foreach (var item in model)
                {
                    servicechoose += item.Name + ",";
                }
                // them benh nhan truoc
                PatientModify patient = new PatientModify()
                {
                    PatientId = int.Parse(Request.Form["pid"].ToString()),
                    ServiceDetailName = servicechoose.Substring(0,servicechoose == null?0:servicechoose.Length-1),
                    Contact = new ContactPatientDTO
                    {
                        CId = contacts.Count + 1,
                        Address = Request.Form["address"].ToString(),
                        Dob = DateTime.ParseExact(Request.Form["dob"].ToString(), "yyyy-MM-dd", null),
                        Gender = Request.Form["gender"] == "male" ? true:false,
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

                // call list medical record
                MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/ListMedicalRecord";
                response = await client.GetAsync(MedicalRecordAPI);
                string strMR = await response.Content.ReadAsStringAsync();
                List<MedicalRecordDaoOutputDto> listMRs = System.Text.Json.JsonSerializer.Deserialize<List<MedicalRecordDaoOutputDto>>(strMR, options);

                MedicalRecordDaoOutputDto mr = listMRs.Last();
                int mrid = 1;
                if (mr != null && listMRs.Count > 0) mrid = mr.MedicalRecordId + 1;
                if (listMRs.Count == 0) mrid = 1;

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

                return RedirectToAction("Index", "MedicalRecord");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            
            return View();
        }


    }

    internal class MyViewModel
    {
        public string Name { get; set; }
        public decimal price { get; set; }
    }
}
