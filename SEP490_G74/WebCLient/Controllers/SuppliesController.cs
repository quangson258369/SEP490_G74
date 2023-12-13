using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class SuppliesController : Controller
    {
        private readonly HttpClient client = null;
        private string SuppliesRecordAPI = "";
        public SuppliesController() 
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> Index(int page)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/ListSupplies?page=" + page;
                HttpResponseMessage response = await client.GetAsync(SuppliesRecordAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.SuppliesPagination listSupplies = JsonSerializer.Deserialize<HcsBE.DTO.SuppliesPagination>(strData, option);
                ViewBag.CurrentPage = page;
                return View(listSupplies);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> SuppliesDetailAsync(int id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/SuppliesDetail?id=" + id;
                HttpResponseMessage response = await client.GetAsync(SuppliesRecordAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.SuppliesDetailDTO suppliesDetail = JsonSerializer.Deserialize<HcsBE.DTO.SuppliesDetailDTO>(strData, option);
                return View(suppliesDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> EditSupplies(int id) {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/SuppliesDetail?id=" + id;
                HttpResponseMessage response = await client.GetAsync(SuppliesRecordAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.SuppliesDetailDTO suppliesDetail = JsonSerializer.Deserialize<HcsBE.DTO.SuppliesDetailDTO>(strData, option);
                //ViewBag list suppliesType
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/ListSuppliesType";
                response = await client.GetAsync(SuppliesRecordAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<HcsBE.DTO.SuppliesTypeDTO> listSuppliesType = JsonSerializer.Deserialize<List<HcsBE.DTO.SuppliesTypeDTO>>(strData, option);
                ViewBag.ListSuppliesType = listSuppliesType;
                return View(suppliesDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public async Task<IActionResult> EditInformationSupplies()
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/UpdateSupplies";
                String inputdayString = Request.Form["InputDay"];
                String expString = Request.Form["Exp"];
                var newSupplies = new SuppliesExcuteDTO
                {
                    SId = int.TryParse(Request.Form["SId"], out int SId) ? SId : 0,
                    SName = Request.Form["SName"],
                    Uses = Request.Form["Uses"],
                    Exp = DateTime.ParseExact(expString, "yyyy-MM-dd", null),
                    Distributor = Request.Form["Distributor"],
                    UnitInStock = (short)(short.TryParse(Request.Form["UnitInStock"], out short unitInStock) ? unitInStock : 0),
                    Price = decimal.TryParse(Request.Form["Price"], out decimal price) ? price : 0,
                    SuppliesTypeID = int.TryParse(Request.Form["SuppliesTypeName"], out int suppliesTypeId) ? suppliesTypeId : 0,
                    InputDay = DateTime.ParseExact(inputdayString, "yyyy-MM-dd", null),
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(SuppliesRecordAPI, newSupplies);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                bool rowEffected = JsonSerializer.Deserialize<bool>(strData, option);
                return RedirectToAction("Index", "Supplies");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> AddInformationSupplies()
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/AddSupplies";
                String inputdayString = Request.Form["InputDay"];
                String expString = Request.Form["Exp"];
                var newSupplies = new SuppliesExcuteDTO
                {
                    SId = int.TryParse(Request.Form["SId"], out int SId) ? SId : 0,
                    SName = Request.Form["SName"],
                    Uses = Request.Form["Uses"],
                    Exp = DateTime.ParseExact(expString, "yyyy-MM-dd", null),
                    Distributor = Request.Form["Distributor"],
                    UnitInStock = (short)(short.TryParse(Request.Form["UnitInStock"], out short unitInStock) ? unitInStock : 0),
                    Price = decimal.TryParse(Request.Form["Price"], out decimal price) ? price : 0,
                    SuppliesTypeID = int.TryParse(Request.Form["SuppliesTypeName"], out int suppliesTypeId) ? suppliesTypeId : 0,
                    InputDay = DateTime.ParseExact(inputdayString, "yyyy-MM-dd", null),
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(SuppliesRecordAPI, newSupplies);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                bool rowEffected = JsonSerializer.Deserialize<bool>(strData, option);
                return RedirectToAction("Index", "Supplies");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> AddSupplies()
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                SuppliesRecordAPI = "https://localhost:7249/api/Supplies/ListSuppliesType";
                HttpResponseMessage response = await client.GetAsync(SuppliesRecordAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<HcsBE.DTO.SuppliesTypeDTO> listSuppliesType = JsonSerializer.Deserialize<List<HcsBE.DTO.SuppliesTypeDTO>>(strData, option);
                return View(listSuppliesType);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
