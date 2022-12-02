using MessagingSystemApp.Application.Abstractions.Hubs;
using MessagingSystemApp.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MessagingSystemApp.Infrastructure.SignalR.HubServices
{
    public class MessagingHubService : IMessagingHubService
    {
        readonly IHubContext<MessagingHub> _hubContext;

        public MessagingHubService(IHubContext<MessagingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task CreatePostInChannel(string message)
        {
            
            throw new NotImplementedException();
           
        }
       
    }
}
