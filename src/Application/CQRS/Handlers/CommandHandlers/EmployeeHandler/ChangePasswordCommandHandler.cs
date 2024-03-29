﻿using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.TokenServices;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.EmployeeHandler
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, ChangePasswordCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService authService;
        private readonly ITokenService _tokenService;

        public ChangePasswordCommandHandler(IUserService userService, ITokenService tokenService, IAuthService authService)
        {
            _userService = userService;
            _tokenService = tokenService;
            this.authService = authService;
        }

        public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await authService.GetUserAuthAsync();
            var success = !await _userService.ChekCurrentPasswordAsync(employee, request.CurrentPassword);
            if (success) throw new BadRequestException("Please enter the current password correctly");
            var IsSame = _userService.CheckPasswordsIsSame(employee, request.NewPassword);
            if (IsSame) throw new BadRequestException("Your new and old password cannot be the same");
            var result = await _userService.ChangePasswordAsync(employee, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded) throw new ValidationException(result);
            var token = _tokenService.GenerateAccessToken(employee, 5);
            return new ChangePasswordCommandResponse() { Token=token };
        }
    }
}
