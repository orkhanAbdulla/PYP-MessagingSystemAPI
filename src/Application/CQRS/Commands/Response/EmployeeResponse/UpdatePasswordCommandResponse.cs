using MessagingSystemApp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse
{
    public class UpdatePasswordCommandResponse
    {
        public Result Result { get; set; } = null!;
    }
}
