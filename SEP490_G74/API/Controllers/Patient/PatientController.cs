using AutoMapper;
using HcsBE.Bussiness.MedicalRecord;
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

        [HttpGet("ListMRByPatient/{pid}")]
        public IActionResult ListMRByPatient(int pid)
        {
            var res = new PatientLogic(_mapper);
            var list = res.ListMedicalRecordByPatient(pid);
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpGet("ListPatient")]
        public IActionResult ListPatient()
        {
            var res = new PatientLogic(_mapper);
            var list = res.ListPatient();
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpGet("ListPatientPaging")]
        public IActionResult ListPatient(int page)
        {
            var res = new PatientLogic(_mapper);
            var list = res.ListPatientPaging(page);
            if (list == null) return NotFound();
            return Ok(list);
        }

        [HttpGet("GetCountOfListPatient")]
        public IActionResult GetCountOfListPatient()
        {
            var res = new PatientLogic(_mapper);
            var output = res.GetCountOfListPatient();
            return Ok(output);
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
        
        [HttpPost("AddContactPatient")]
        public IActionResult AddContactPatient(ContactPatientDTO p)
        {
            var process = new PatientLogic(_mapper);
            var status = process.AddContactForPatient(p);
            return Ok(status);
        }

        [HttpPut("UpdatePatient")]
        public IActionResult UpdatePatient(PatientModify p)
        {
            var process = new PatientLogic(_mapper);
            var status = process.Update(p);
            return Ok(status == true ? "Update Successfully!" : "Update Failed!");
        }

        [HttpPut("UpdateContactPatient")]
        public IActionResult UpdateContactPatient(ContactPatientDTO c)
        {
            var process = new PatientLogic(_mapper);
            var status = process.UpdateContactForPatient(c);
            return Ok(status == true ? "Update Contact Patient Successfully!" : "Update Contact Patient Failed!");
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
        public IActionResult SearchPatient(string name)
        {
            var process = new PatientLogic(_mapper);
            var list= process.SearchPatient(name);
            if (list == null) return NotFound();
            return Ok(list);
        }
    }
}
