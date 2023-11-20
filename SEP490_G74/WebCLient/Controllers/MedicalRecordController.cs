using Microsoft.AspNetCore.Mvc;

namespace WebCLient.Controllers
{
    public class MedicalRecordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
