using MessagingSystemApp.Application.Abstractions.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.SignalR.HubServices
{
    public class MessaginHubService : IMessagingHubService
    {
        public Task CreatePostInChannel(string message)
        {
            throw new NotImplementedException();
        }
    }
}
