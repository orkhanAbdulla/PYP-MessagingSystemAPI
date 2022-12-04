using MessagingSystemApp.Application.Abstractions.Hubs;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using MessagingSystemApp.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.RegularExpressions;

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

        public async Task CreatePostInChannelAsync(int connectionId, string message,string userName)
        {
            string?[] SignalRIds = _userManager.Users.
                Include(x => x.EmployeeChannels.Where(x => x.ChannelId == connectionId)).Where(x => x.UserName != userName && x.SignalRId != null).Select(x=>x.SignalRId).ToArray();
            if (SignalRIds != null)
            {
                await _hubContext.Clients.Clients(SignalRIds).
                 SendAsync("ReceiveMessage", connectionId, message, userName, DateTime.Now);
            }
               
        }
        public async Task PostInDirectlyMessage(Connection connection, string message, string userId,string UserName)
        {
            
            string? SignalRId;
            if (connection.SenderId != userId)
            {
                SignalRId = _userManager.Users.Where(x => x.Id == connection.SenderId && x.SignalRId != null).Select(x => x.SignalRId).FirstOrDefault();
            }
            else
            {
                SignalRId = _userManager.Users.Where(x => x.Id == connection.ReciverId && x.SignalRId != null).Select(x => x.SignalRId).FirstOrDefault();
            }
            if (SignalRId!=null)
            {
                await _hubContext.Clients.Client(SignalRId).SendAsync("ReceiveMessage", connection.Id, message, UserName, DateTime.Now);
            }

        }


    }
}
