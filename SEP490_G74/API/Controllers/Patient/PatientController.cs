using HcsBE.Bussiness.Patient;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpGet("ListPatient")]
        public IActionResult ListPatient()
        {
            var res = new PatientLogic();
            var list = res.ListPatient();
            if (list == null) return NotFound();
            return Ok(list);
        }
    }
}
