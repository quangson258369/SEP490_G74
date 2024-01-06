using HCS.Business.RequestModel.SuppliesTypeRequestModel;
using HCS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliesTypeController :ControllerBase
{
    private readonly ISuppliesTypeService _suppliesTypeService;

    public SuppliesTypeController(ISuppliesTypeService suppliesTypeService)
    {
        _suppliesTypeService = suppliesTypeService;
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSuppliesType(int id)
    {
        var result = await _suppliesTypeService.GetSuppliesType(id);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin, Doctor, Nurse, Cashier")]
    [HttpGet]
    public async Task<IActionResult> GetAllSuppliesType()
    {
        var result = await _suppliesTypeService.GetAllSuppliesType();

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddSuppliesType([FromBody] SuppliesTypeAddModel suppliesType)
    {
        var result = await _suppliesTypeService.AddSuppliesType(suppliesType);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSuppliesType(int id, SuppliesTypeUpdateModel suppliesType)
    {
        var result = await _suppliesTypeService.UpdateSuppliesType(id, suppliesType);

        return result.IsSuccess ? Ok(result) : NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSuppliesType(int id)
    {
        var result = await _suppliesTypeService.DeleteSuppliesType(id);

        return result.IsSuccess ? Ok(result) : NoContent();
    }
}