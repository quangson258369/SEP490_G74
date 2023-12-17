using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(int page)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 3 || HttpContext.Session.GetInt32("RoleId") == 1)
            {
                string uid = HttpContext.Session.GetString("USERID");
                uid = (uid == null || uid.Length == 0) ? "1" : uid;
                // get list search invoice paging
                var list = await getListInvoice(page,int.Parse(uid));
                // get count item
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private async Task<List<Invoice>> getListInvoice(int page, int uid)
        {
            throw new NotImplementedException();
        }
    }
}
