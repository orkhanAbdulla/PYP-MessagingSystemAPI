using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest
{
    public class AddUserToChannelCommandRequest:IRequest<AddUserToChannelCommandResponse>
    {
        public int ConnectionId { get; set; }
        public string UserName { get; set; } = null!;
        public string AddedUser { get; set; } = null!;
    }
}
