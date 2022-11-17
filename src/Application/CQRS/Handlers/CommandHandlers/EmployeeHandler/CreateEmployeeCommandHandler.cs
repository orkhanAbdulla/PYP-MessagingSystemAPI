
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.MailServices;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.UserRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.EmployeeCommandHandler
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommandRequest, CreateEmployeeCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly IAuthService _authService;
        public CreateEmployeeCommandHandler(IUserService userService, IMailService mailService, IAuthService authService)
        {
            _userService = userService;
            _mailService = mailService;
            _authService = authService;
        }

        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = await _userService.CreateUserAsync(request.Username, request.Email, request.Password);
            if (!serviceResult.IdentityResult.Succeeded) throw new ValidationException(serviceResult.IdentityResult);
            string token = await _authService.GenerateEmailConfirmationTokenAsync(serviceResult.UserId);
            await _mailService.SendEmailConfirmationAsync(request.Email, serviceResult.UserId, token);
            return new() { UserId = serviceResult.UserId};
        }
    }
}
