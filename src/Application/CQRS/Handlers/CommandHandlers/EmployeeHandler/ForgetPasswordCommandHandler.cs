using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.MailServices;
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
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommandRequest, ForgetPasswordCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;

        public ForgetPasswordCommandHandler(IUserService userService, IAuthService authService, IMailService mailService)
        {
            _userService = userService;
            _authService = authService;
            _mailService = mailService;
        }

        public async Task<ForgetPasswordCommandResponse> Handle(ForgetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.Email == request.Email);
            if (employee!=null)
            {
                string token=await _authService.ForgetPassawordAsync(employee);
                await _mailService.SendForgetPasswordMailAsync(request.Email,employee.Id,token);
            }
            return new();
        }
    }
}
