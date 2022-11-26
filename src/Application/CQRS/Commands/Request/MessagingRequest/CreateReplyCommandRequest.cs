using MediatR;
using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest
{
    public class CreateReplyCommandRequest:IRequest<CreateReplyCommandResponse>,IMapFrom<Post>
    {
        public int ReplyPostId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public int ConnectionId { get; set; }
        public string Message { get; set; } = null!;
    }
}
