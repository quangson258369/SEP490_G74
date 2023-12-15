 using Azure;
using DataAccess.Entity;
using HcsBE.Dao.ServiceDao;
using HcsBE.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class PrecriptionController : Controller
    {
        private readonly HttpClient client = null;
        private string PrecriptionAPI = "";
        public PrecriptionController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        
        public async Task<IActionResult> IndexAsync(int page)
        {
            //https://localhost:7249/api/Prescription/CountListPresciptionInfor
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                PrecriptionAPI = "https://localhost:7249/api/Prescription/ListPresciptionInfor?page=" + page + "&idUser=" + HttpContext.Session.GetString("USERID");
                HttpResponseMessage response = await client.GetAsync(PrecriptionAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<HcsBE.DTO.PrescriptionInforDTO> listPrescriptionInfor = System.Text.Json.JsonSerializer.Deserialize<List<HcsBE.DTO.PrescriptionInforDTO>>(strData, option);
                //
                PrecriptionAPI = "https://localhost:7249/api/Prescription/CountListPresciptionInfor";
                response = await client.GetAsync(PrecriptionAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                int countListPrescriptionInfor = System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);
                ViewBag.CurrentPage = page;
                ViewBag.TotalItemCount = countListPrescriptionInfor;
                return View(listPrescriptionInfor);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //AddPrescription
        public async Task<IActionResult> AddPrescription(int medicalRecordId)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                ViewBag.MedicalRCid = medicalRecordId;
                PrecriptionAPI = "https://localhost:7249/api/Supplies/ListSuppliesType";
                HttpResponseMessage response = await client.GetAsync(PrecriptionAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<HcsBE.DTO.SuppliesTypeDTO> listTypeSupplies = System.Text.Json.JsonSerializer.Deserialize<List<HcsBE.DTO.SuppliesTypeDTO>>(strData, option);
                ViewBag.ListTypeSupplies = listTypeSupplies;
                //https://localhost:7249/api/Prescription/GetDoctorName?idDoctor=1
                PrecriptionAPI = "https://localhost:7249/api/Prescription/GetDoctorName?idUser=" + HttpContext.Session.GetString("USERID");
                response = await client.GetAsync(PrecriptionAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string doctorName = System.Text.Json.JsonSerializer.Deserialize<string>(strData, option);
                ViewBag.DoctorName = doctorName;
                //https://localhost:7249/api/Prescription/GetPatientContactByMedicalRCId?medicalRCId=2
                PrecriptionAPI = "https://localhost:7249/api/Prescription/GetPatientContactByMedicalRCId?medicalRCId="+ medicalRecordId;
                response = await client.GetAsync(PrecriptionAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.ContactPatientInPrescriptionDTO contactPatient = System.Text.Json.JsonSerializer.Deserialize<HcsBE.DTO.ContactPatientInPrescriptionDTO>(strData, option);
                return View(contactPatient);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddInformationPrecription([FromBody] List<Dictionary<string, string>> data)
        {
            //https://localhost:7249/api/Prescription/AddSuppliesInPrescription
            //foreach (var row in data)
            //{
            //    string idOfSupplies = row["idOfSupplies"];
            //    string unitInStock = row["unitInStock"];
            //    Console.WriteLine($"Hidden Input Value: {idOfSupplies}");
            //    Console.WriteLine($"Number Input Value: {unitInStock}");
            //    //PrecriptionAPI = "https://localhost:7249/api/Prescription/AddSuppliesInPrescription";
            //    //String createDate = Request.Form["creatDate"];
            //    //var newSuppliesToPrescription = new SuppliesPrescription
            //    //{
            //    //    SId = int.TryParse(idOfSupplies, out int SId) ? SId : 0,
            //    //    Quantity = int.TryParse(unitInStock, out int Quantity) ? Quantity : 0,
            //    //};
            //    //HttpResponseMessage response = await client.PostAsJsonAsync(PrecriptionAPI, newSuppliesToPrescription);
            //    //string strData = await response.Content.ReadAsStringAsync();
            //    //var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            //    //bool rowEffected = JsonSerializer.Deserialize<bool>(strData, option);
            //}
            HttpContext.Session.Set("PrescriptionData", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
            //var jsonDataBytes = HttpContext.Session.Get("PrescriptionData");
            //if (jsonDataBytes != null)
            //{
            //    var jsonData = Encoding.UTF8.GetString(jsonDataBytes);
            //    var dataa = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
            //    foreach (var row in dataa)
            //    {
            //        string idOfSupplies = row["idOfSupplies"];
            //        string unitInStock = row["unitInStock"];
            //        Console.WriteLine($"Hidden Input Value: {idOfSupplies}");
            //        Console.WriteLine($"Number Input Value: {unitInStock}");
            //    }
            //}
            var responseCheck = new { Message = "Data received successfully" };
            return Json(responseCheck);


        }
        public async Task<IActionResult> AddInformationSuppliesToPrecription(int medicalRCid)
        {

            PrecriptionAPI = "https://localhost:7249/api/Prescription/AddPrescription";
            String createDate = Request.Form["creatDate"];
            var newPrescription = new Prescription
            {
                CreateDate = DateTime.ParseExact(createDate, "yyyy-MM-dd", null),
                Diagnose = Request.Form["diagnose"],
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(PrecriptionAPI, newPrescription);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            int idInserted = System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);
            var jsonDataBytes = HttpContext.Session.Get("PrescriptionData");
            if (jsonDataBytes != null)
            {
                var jsonData = Encoding.UTF8.GetString(jsonDataBytes);
                var data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                foreach (var row in data)
                {
                    string idOfSupplies = row["idOfSupplies"];
                    string unitInStock = row["unitInStock"];
                    PrecriptionAPI = "https://localhost:7249/api/Prescription/AddSuppliesInPrescription";
                    var newSuppliesToPrescription = new SuppliesPrescription
                    {
                        SId = int.TryParse(idOfSupplies, out int result) ? result : 0,
                        Quantity = int.TryParse(unitInStock, out int Quantity) ? Quantity : 0,
                        PrescriptionId = idInserted,
                    };
                    response = await client.PostAsJsonAsync(PrecriptionAPI, newSuppliesToPrescription);
                    strData = await response.Content.ReadAsStringAsync();
                    option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    bool rowEffectedSuccess = System.Text.Json.JsonSerializer.Deserialize<bool>(strData, option);
                }
            }
            //https://localhost:7249/api/Prescription/AddPrescriptionInMedicalRC?medicalRCid=1&prescriptionId=1
            PrecriptionAPI = "https://localhost:7249/api/Prescription/AddPrescriptionInMedicalRC?medicalRCid=" + medicalRCid + "&prescriptionId=" + idInserted;
            response = await client.PostAsJsonAsync(PrecriptionAPI, "oke");
            strData = await response.Content.ReadAsStringAsync();
            option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool rowEffected = System.Text.Json.JsonSerializer.Deserialize<bool>(strData, option);
            return RedirectToAction("Index", "Precription");

        }
        //ViewDetailPrescription
        public async Task<IActionResult> ViewDetailPrescription(int id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                //https://localhost:7249/api/Prescription/GetPatientContactByPrescriptionId?idPrescription=1
                PrecriptionAPI = "https://localhost:7249/api/Prescription/GetPatientContactByPrescriptionId?idPrescription=" + id;
                HttpResponseMessage response = await client.GetAsync(PrecriptionAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                ContactPatientInPrescriptionDTO detailContactPatient = System.Text.Json.JsonSerializer.Deserialize<ContactPatientInPrescriptionDTO>(strData, option);
                ViewBag.DetailContactPatient = detailContactPatient;
                //https://localhost:7249/api/Prescription/GetListSuppliesByPrescriptionId?idPrescription=1
                PrecriptionAPI = "https://localhost:7249/api/Prescription/GetListSuppliesByPrescriptionId?idPrescription=" + id;
                response = await client.GetAsync(PrecriptionAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<PrescriptionDetailSuppliesDTO> listSuppliesByPrescriptionId = System.Text.Json.JsonSerializer.Deserialize<List<PrescriptionDetailSuppliesDTO>>(strData, option);

                return View(listSuppliesByPrescriptionId);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
