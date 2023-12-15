using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Http.Headers;
using System.Text.Json;
using Org.BouncyCastle.Crypto.Macs;
using HcsBE.Dao.ServiceDao;
using Azure;

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
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                MemberRecordAPI = "https://localhost:7249/api/Member/ListMember?page=" + page;
                HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.MemberPagination listMember = JsonSerializer.Deserialize<HcsBE.DTO.MemberPagination>(strData, option);
                ViewBag.CurrentPage = page;
                return View(listMember);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public async Task<IActionResult> AddMemberAsync()
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                MemberRecordAPI = "https://localhost:7249/api/Member/ListRole";
                HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<HcsBE.DTO.RoleDTO> listRole = JsonSerializer.Deserialize<List<HcsBE.DTO.RoleDTO>>(strData, option);
                //list role viewbag
                MemberRecordAPI = "https://localhost:7249/api/Service/ListServiceType";
                response = await client.GetAsync(MemberRecordAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<DataAccess.Entity.ServiceType> listServiceType = JsonSerializer.Deserialize<List<DataAccess.Entity.ServiceType>>(strData, option);
                ViewBag.ListServiceType = listServiceType;
                return View(listRole);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public async Task<IActionResult> ViewDetailMember(int id)
        {
            MemberRecordAPI = "https://localhost:7249/api/Member/GetDoctorId?idUser=" + HttpContext.Session.GetString("USERID");
            HttpResponseMessage response = await client.GetAsync(MemberRecordAPI);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            int doctorId = System.Text.Json.JsonSerializer.Deserialize<int>(strData, option);
            if (HttpContext.Session.GetInt32("RoleId") == 1 || doctorId == id)
            {
                MemberRecordAPI = "https://localhost:7249/api/Member/MemberDetail?id=" + id;
                response = await client.GetAsync(MemberRecordAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                HcsBE.DTO.MemberDetailDTO memberDetail = JsonSerializer.Deserialize<HcsBE.DTO.MemberDetailDTO>(strData, option);
                return View(memberDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInformationMember(IFormFile ImgLink)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                MemberRecordAPI = "https://localhost:7249/api/Member/AddMember";
                string fileName = null;
                string bodyString = null;
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
                    Dob = DateTime.ParseExact(Request.Form["Dob"], "yyyy-MM-dd", null),
                    ServiceType = Request.Form["ServiceType"],
                    ImageLink = fileName,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(MemberRecordAPI, newMember);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string password = JsonSerializer.Deserialize<string>(strData, option);
                if (password != "exist")
                {
                    bodyString = "Your account: " + Request.Form["Gmail"] + "\n" + "Your password: " + password;
                    return RedirectToAction("SendEmail", new { toEmail = Request.Form["Gmail"], subject = "Health care system send you account and password:", body = bodyString });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public async Task<IActionResult> EditMember(int id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
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
                //list role serviceType
                MemberRecordAPI = "https://localhost:7249/api/Service/ListServiceType";
                response = await client.GetAsync(MemberRecordAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<DataAccess.Entity.ServiceType> listServiceType = JsonSerializer.Deserialize<List<DataAccess.Entity.ServiceType>>(strData, option);
                ViewBag.ListServiceType = listServiceType;
                return View(memberDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateInformationMember(IFormFile ImgLink)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
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
                    Dob = DateTime.ParseExact(Request.Form["Dob"], "yyyy-MM-dd", null),
                    ImageLink = fileName,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(MemberRecordAPI, newMember);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                bool rowEffected = JsonSerializer.Deserialize<bool>(strData, option);
                // list serviceType
                /*MemberRecordAPI = "https://localhost:7249/api/Member/UpdateMember";
                response = await client.GetAsync(MemberRecordAPI);
                strData = await response.Content.ReadAsStringAsync();
                option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<DataAccess.Entity.ServiceType> listServiceType = JsonSerializer.Deserialize<List<DataAccess.Entity.ServiceType>>(strData, option);
                ViewBag.ListServiceType = listServiceType;*/
                return RedirectToAction("Index", "Member");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string body)
        {
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("healthsystemcare", "healthsystemcare0@gmail.com"));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder();

                if (body != null)
                {
                    bodyBuilder.TextBody = body;
                }
                else
                {
                    bodyBuilder.TextBody = "Default message when body is null.";
                }

                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    await client.AuthenticateAsync("healthsystemcare0@gmail.com", "bwwy fphq jmqf hkwc");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return RedirectToAction("Index", "Member");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
