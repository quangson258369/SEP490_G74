using DataAccess.Entity;
using HcsBE.Bussiness.Patient;
using HcsBE.Bussiness.Supplies;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Supplies
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        [HttpGet("ListSupplies")]
        public IActionResult ListSupplies(int page)
        {
            var res = new SuppliesLogic();
            var list = res.GetListSupplies(page);
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpGet("SuppliesDetail")]
        public IActionResult SuppliesDetail(int id)
        {
            var res = new SuppliesLogic();
            var list = res.GetSuppliesDetail(id);
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpPost("UpdateSupplies")]
        public IActionResult EditSupplies(SuppliesExcuteDTO supply)
        {
            var res = new SuppliesLogic();
            var rowAffected = res.EditSupplies(supply);
            return Ok(rowAffected);
        }
        [HttpPost("AddSupplies")]
        public IActionResult AddSupplies(SuppliesExcuteDTO supply)
        {
            var res = new SuppliesLogic();
            var rowAffected = res.AddSupplies(supply);
            return Ok(rowAffected);
        }
        [HttpGet("ListSuppliesType")]
        public IActionResult ListSuppliesType()
        {
            var res = new SuppliesLogic();
            var list = res.GetListSuppliesTypes();
            if (list == null) return NotFound();
            return Ok(list);
        }
    }
}
