using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MessagingSystemApp.WebClient.Controllers
{
    public class AccountController :Controller
    {
        private const string BaseAPIURl = "https://localhost:7055/api/";
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Token") != null) return RedirectToAction("Index", "Messaging");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginEmployeeCommandRequest command)
        {
            using (var client= new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPIURl+"EmployeeManager/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.PostAsJsonAsync<LoginEmployeeCommandRequest>("Login",command);

                if (responseMessage.StatusCode== HttpStatusCode.OK)
                {
                    string resultMessage = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<LoginEmployeeCommandResponse>(resultMessage);
                    HttpContext.Session.SetString("Token", response.Token.AccessToken);
                    HttpContext.Session.SetString("UserName",response.UserName);
                    return RedirectToAction("Index", "Messaging");
                }
                 
            }
            return BadRequest();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login");
        }
    }
}
