using HCS.Business.RequestModel.ServiceTypeRequestModel;
using HCS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceTypeController : ControllerBase
{
    private readonly IServiceTypeService _service;

    public ServiceTypeController(IServiceTypeService service)
    {
        _service = service;
    }

    //[Authorize(Roles = "Admin, Doctor, Nurse")]
    //[HttpGet("{id:int}")]
    //public async Task<IActionResult> GetServiceType(int id)
    //{
    //    var result = await _service.GetServiceType(id);

    //    return result.IsSuccess ? Ok(result) : BadRequest(result);
    //}

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetListServiceType(int id)
    {
        var result = await _service.GetListServiceType(id);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpGet("service/{id:int}")]
    public async Task<IActionResult> GetListServiceBySerivceTypeId(int id)
    {
        var result = await _service.GetListServiceByServiceTypeId(id);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpPost]
    public async Task<IActionResult> AddServiceType([FromBody] ServiceTypeAddModel serviceTypeAddModel)
    {
        var result = await _service.AddServiceType(serviceTypeAddModel);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateServiceType(int id, [FromBody] ServiceTypeUpdateModel serviceTypeUpdateModel)
    {
        var result = await _service.UpdateServiceType(id, serviceTypeUpdateModel);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpDelete("{id:int}")]
    public async Task DeleteServiceType(int id)
    {
        await _service.DeleteServiceType(id);
    }
}