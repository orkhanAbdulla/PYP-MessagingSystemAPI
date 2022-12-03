using MessagingSystemApp.Application.Abstractions.Hubs;
using MessagingSystemApp.Domain.Identity;
using MessagingSystemApp.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace MessagingSystemApp.Infrastructure.SignalR.HubServices
{
    public class MessagingHubService : IMessagingHubService
    {
        readonly IHubContext<MessagingHub> _hubContext;
        private readonly UserManager<Employee> _userManager;

        public MessagingHubService(IHubContext<MessagingHub> hubContext, UserManager<Employee> userManager)
        {
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public async Task CreatePostInChannelAsync(int ChannelId,string message,string userName,DateTime date)
        {
            string?[] SignalRIds = _userManager.Users.Include(x => x.EmployeeChannels.Where(x => x.ChannelId == ChannelId)).Where(x=>x.UserName!= userName&&x.SignalRId!=null).Select(x => x.SignalRId).ToArray();
            if (SignalRIds!=null)
            {
                await _hubContext.Clients.Clients(SignalRIds).SendAsync("ReceiveMessage", message,userName, date);
            }
        }
       
    }
}
