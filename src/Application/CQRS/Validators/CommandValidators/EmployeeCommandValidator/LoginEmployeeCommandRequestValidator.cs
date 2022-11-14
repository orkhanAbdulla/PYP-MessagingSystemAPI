using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Request.UserRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.EmployeeCommandValidator
{
    public class LoginEmployeeCommandRequestValidator: AbstractValidator<LoginEmployeeCommandRequest>
    {
        public LoginEmployeeCommandRequestValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
