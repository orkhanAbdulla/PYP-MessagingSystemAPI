using MediatR;
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
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public RefreshTokenLoginCommandHandler(IAuthService authService, ITokenService tokenService, IUserService userService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee=await _authService.GetUserAuthAsync();
            if (employee.RefreshTokenEndDate < DateTime.Now) throw new ExpiredException();
            Token token = _tokenService.GenerateAccessToken(employee, 2);
            await _userService.UpdateRefreshToken(employee, token.RefreshToken, token.Expiration, 3);
            return new RefreshTokenLoginCommandResponse() { Token = token };
        }
    }
}
