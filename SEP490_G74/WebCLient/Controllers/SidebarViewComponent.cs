using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCLient.Controllers
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            ViewBag.RoleId=HttpContext.Session.GetInt32("RoleId");
            return View();
        }
    }
}
