using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebCLient.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient client = null;
        private string MemberRecordAPI = "";
        public MemberController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> IndexAsync(int page)
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/ListMember?page="+page;
            HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HcsBE.DTO.MemberPagination listMember = JsonSerializer.Deserialize<HcsBE.DTO.MemberPagination>(strData, option);
            ViewBag.CurrentPage = page;
            return View(listMember);
        }
        public async Task<IActionResult> AddMemberAsync()
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/ListRole";
            HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<HcsBE.DTO.RoleDTO> listRole = JsonSerializer.Deserialize<List<HcsBE.DTO.RoleDTO>>(strData, option);
            return View(listRole);
        }
        public async Task<IActionResult> ViewDetailMember(int id)
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/MemberDetail?id="+id;
            HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HcsBE.DTO.MemberDetailDTO memberDetail = JsonSerializer.Deserialize<HcsBE.DTO.MemberDetailDTO>(strData, option);
            return View(memberDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddInformationMember(IFormFile ImgLink)
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/AddMember";
            string fileName=null;
            if (ImgLink != null && ImgLink.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/imgMember");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                fileName = Path.GetFileName(ImgLink.FileName);
                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImgLink.CopyTo(stream);
                }
            }
            var newMember = new MemberDetailDTO
            {
                MemberId = int.TryParse(Request.Form["MemberId"], out int memberIdValue) ? memberIdValue : 0,
                Name = Request.Form["Name"],
                Gender = bool.TryParse(Request.Form["Gender"], out bool genderValue),
                Gmail = Request.Form["Gmail"],
                Phone = Request.Form["Phone"],
                RoleName = Request.Form["RoleName"],
                Address = Request.Form["Address"],
                DoctorSpecialize = Request.Form["DoctorSpecialize"],
                Dob = DateTime.ParseExact(Request.Form["Dob"], "yyyy-MM-dd", null),
                ImageLink = fileName,
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(MemberRecordAPI, newMember);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool rowEffected = JsonSerializer.Deserialize<bool>(strData, option);
            return RedirectToAction("Index", "Member");
        }
        public async Task<IActionResult> EditMember(int id)
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/MemberDetail?id=" + id;
            HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HcsBE.DTO.MemberDetailDTO memberDetail = JsonSerializer.Deserialize<HcsBE.DTO.MemberDetailDTO>(strData, option);
            //list role viewbag
            MemberRecordAPI = "https://localhost:7249/api/Member/ListRole";
            response = await client.GetAsync(MemberRecordAPI);
            strData = await response.Content.ReadAsStringAsync();
            option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<HcsBE.DTO.RoleDTO> listRole = JsonSerializer.Deserialize<List<HcsBE.DTO.RoleDTO>>(strData, option);
            ViewBag.ListRole = listRole;
            return View(memberDetail);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInformationMember(IFormFile ImgLink)
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/UpdateMember";
            string fileName = null;
            if (ImgLink != null && ImgLink.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/imgMember");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                fileName = Path.GetFileName(ImgLink.FileName);
                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImgLink.CopyTo(stream);
                }
            }
            var newMember = new MemberDetailDTO
            {
                MemberId = int.TryParse(Request.Form["MemberId"], out int memberIdValue) ? memberIdValue : 0,
                Name = Request.Form["Name"],
                Gender = bool.TryParse(Request.Form["Gender"], out bool genderValue),
                Gmail = Request.Form["Gmail"],
                Phone = Request.Form["Phone"],
                RoleName = Request.Form["RoleName"],
                Address = Request.Form["Address"],
                DoctorSpecialize = Request.Form["DoctorSpecialize"],
                Dob = DateTime.ParseExact(Request.Form["Dob"], "yyyy-MM-dd", null),
                ImageLink = fileName,
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(MemberRecordAPI, newMember);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool rowEffected = JsonSerializer.Deserialize<bool>(strData, option);
            return RedirectToAction("Index", "Member");
        }
    }
}
