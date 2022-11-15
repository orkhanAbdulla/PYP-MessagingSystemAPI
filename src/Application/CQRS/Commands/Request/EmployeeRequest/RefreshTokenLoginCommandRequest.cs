using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest
{
    public class RefreshTokenLoginCommandRequest:IRequest<RefreshTokenLoginCommandResponse>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
