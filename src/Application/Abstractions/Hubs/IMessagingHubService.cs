using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Hubs
{
    public interface IMessagingHubService
    {
        public Task CreatePostInChannelAsync(int connectionId, string message, string userName);
        public Task PostInDirectlyMessage(Connection connectionId, string message, string userId, string UserName);
    }
}
