using AutoMapper;
using HcsBE.Bussiness.Login;
using HcsBE.Bussiness.MedicalRecord;
using HcsBE.Dao.MedicalRecordDAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.MedicalRecord
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {

        [HttpGet("ListMedicalRecord")]
        public IActionResult MedicalRecordList()
        {
            var res = new MedicalRecordBusinessLogic();
            var list = res.GetListMedicalRecord();
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpGet("GetMedicalRecord/{id}")]
        public IActionResult GetMedicalRecord(int id) 
        {
            var resDao = new MedicalRecordBusinessLogic();
            var medicalRecord = resDao.GetMedicalRecord(id);
            if (medicalRecord == null) return NotFound();
            return Ok(medicalRecord);
        }
    }
}
