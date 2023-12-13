using AutoMapper;
using HcsBE.Bussiness.ExamResult;
using HcsBE.Bussiness.MedicalRecord;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.MedicalRecord
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        private IMapper mapper;
        public ExamResultController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet("GetServiceMRByServiceType")]
        public IActionResult GetServiceMRByServiceType(int type)
        {
            var res = new ExamResultLogic(mapper);
            var list = res.GetServiceMRByServiceType(type);
            return Ok(list);
        }

        [HttpGet("ListExamResult")]
        public IActionResult ExamResultList()
        {
            var res = new ExamResultLogic(mapper);
            var list = res.getList();
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpGet("GetExamResult/{id}")]
        public IActionResult GetExamResult(int id)
        {
            var res = new ExamResultLogic(mapper);
            var medicalRecord = res.GetExamination(id);
            if (medicalRecord != null) return Ok(medicalRecord);
            return NotFound();
        }

        [HttpPut("UpdateExamResult")]
        public IActionResult updateExamResult(ExaminationResultIdMRDTO mdto)
        {
            var res = new ExamResultLogic(mapper);
            var status = res.Update(mdto);
            if (status == false)
            {
                return NotFound();
            }
            return Ok("Update successfully!");
        }

        [HttpPost("AddExamResult")]
        public IActionResult AddExamResult(ExaminationResultIdMRDTO m)
        {
            var res = new ExamResultLogic(mapper);
            var status = res.Add(m);
            if (status == false)
            {
                return StatusCode(304);
            }
            return Ok("Add successfully!");
        }

        [HttpDelete("DeleteExamResult")]
        public IActionResult DeleteExamResult(int id)
        {
            var res = new ExamResultLogic(mapper);
            var status = res.Delete(id);
            if(status == false) return NotFound();
            return Ok("Delete Successfully!");
        }
    }
}
