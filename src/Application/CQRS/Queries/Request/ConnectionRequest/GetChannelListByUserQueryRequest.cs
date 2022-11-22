using MediatR;
using MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest
{
    public class GetChannelListByUserQueryRequest:IRequest<IEnumerable<GetChannelListByUserQueryResponse>>
    {
        public string UserName { get; set; } = null!;
    }
}
