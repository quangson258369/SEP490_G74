using HcsBE.Bussiness.Member;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController:ControllerBase
    {
        [HttpGet("ListMember")]
        public IActionResult ListMember(int page)
        {
            var res = new MemberLogic();
            var list = res.GetListMembers(page);
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpGet("MemberDetail")]
        public IActionResult MemberDetail(int id)
        {
            var res = new MemberLogic();
            var list = res.GetMembersDetail(id);
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpGet("ListRole")]
        public IActionResult ListRole()
        {
            var res = new MemberLogic();
            var list = res.GetRoles();
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpPost("AddMember")]
        public IActionResult AddMember(MemberDetailDTO member )
        {
            var res = new MemberLogic();
            var list = res.AddMember(member);
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpPost("UpdateMember")]
        public IActionResult EditMember(MemberDetailDTO member)
        {
            var res = new MemberLogic();
            var list = res.EditMember(member);
            if (list == null) return NotFound();
            return Ok(list);
        }
        [HttpPost("role")]
        public IActionResult role(int id)
        {
            var res = new MemberLogic();
            var list = res.rolebyid(id);
            if (list == null) return NotFound();
            return Ok(list);
        }
    }
}
