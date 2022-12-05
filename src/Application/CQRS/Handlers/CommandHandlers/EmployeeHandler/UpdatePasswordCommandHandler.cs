using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.EmployeeHandler
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService  _authService;


        public UpdatePasswordCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            IdentityResult result = await _userService.UpdatePasswordAsync(employee, request.Token, request.Password);
            if (!result.Succeeded) throw new ValidationException(result);
            return new UpdatePasswordCommandResponse() { };
        }
    }
}
