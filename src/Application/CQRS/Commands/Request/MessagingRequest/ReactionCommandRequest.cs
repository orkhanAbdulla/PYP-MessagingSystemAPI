using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest
{
    public class ReactionCommandRequest:IRequest<ReactionCommandResponse>
    {
        public int PostId { get; set; }
        public Emoji Emoji { get; set; }
        public int ConnectionId { get; set; }
        public string EmployeeId { get; set; } = null!;
       
    }
}
