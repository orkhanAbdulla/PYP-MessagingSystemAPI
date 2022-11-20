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
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        public CreateConnectionCommandRequestValidator(IApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.ChannelName).MaximumLength(50);
        }
    }
}
