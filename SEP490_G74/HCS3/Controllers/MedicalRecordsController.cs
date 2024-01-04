using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }
        
        [Authorize(Roles = "Admin, Nurse")]
        [HttpPost()]
        public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecordAddModel medicalRecord)
        {
            var response = await _medicalRecordService.AddMedicalRecord(medicalRecord);
            
            return response.IsSuccess ? Created($"Medical Record created ",response) : BadRequest(response);
        }

        [Authorize(Roles = "Admin, Nurse")]
        [HttpGet("id/{patientId:int}")]
        public async Task<IActionResult> GetMedicalRecordByPatientId(
            int patientId,
            [FromQuery]int pageIndex,
            [FromQuery]int pageSize)
        {
            var result = await _medicalRecordService.GetListMrByPatientId(patientId, pageIndex, pageSize);
        
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
    }
}
