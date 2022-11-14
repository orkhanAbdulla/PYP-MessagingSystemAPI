using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest
{
    public class EmployeeEmailConfirmationCommandRequest:IRequest<EmployeeEmailConfirmationCommandResponse>
    {
        public string Token { get; set; } = null!;
        public string UserId { get; set; }=null!;
    }
}
