﻿using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.EmployeeCommandValidator
{
    public class ChangePasswordCommandRequestValidator : AbstractValidator<ChangePasswordCommandRequest>
    {
        public ChangePasswordCommandRequestValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.CurrentPassword).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).NotEmpty().NotNull();
        }
    }
}
