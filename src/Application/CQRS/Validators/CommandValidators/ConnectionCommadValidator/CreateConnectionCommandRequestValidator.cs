using FluentValidation;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using MessagingSystemApp.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.ConnectionCommadValidator
{
    public class CreateConnectionCommandRequestValidator:AbstractValidator<CreateConnectionCommandRequest>
    {
        public CreateConnectionCommandRequestValidator()
        {
            RuleFor(x => x.ChannelName).MaximumLength(50);
        }
    }
}
