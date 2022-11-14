using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.UserRequest
{
    public class CreateEmployeeCommandRequest:IRequest<CreateEmployeeCommandResponse>
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } =null!;
    }
}
