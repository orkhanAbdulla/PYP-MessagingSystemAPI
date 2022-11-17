using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.TokenServices;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.EmployeeCommandHandler
{
    public class LoginEmployeeCommandHandler : IRequestHandler<LoginEmployeeCommandRequest, LoginEmployeeCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public LoginEmployeeCommandHandler(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        public async Task<LoginEmployeeCommandResponse> Handle(LoginEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.Email == request.Email);
            if (employee is null) throw new UnauthorizedAccessException("The email or password is incorrect");
            var result =await _userService.CheckPasswordSignInAsync(employee, request.Password);
            if (result.IsNotAllowed) throw new UnauthorizedAccessException("Please verify your email");
            if (!result.Succeeded) throw new UnauthorizedAccessException("The email or password is incorrect");
            var token = _tokenService.GenerateAccessToken(employee, 3);
            await _userService.UpdateRefreshToken(employee, token.RefreshToken, token.Expiration, 2);
            return new() { Token = token };
        }
    }
}
