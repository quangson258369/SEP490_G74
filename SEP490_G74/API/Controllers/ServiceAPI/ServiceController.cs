using AutoMapper;
using HcsBE.Bussiness.ServiceBusiness;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ServiceAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IMapper mapper;
        public ServiceController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet("ListService")]
        public IActionResult GetAllService()
        {
            var logic = new ServiceLogic(mapper);
            var output = logic.GetAll();
            return Ok(output);
        }
        
       // [HttpGet("SearchService/{name},{typeId}")]
        [HttpGet("SearchService")]
        public IActionResult SearchService(string name, int typeId)
        {
            var logic = new ServiceLogic(mapper);
            var output = logic.SearchService(name, typeId);
            return Ok(output);
        }

        [HttpGet("GetService/{id}")]
        public IActionResult GetService(int id)
        {
            var logic = new ServiceLogic(mapper);
            var output = logic.GetAll();
            return Ok(output);
        }
        
        [HttpPost("AddService")]
        public IActionResult AddService(ServiceDTO service)
        {
            var login = new ServiceLogic(mapper);
            if(service == null)
            {
                return BadRequest();
            }
            var output = login.Add(service);
            if (!output) return StatusCode(304);
            return Ok("Add successful");
        }

        [HttpPut("UpdateService")]
        public IActionResult UpdateServcie(ServiceDTO service)
        {
            var logic = new ServiceLogic(mapper);
            if (service == null)
            {
                return BadRequest();
            }
            var output = logic.Update(service);
            if (output == false) return NotFound();
            return Ok("Update success");
        }

        [HttpDelete("DeleteService/{id}")]
        public IActionResult DeleteService(int id)
        {
            var logic = new ServiceLogic(mapper);
            if(id == 0 || id == null)
            {
                return BadRequest();
            }
            var output = logic.Delete(id);
            if (output == false) return NotFound();
            return Ok("Remove success");
        }
    }
}
