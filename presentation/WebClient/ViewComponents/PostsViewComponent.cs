using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MessagingSystemApp.Application.CQRS.Queries.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse;

namespace MessagingSystemApp.WebClient.ViewComponents
{
    public class PostsViewComponent:ViewComponent
    {
        private const string BaseAPIURl = "https://localhost:7055/api/MessagingManager/";
        public async Task<IViewComponentResult> InvokeAsync(int ConnectionId)
        {
            var token = HttpContext.Session.GetString("Token");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPIURl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"GetPostsByConnectionId?ConnectionId={ConnectionId}&ReactionsCount={3}&RepliesCount={3}");

                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string resultMessage = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<List<GetPostByConnectionIdQueryResponse>>(resultMessage);
                    return View(response);
                }

            }

            return View();
        }
    }
}
