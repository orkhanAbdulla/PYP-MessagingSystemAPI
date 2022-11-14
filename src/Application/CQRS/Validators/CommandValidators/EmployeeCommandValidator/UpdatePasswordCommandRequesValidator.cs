using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.EmployeeCommandValidator
{
    public class UpdatePasswordCommandRequesValidator:AbstractValidator<UpdatePasswordCommandRequest>
    {
        public UpdatePasswordCommandRequesValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Token).NotNull().NotEmpty();
            RuleFor(x => x.Password).Equal(x => x.PasswordConfirm).NotNull().NotEmpty();
        }
    }
}
