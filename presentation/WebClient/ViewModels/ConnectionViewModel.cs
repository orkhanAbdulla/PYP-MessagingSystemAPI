using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse;
using System.Collections;

namespace MessagingSystemApp.WebClient.ViewModels
{
    public class ConnectionViewModel
    {
        public List<GetChannelListByUserQueryResponse>? Channels { get; set; } 
        public List<GetDirectMessagesListByUserQueryRespose>? DirectMessages { get; set; }

    }
}
