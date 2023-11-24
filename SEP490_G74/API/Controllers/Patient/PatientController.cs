using AutoMapper;
using HcsBE.Bussiness.Patient;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IMapper _mapper;
        public PatientController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("ListPatient")]
        public IActionResult ListPatient()
        {
            var res = new PatientLogic(_mapper);
            var list = res.ListPatient();
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpGet("GetPatient/{id}")]
        public IActionResult GetPatient(int id)
        {
            var res = new PatientLogic(_mapper);
            var list = res.GetPatientGetId(id);
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpPost("AddPatient")]
        public IActionResult AddPatient(PatientModify p)
        {
            var process = new PatientLogic(_mapper);
            var status = process.Add(p);
            return Ok(status == true ? "Add Successfully!":"Add Failed!");
        }

        [HttpPut("UpdatePatient")]
        public IActionResult UpdatePatient(PatientModify p)
        {
            var process = new PatientLogic(_mapper);
            var status = process.Update(p);
            return Ok(status == true ? "Update Successfully!" : "Update Failed!");
        }

        [HttpDelete("DeletePatient")]
        public IActionResult DeletePatient(int id)
        {
            var process = new PatientLogic(_mapper);
            var status = process.Delete(id);
            if(status == "0") return NotFound();
            if(status == "-1") return BadRequest();
            return Ok("Delete Successfully!");
        }

        [HttpGet("SearchPatient")]
        public IActionResult SearchPatient()
        {

        }
    }
}
