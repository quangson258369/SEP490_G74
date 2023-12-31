﻿using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "Admin, Nurse, Doctor, Cashier")]
        [HttpGet("id/{patientId:int}")]
        public async Task<IActionResult> GetMedicalRecordByPatientId(
            int patientId,
            [FromQuery]int pageIndex,
            [FromQuery]int pageSize)
        {
            var result = await _medicalRecordService.GetListMrByPatientId(patientId, pageIndex, pageSize);
        
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin, Nurse, Doctor, Cashier")]
        [HttpGet("detail/id/{id:int}")]
        public async Task<IActionResult> GetMedicalRecordById(
            int id)
        {
            var result = await _medicalRecordService.GetMrById(id);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin, Nurse, Cashier")]
        [HttpPatch("payment/id/{id:int}")]
        public async Task<IActionResult> UpdateMrStatusToPaid(
            int id)
        {
            var result = await _medicalRecordService.UpdateMrStatus(id, true);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
        [HttpPatch("check-up/id/{id:int}")]
        public async Task<IActionResult> UpdateMrStatusToCheckUp(
            int id)
        {
            var result = await _medicalRecordService.UpdateMrStatus(id, false);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin, Nurse, Doctor, Cashier")]
        [HttpPatch("id/{id:int}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] NewMedicalRecordUpdateModel newMedicalRecord)
        {
            var roleClaims = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if(roleClaims is not null)
            {
                var userIdString = roleClaims.Value;
                var userId = int.Parse(userIdString);
                var result = await _medicalRecordService.NewUpdateMedicalRecord(userId, id, newMedicalRecord);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
