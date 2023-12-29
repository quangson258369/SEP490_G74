using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HCS.Business.RequestModel.UserRequestModel;
using HCS.Business.Util.MD5PasswordGenerator;
using HCS.Business.IService;
using HCS.Business.RequestModel.PatientRequestModel;
using Microsoft.AspNetCore.Authorization;
using HCS.Business.RequestModel.ContactRequestModel;
using System.Security.Claims;
using HCS.Business.RequestModel.MedicalRecordRequestModel;

namespace HCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel user)
        {
            var loginResponse = await _userService.Login(user);

            if (loginResponse == null || !loginResponse.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(loginResponse);
        }

        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            if(User.Identity is ClaimsIdentity claimsIdentity)
            {
                var idClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if(idClaim == null)
                {
                    return BadRequest("Got error when reading claim Id");
                }

                var isUserId = int.TryParse(idClaim.Value, out int userId);

                if (!isUserId) return BadRequest("User ID in Claims is wrong.");

                var loginResponse = await _userService.GetProfile(userId);

                if (loginResponse == null || !loginResponse.IsSuccess)
                {
                    return BadRequest();
                }

                return Ok(loginResponse);

            }

            return BadRequest();
        }

        [Authorize(Roles ="Admin, Nurse")]
        [HttpPost("patient")]
        public async Task<IActionResult> AddPatient([FromBody] PatientAddModel patient)
        {
            var response = await _userService.AddPatient(patient);
            if (response.IsSuccess)
            {
                return Created("Patient created", response);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin, Nurse")]
        [HttpPost("contact")]
        public async Task<IActionResult> AddContact([FromBody] ContactAddModel contact)
        {
            var response = await _userService.AddContact(contact);
            if (response.IsSuccess)
            {
                return Created("Contact created", response);
            }
            return BadRequest();
        }
       

        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] int userId)
        {
            var response = await _userService.GetPatients(pageIndex, pageSize, userId);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
