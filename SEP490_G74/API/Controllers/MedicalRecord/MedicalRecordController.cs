using AutoMapper;
using HcsBE.Bussiness.MedicalRecord;
using HcsBE.DTO;
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
            if (medicalRecord!= null) return Ok(medicalRecord);
            return NotFound();
        }

        [HttpPut("UpdateMedicalRecord")]
        public IActionResult updateMedicalRecord(MedicalRecordDaoOutputDto mdto)
        {
            var resDao = new MedicalRecordBusinessLogic();
            var status = resDao.Update(mdto);
            if (status == false)
            {
                return NotFound();
            }
            return Ok("Update successfully!");
        }

        [HttpPost("AddMedicalRecord")]
        public IActionResult AddMedicalRecord(MedicalRecordDaoOutputDto m)
        {
            var resDao = new MedicalRecordBusinessLogic();
            var status = resDao.AddMR(m);
            if (status == false)
            {
                return StatusCode(304);
            }
            return Ok("Add successfully!");
        }

        [HttpDelete("DeleteMedicalRecord")]
        public IActionResult DeleteMedicalRecord(int id)
        {
            var resDao = new MedicalRecordBusinessLogic();
            var status = resDao.DeleteMR(id);

            if (status == "-1")
            {
                return StatusCode(304);
            }else if(status == "0") {
                return NotFound();
            }
            return Ok("Delete successfully!");
        }
    }
}
