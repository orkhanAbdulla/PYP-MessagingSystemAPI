using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest
{
    public class DeleteReplyCommandRequest:IRequest<DeleteReplyCommandResponse>
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = null!;
        public int ConnectionId { get; set; }
    }
}
