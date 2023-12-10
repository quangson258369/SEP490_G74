using HcsBE.Bussiness.Login;
using HcsBE.Dao.Login;
using HcsBE.DTO;
using Microsoft.AspNetCore.Mvc;
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
            LoginInputDto input = new LoginInputDto();
            input.Email = Request.Form["username"];
            input.Password = Request.Form["password"];
            HttpResponseMessage response = await client.PostAsync("https://localhost:7249/api/login",input);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            LoginOutputDto output = JsonSerializer.Deserialize<LoginOutputDto>(strData, option);
            if(output.ResultCd == 0 && output.UserInfoDto.Status == true)
            {
                HttpContext.Session.SetString("USERID",output.UserInfoDto.UserId);
                HttpContext.Session.SetString("Token", output.KeyDto.Key);
                return RedirectToAction("Index","Patient");
            }
            else
            {
                TempData["LoginFailedMessage"] = "Login Failed!";
                return RedirectToAction("Index");
            }
        }
    }
}