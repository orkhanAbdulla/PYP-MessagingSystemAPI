using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Request.UserRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.EmployeeHandler
{
    public class EmployeeEmailConfirimationCommandHandler : IRequestHandler<EmployeeEmailConfirmationCommandRequest, EmployeeEmailConfirmationCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public EmployeeEmailConfirimationCommandHandler(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public async Task<EmployeeEmailConfirmationCommandResponse> Handle(EmployeeEmailConfirmationCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.Id == request.UserId);
            if (employee==null)  throw new NotFoundException(nameof(Employee),request.UserId);
            IdentityResult result = await _authService.VerifyEmailConfirmationToken(employee, request.Token);
            if (!result.Succeeded)
            {
                throw new ValidationException(result);
            }
            return new EmployeeEmailConfirmationCommandResponse() { Successed=true};
        }

    }
}
