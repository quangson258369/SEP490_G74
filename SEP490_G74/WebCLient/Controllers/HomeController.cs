using HcsBE.Bussiness.Login;
using HcsBE.Dao.GenPassword;
using HcsBE.Dao.Login;
using HcsBE.DTO;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using WebCLient.Models;

namespace WebCLient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public IActionResult Index()
        {
            string failed = TempData["LoginFailedMessage"] as string;
            ViewBag.LoginFailedMessage = failed;
            return View();
        }

        public async Task<IActionResult> Login()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            LoginInputDto input = new LoginInputDto();
            input.Email = Request.Form["username"];
            input.Password = passwordGenerator.GetMD5Hash(Request.Form["password"]);
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7249/api/login", input);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            LoginOutputDto output = JsonSerializer.Deserialize<LoginOutputDto>(strData, option);
            if (output.ResultCd == 0 && output.UserInfoDto.Status == true)
            {
                HttpContext.Session.SetString("USERID", output.UserInfoDto.UserId);
                HttpContext.Session.SetInt32("RoleId", output.UserInfoDto.Roles.FirstOrDefault().RoleId);
                HttpContext.Session.SetString("Token", output.KeyDto.Key);
                Console.WriteLine(HttpContext.Session.GetInt32("RoleId").ToString());

                return RedirectToAction("Index", "Patient");
            }
            else
            {
                TempData["LoginFailedMessage"] = "Login Failed!";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordPost()
        {
            string userName = Request.Form["username"];
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7249/api/CheckUserName?username="+userName, userName);
            string strData = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response);
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool output = JsonSerializer.Deserialize<bool>(strData, option);
            
            if (output)
            {
                OTPGenearator oTPGenearator = new OTPGenearator();
                string OTP = oTPGenearator.OTPGenearatorRandom();
                HttpContext.Session.SetString("OTP", OTP);
                HttpContext.Session.SetString("Username", userName);
                string bodyString = "Your OTP to reset password: " + "\n" + HttpContext.Session.GetString("OTP");
                return RedirectToAction("SendEmail", new { toEmail = Request.Form["username"], subject = "Health care system send you OTP", body = bodyString });   
            }
            else
            {
                return View("Index","Home");
            }
        }
        
        public async Task<IActionResult> VerifyOTP()
        {            
            return View();
        }
        public async Task<IActionResult> VerifyOTPPost()
        {
            string OTP = Request.Form["OTP"];
            if (OTP == HttpContext.Session.GetString("OTP"))
            {
                return RedirectToAction("ResetPassword", "Home");
            }
            else
            {
                return RedirectToAction("VerifyOTP", "Home");
            }

        }
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        public async Task<IActionResult> ResetPasswordPost()
        {
            //HttpContext.Session.GetString("Username");
            //https://localhost:7249/api/UpdatePassword?username=a01652741927%40gmail.com&newPass=123%4012345
            if (Request.Form["newPass"] == Request.Form["verifyPass"])
            {
                HttpResponseMessage response = await 
                    client.PostAsJsonAsync("https://localhost:7249/api/UpdatePassword?username="+ HttpContext.Session.GetString("Username") + "&newPass=" + Request.Form["newPass"], "Done");
                string strData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                bool output = JsonSerializer.Deserialize<bool>(strData, option);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("ResetPassword", "Home");
            }
        }
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string body)
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

            return RedirectToAction("VerifyOTP", "Home");
        }
    }
}