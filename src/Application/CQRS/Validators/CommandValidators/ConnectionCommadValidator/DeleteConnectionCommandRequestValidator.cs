using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.ConnectionCommadValidator
{
    public class DeleteConnectionCommandRequestValidator:AbstractValidator<DeleteConnectionCommandRequest>
    {
        public DeleteConnectionCommandRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
