using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.PostCommadValidator
{
    public class CreatePostCommandRequestValidator:AbstractValidator<CreatePostCommandRequest>
    {
        public CreatePostCommandRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.ConnectionId).NotNull().NotEmpty();
        }
    }
}
