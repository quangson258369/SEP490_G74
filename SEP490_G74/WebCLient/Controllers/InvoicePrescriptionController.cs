using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class InvoicePrescriptionController : Controller
    {
        private readonly HttpClient client = null;
        private string InvoicePrecriptionAPI = "";
        public InvoicePrescriptionController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> IndexAsync(int page)
        {
            //https://localhost:7249/api/InvoicePrescription/CountListPresciptionInfor
            //https://localhost:7249/api/InvoicePrescription/ListPresciptionInfor?page=1
            InvoicePrecriptionAPI = "https://localhost:7249/api/InvoicePrescription/ListPresciptionInfor?page=" + page;
            HttpResponseMessage response = await client.GetAsync(InvoicePrecriptionAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<HcsBE.DTO.InvoicePrescriptionDTO> listPrescriptionInfor = System.Text.Json.JsonSerializer.Deserialize<List<HcsBE.DTO.InvoicePrescriptionDTO>>(strData, option);
            //
            InvoicePrecriptionAPI = "https://localhost:7249/api/InvoicePrescription/CountListPresciptionInfor";
            response = await client.GetAsync(InvoicePrecriptionAPI);
            strData = await response.Content.ReadAsStringAsync();
            option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            int countListPrescriptionInfor = System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);
            ViewBag.CurrentPage = page;
            ViewBag.TotalItemCount = countListPrescriptionInfor;
            return View(listPrescriptionInfor);
            return View();
        }
    }
}
