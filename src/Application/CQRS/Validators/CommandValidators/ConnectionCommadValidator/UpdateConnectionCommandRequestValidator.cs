﻿using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.ConnectionCommadValidator
{
    public class UpdateConnectionCommandRequestValidator:AbstractValidator<UpdateConnectionCommandRequest>
    {
        public UpdateConnectionCommandRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x=>x.ChannelName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserName).NotEmpty().NotNull();
        }
    }
}
