using MediatR;
using MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Queries.Request.MessagingRequest
{
    public class GetPostByConnectionIdQueryRequest:IRequest<IEnumerable<GetPostByConnectionIdQueryResponse>>
    {
        public int ConnectionId { get; set; }
        public int ReactionsCount { get; set; }
        public int RepliesCount { get; set; }
    }
}
