﻿using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.PostCommadValidator
{
    public class UpdatePostCommandRequestValidator:AbstractValidator<UpdatePostCommandRequest>
    {
        public UpdatePostCommandRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.ConnectionId).NotNull().NotEmpty();
            RuleFor(x => x.Message).NotEmpty().NotNull();
        }
    }
}
