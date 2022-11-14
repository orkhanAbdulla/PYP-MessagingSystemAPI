using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
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
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.Id == request.UserId);
            if (employee == null) throw new Exception();  // TODO: Burada NotFoundException  olacaq
            Result result = await _userService.UpdatePasswordAsync(employee, request.Token, request.Password);
            if (!result.Successed)
            {
                throw new Exception(); // TODO: Burada ValidationException  olacaq
            }
            return new UpdatePasswordCommandResponse() { Result=result };
            
        }
    }
}
