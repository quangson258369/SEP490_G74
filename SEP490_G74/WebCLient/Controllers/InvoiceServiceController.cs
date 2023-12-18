using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class InvoiceServiceController : Controller
    {
        private readonly HttpClient client = null;
        public InvoiceServiceController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 3 || HttpContext.Session.GetInt32("RoleId") == 1)
            {
                string uid = HttpContext.Session.GetString("USERID");
                uid = (uid == null || uid.Length == 0) ? "1" : uid;
                // get list search invoice paging
                var list = await getListInvoice(page,int.Parse(uid));
                ViewBag.CurrentPage = page;
                // get count item
                ViewBag.TotalItemCount = await GetTotaInvoiceServicelCountAsync(int.Parse(uid));
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(string term, int status, int page = 1)
        {
            string uid = HttpContext.Session.GetString("USERID");
            uid = (uid == null || uid.Length == 0) ? "1" : uid;
            if(status == -1)
            {
                var rs = await getListInvoice(page, int.Parse(uid));
                return PartialView("_InvoiceServiceTablePartial", rs);
            }
            else
            {
                var result = await SearchAndPagingAsync(term, page, uid, status);
                return PartialView("_InvoiceServiceTablePartial", result);
            }
        }

        private async Task<List<InvoiceDTO>> SearchAndPagingAsync(string term, int page, string uid, int status)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7249/api/InvoiceService/SearchPagingInvoiceService?page={page}&uid={uid}&str={term}&status={status}");
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<InvoiceDTO>> (strData, option);
        }

        private async Task<int> GetTotaInvoiceServicelCountAsync(int uid)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7249/api/InvoiceService/GetInvoiceServiceCount?uid={uid}");
            var strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<int>(strData, option);
        }

        private async Task<List<InvoiceDTO>> getListInvoice(int page, int uid)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7249/api/InvoiceService/GetListInvoiceByCashier?page={page}&uid={uid}");
            var strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<InvoiceDTO>>(strData, option);
        }
    }
}
