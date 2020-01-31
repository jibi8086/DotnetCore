using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleWebApp.Models;


namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        //private const string URL = "https://localhost:44342/";
        private static readonly HttpClient _client;
        private readonly static string baseAddress = "https://localhost:44342/"; //EGATEConstants.BaseAddress;
                                                                                 //private string urlParameters = "?api_key=123";

        static HomeController()
        {
            _client = new HttpClient();
        }
        public  IActionResult Index()
        {
            UserModel login = new UserModel();
            login.EmailAddress="Test";
            login.Username = "um";
            var test = Login(login,10);
            try
            {
                 var data1 = ProviderBase.PostAsync<long>($"api/login/SignUp", login, "");
                login.Username = "um";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View();
        }

        public async Task<ActionResult> Login(UserModel login, int? expireTime) {

            try
            {
                CookieOptions option = new CookieOptions();

                if (expireTime.HasValue)
                    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                else
                    option.Expires = DateTime.Now.AddMilliseconds(10);

                Response.Cookies.Append("token", "123456", option);
                //string token = this.Request.Cookies["at"]?.
                string token = Request.Cookies["token"];
                var data = await ProviderBase.PostAsync<long>($"api/login/SignUp", login, string.Empty);
                return Json(data);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
