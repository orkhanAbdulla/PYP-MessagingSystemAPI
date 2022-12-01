using MessagingSystemApp.Application.Abstractions.Hubs;

namespace MessagingSystemApp.Infrastructure.SignalR.HubServices
{
    public class MessagingHubService : IMessagingHubService
    {
        public Task CreatePostInChannel(string message)
        {
            throw new NotImplementedException();
        }
    }
}
