using HCS.Business.RequestModel.ExaminationResultRequestModel;
using HCS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExaminationResultController : ControllerBase
{
    private readonly IExaminationResultService _service;

    public ExaminationResultController(IExaminationResultService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpPost]
    public async Task<IActionResult> AddExaminationResult(
        [FromBody] ExaminationResultAddModel examinationResultAddModel)
    {
        var result = await _service.AddExaminationResult(examinationResultAddModel);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet("id/{medicalRecordId:int}")]
    public async Task<IActionResult> GetExaminationResultByMedicalRecordId(int medicalRecordId)
    {
        var result = await _service.GetExaminationResultByMedicalRecordId(medicalRecordId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpGet("detail/id/{id:int}")]
    public async Task<IActionResult> GetListExamDetailByMedicalRecordId(int id)
    {
        var result = await _service.GetListExamDetailByMedicalRecordId(id);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpPut("detail/id/{id:int}")]
    public async Task<IActionResult> AddExamDetailByMedicalRecordId(int id, [FromBody] ExaminationDetaislResponseModel examDetails)
    {
        var result = await _service.AddExamDetailsByMedicalRecordId(id, examDetails);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}