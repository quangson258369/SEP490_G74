using Azure;
using DataAccess.Entity;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class PatientController : Controller
    {
        private readonly HttpClient client = null;

        public PatientController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Patient/ListPatientPaging?page="+page);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<PatientDTO> result = JsonSerializer.Deserialize<List<PatientDTO>>(strData,option);

            response = await client.GetAsync("https://localhost:7249/api/Patient/GetCountOfListPatient");
            strData = await response.Content.ReadAsStringAsync();
            int countListService = JsonSerializer.Deserialize<int>(strData, option);
            ViewBag.CurrentPage = page;
            ViewBag.TotalItemCount = countListService;
            return View(result);
        }
        
        public async Task<IActionResult> Add()
        {
            // call get patiet information
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/Patient/ListPatient");
            string str = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<PatientDTO> p = JsonSerializer.Deserialize<List<PatientDTO>>(str, option);
            // get last patient
            PatientDTO patient = p.Last();
            // set id for new patient
            int pid = 1;
            if (patient != null && p.Count > 0) pid = patient.PatientId + 1;
            if (p.Count == 0) pid = 1;
            ViewBag.Pid = pid;
            return View();
        }

        public async Task<IActionResult> AddPatient()
        {
            HcsContext context = new HcsContext();
            var contacts = context.Contacts.ToList();

            // them benh nhan truoc
            PatientModify patient = new PatientModify()
            {
                PatientId = int.Parse(Request.Form["pid"].ToString()),
                ServiceDetailName = "chuan bi xoa bo",
                Height = byte.Parse((Request.Form["height"].ToString() == null || Request.Form["height"].ToString().Length == 0) ? "0" : Request.Form["height"].ToString()),
                Weight = byte.Parse((Request.Form["weight"].ToString() == null || Request.Form["weight"].ToString().Length == 0) ? "0" : Request.Form["weight"].ToString()),
                BloodPressure = byte.Parse((Request.Form["bloodpress"].ToString() == null || Request.Form["bloodpress"].ToString().Length == 0) ? "0" : Request.Form["bloodpress"].ToString()),
                BloodGroup = Request.Form["bloodgr"],
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

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // add patient
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7249/api/Patient/AddPatient", patient);
            string strPatient = await response.Content.ReadAsStringAsync();
            string rowEffected = JsonSerializer.Deserialize<string>(strPatient, options);
            return RedirectToAction("Index","Patient");
        }
    }
}
