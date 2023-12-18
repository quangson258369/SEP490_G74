using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
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

        public async Task<IActionResult> Detail(int id)
        {
            //get Invoice detail
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/InvoiceService/GetInvoiceDetail?id=" + id);
            string strI = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            InvoiceDTO invoice = JsonSerializer.Deserialize<InvoiceDTO>(strI, option);

            int mrid = invoice.InvoiceDetails.First().MedicalRecordId;
            //get list danh sach dich vu kham
            response = await client.GetAsync("https://localhost:7249/api/MedicalRecord/ListServiceUses/" + mrid);
            string strService = await response.Content.ReadAsStringAsync();
            List<ServiceMRDTO> service = JsonSerializer.Deserialize<List<ServiceMRDTO>>(strService, option);

            if (service == null) service = new List<ServiceMRDTO>();
            ViewBag.Service = service;

            return View(invoice);
        }

        public async Task<IActionResult> ComfirmInvoice(int id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 3 || HttpContext.Session.GetInt32("RoleId") == 1)
            {
                string payMethod = Request.Form["PayMethod"].ToString();
                string payDate = DateTime.Now.ToString();
                string u = "https://localhost:7249/api/InvoiceService/UpdateStatusInvoiceService?id=" + id+"&payMethod="+payMethod+"&PayDate="+payDate;
                HttpResponseMessage response = await client.PostAsJsonAsync(u, "oke");
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Console.WriteLine(strData);
                bool row = JsonSerializer.Deserialize<bool>(strData, option);
                return RedirectToAction("Index", "InvoiceService");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task<IActionResult> PrintfInvoice(string Service, int idInvoice) 
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7249/api/InvoiceService/GetInvoiceDetail?id=" + idInvoice);
            string strI = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            InvoiceDTO invoice = JsonSerializer.Deserialize<InvoiceDTO>(strI, option);

            return View();
        }
    }
}
