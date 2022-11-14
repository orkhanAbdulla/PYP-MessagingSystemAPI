using MessagingSystemApp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse
{
    public class CreateEmployeeCommandResponse
    {
        public Result? Result { get; set; }
        public string? UserId { get; set; }
        public string Message { get; set; } = null!;
    }
}
