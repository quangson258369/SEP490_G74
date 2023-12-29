using HCS.Business.IService;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost("medical-record")]
        public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecordAddModel medicalRecord)
        {
            var response = await _medicalRecordService.AddMedicalRecord(medicalRecord);
            
            return response.IsSuccess ? Created($"Medical Record created ",response) : BadRequest(response);
        }
        
        
    }
}
