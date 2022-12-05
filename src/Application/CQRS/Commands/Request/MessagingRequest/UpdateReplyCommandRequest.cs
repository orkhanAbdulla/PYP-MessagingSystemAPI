using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest
{
    public class UpdateReplyCommandRequest:IRequest<UpdateReplyCommandResponse>
    {
        public int Id { get; set; }
        public int ConnectionId { get; set; }
        public string Message { get; set; } = null!;

    }
}
