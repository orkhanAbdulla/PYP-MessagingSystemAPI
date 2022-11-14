using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.EmployeeHandler
{
    public class VerifyForgetPasswordCommandHandler : IRequestHandler<VerifyForgetPasswordCommandRequest, VerifyForgetPasswordCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public VerifyForgetPasswordCommandHandler(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<VerifyForgetPasswordCommandResponse> Handle(VerifyForgetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.Id == request.UserId);
            if (employee==null) throw new Exception();  // TODO: Burada NotFoundException  olacaq
            bool state =await _authService.VerifyForgetPasswordAsync(employee, request.Token);
            return new VerifyForgetPasswordCommandResponse() { State = state };
        }
    }
}
