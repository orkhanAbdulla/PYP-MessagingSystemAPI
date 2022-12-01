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
        public Task CreatePostInChannel(string message);
    }
}
