using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly HttpClient client = null;
        private string HeaderAPI = "";
        public HeaderViewComponent()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.Session.GetString("USERID")!=null)
            {
                HeaderAPI = "https://localhost:7249/api/Prescription/GetDoctorName?idUser=" + HttpContext.Session.GetString("USERID");
                HttpResponseMessage response = await client.GetAsync(HeaderAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string doctorName = System.Text.Json.JsonSerializer.Deserialize<string>(strData, option);
                ViewBag.DoctorName = doctorName;
                //https://localhost:7249/api/Member/GetDoctorId?idUser=1
                HeaderAPI = "https://localhost:7249/api/Member/GetDoctorId?idUser=" + HttpContext.Session.GetString("USERID");
                response = await client.GetAsync(HeaderAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                int doctorId = System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);
                ViewBag.DoctorId = doctorId;
            }
            
            return View();
        }
    }
}
