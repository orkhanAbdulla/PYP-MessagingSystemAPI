using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.TokenServices;
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
        private readonly ITokenService _tokenService;

        public ChangePasswordCommandHandler(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.Username);
            if (employee == null) throw new Exception("Username notfound");  // TODO: Burada NotFoundException  olacaq
            var success = !await _userService.ChekCurrentPasswordAsync(employee, request.CurrentPassword);
            if (success) throw new Exception("Please enter the current password correctly"); // TODO: Buraya fikirlesh
            var IsSame = _userService.CheckPasswordsIsSame(employee, request.NewPassword);
            if (IsSame) throw new Exception("Your new and old password cannot be the same"); // TODO: Buraya fikirlesh
            var result = await _userService.ChangePasswordAsync(employee, request.CurrentPassword, request.NewPassword);
            Token token = new Token();
            if (result.Successed)
            {
                token = _tokenService.GenerateAccessToken(employee, 5);
            }

            return new ChangePasswordCommandResponse() { Result = result, Token = token };

        }
    }
}
