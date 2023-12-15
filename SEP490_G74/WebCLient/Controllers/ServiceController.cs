using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class ServiceController : Controller
    {
        private readonly HttpClient client = null;
        private string ServicedAPI = "";
        public ServiceController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> IndexAsync(int page)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                //https://localhost:7249/api/Service/GetCountOfListService
                ServicedAPI = "https://localhost:7249/api/Service/ListService?page=" + page;
                HttpResponseMessage response = await client.GetAsync(ServicedAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<HcsBE.DTO.ServiceDTO> listService = JsonSerializer.Deserialize<List<HcsBE.DTO.ServiceDTO>>(strData, option);
                ServicedAPI = "https://localhost:7249/api/Service/GetCountOfListService";
                response = await client.GetAsync(ServicedAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                int countListService = JsonSerializer.Deserialize<int>(strData, option);

                ViewBag.CurrentPage = page;
                ViewBag.TotalItemCount = countListService;
                return View(listService);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> EditService(int id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                ServicedAPI = "https://localhost:7249/api/Service/ListServiceType";
                HttpResponseMessage response = await client.GetAsync(ServicedAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<DataAccess.Entity.ServiceType> listServiceType = JsonSerializer.Deserialize<List<DataAccess.Entity.ServiceType>>(strData, option);
                ViewBag.ListServiceType = listServiceType;
                ServicedAPI = "https://localhost:7249/api/Service/GetService/" + id;
                response = await client.GetAsync(ServicedAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.ServiceDTO serviceDetail = JsonSerializer.Deserialize<HcsBE.DTO.ServiceDTO>(strData, option);
                Console.WriteLine(id);
                return View(serviceDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> EditInformationService(int id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                ServicedAPI = "https://localhost:7249/api/Service/UpdateService?id=" + id;
                var newService = new Service
                {
                    ServiceId= id,
                    ServiceTypeId = int.TryParse(Request.Form["ServiceTypeId"], out int memberIdValue) ? memberIdValue : 0,
                    ServiceName = Request.Form["ServiceName"],
                    Price = decimal.TryParse(Request.Form["Price"], out decimal price) ? price : 0,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(ServicedAPI, newService);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                String rowEffected = JsonSerializer.Deserialize<String>(strData, option);
                return RedirectToAction("Index", "Service");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> AddService()
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                ServicedAPI = "https://localhost:7249/api/Service/ListServiceType";
                HttpResponseMessage response = await client.GetAsync(ServicedAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<DataAccess.Entity.ServiceType> listServiceType = JsonSerializer.Deserialize<List<DataAccess.Entity.ServiceType>>(strData, option);
                return View(listServiceType);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> AddInformationService()
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                ServicedAPI = "https://localhost:7249/api/Service/AddService";
                var newService = new Service
                {
                    ServiceTypeId = int.TryParse(Request.Form["ServiceTypeId"], out int memberIdValue) ? memberIdValue : 0,
                    ServiceName = Request.Form["ServiceName"],
                    Price = decimal.TryParse(Request.Form["Price"], out decimal price) ? price : 0,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(ServicedAPI, newService);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                String rowEffected = JsonSerializer.Deserialize<String>(strData, option);
                return RedirectToAction("Index", "Service");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            } 
        }
    }
}
