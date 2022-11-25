using MediatR;
using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Application.CQRS.Commands.Response.ConnectionResponse;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest
{
    public class UpdatePostCommandRequest:IRequest<UpdatePostCommandResponse>
    {
        public int Id { get; set; }
        public int ConnectionId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
