using HcsBE.Bussiness.Login;
using HcsBE.Bussiness.MedicalRecord;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.MedicalRecord
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        [HttpGet]
        public IActionResult MedicalRecordList()
        {
            var res = new MedicalRecordBusinessLogic();
            var list = res.GetListMedicalRecord();
            if (list == null) return BadRequest();
            return Ok(list);
        }

        [HttpGet("GetMedicalRecord/{id}")]
        public IActionResult GetMedicalRecord(int id) 
        {

            return Ok();
        }
    }
}
