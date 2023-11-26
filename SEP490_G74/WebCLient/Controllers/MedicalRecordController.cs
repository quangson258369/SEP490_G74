using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
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
            List<HcsBE.DTO.MedicalRecordDaoOutputDto> listProducts = JsonSerializer.Deserialize<List<HcsBE.DTO.MedicalRecordDaoOutputDto>>(strData, option);
            return View(listProducts);
        }
        public async Task<IActionResult> MedicalRecordDetail(int id)
        {
            MedicalRecordAPI = "https://localhost:7249/api/MedicalRecord/GetMedicalRecord/"+id;
            HttpResponseMessage response = await client.GetAsync(MedicalRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HcsBE.DTO.MedicalRecordDaoOutputDto medicalRecordDetail = JsonSerializer.Deserialize<HcsBE.DTO.MedicalRecordDaoOutputDto>(strData, option);
            return View(medicalRecordDetail);
        }
    }
}
