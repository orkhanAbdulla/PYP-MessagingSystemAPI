using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Application.CQRS.Queries.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse;
using MessagingSystemApp.WebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MessagingSystemApp.WebClient.Controllers
{
    public class MessagingController :Controller
    {
        private const string BaseAPIURl = "https://localhost:7055/api/";
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (token == null) return RedirectToAction("Login", "Account");
            ConnectionViewModel connectionViewModel=new ConnectionViewModel();
            using (var client=new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(BaseAPIURl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseChanel = await client.GetAsync("ConnectionManager/GetChannelListByUser");
               
                if (responseChanel.StatusCode==HttpStatusCode.OK)
                {
                   string resultChanlel = await responseChanel.Content.ReadAsStringAsync();
                   connectionViewModel.Channels = JsonConvert.DeserializeObject<List<GetChannelListByUserQueryResponse>>(resultChanlel);
                }
            }
            using (var client2 = new HttpClient())
            {
                client2.DefaultRequestHeaders.Clear();
                client2.BaseAddress = new Uri(BaseAPIURl);
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseDirectMessages = await client2.GetAsync("ConnectionManager/GetDirectMessagesListByUser");
                if (responseDirectMessages.StatusCode == HttpStatusCode.OK)
                {
                    string resultDirrect = await responseDirectMessages.Content.ReadAsStringAsync();
                    connectionViewModel.DirectMessages = JsonConvert.DeserializeObject<List<GetDirectMessagesListByUserQueryRespose>>(resultDirrect);
                }
            }
         
            return View(connectionViewModel);
        }
    }
}
