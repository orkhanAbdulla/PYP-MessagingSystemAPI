using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.ConnectionCommadValidator
{
    public class AddUserToChannelCommandRequestValidator:AbstractValidator<AddUserToChannelCommandRequest>
    {
        public AddUserToChannelCommandRequestValidator()
        {
            RuleFor(x => x.ConnectionId).NotNull().NotEmpty();
            RuleFor(x=>x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.AddedUser).NotNull().NotEmpty();
        }
    }
}
