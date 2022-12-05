using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.ConnectionHandler
{
    public class AddUserToChannelCommandHandler : IRequestHandler<AddUserToChannelCommandRequest, AddUserToChannelCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;

        public AddUserToChannelCommandHandler(IUserService userService, IEmployeeChannelRepository employeeChannelRepository, IAuthService authService)
        {
            _userService = userService;
            _employeeChannelRepository = employeeChannelRepository;
            _authService = authService;
        }

        public async Task<AddUserToChannelCommandResponse> Handle(AddUserToChannelCommandRequest request, CancellationToken cancellationToken)
        {
            Employee CurrentEmployee = await _authService.GetUserAuthAsync();
            Employee AddedEmployee = await _userService.GetUserAsync(x => x.UserName == request.AddedUser);
            if (AddedEmployee==null) throw new NotFoundException(nameof(Employee), request.AddedUser);
            var IsExistCurrentEmployeeChannel = await _employeeChannelRepository.
                IsExistAsync(x => x.ChannelId == request.ConnectionId && x.EmployeeId == CurrentEmployee.Id);
            if (!IsExistCurrentEmployeeChannel) 
                throw new NotFoundException(nameof(Connection), $"Id:{request.ConnectionId}");
            var IsExistAddedEmployeeChannel = await _employeeChannelRepository.
                    IsExistAsync(x => x.ChannelId == request.ConnectionId && x.EmployeeId == AddedEmployee.Id);
            if (IsExistAddedEmployeeChannel) throw new BadRequestException
                    ($"The Employee:{AddedEmployee.UserName} already exist in ChannelId: {request.ConnectionId }");
            EmployeeChannel employeeChannel = new() { ChannelId = request.ConnectionId, EmployeeId = AddedEmployee.Id};
            await _employeeChannelRepository.AddAsync(employeeChannel);
            await _employeeChannelRepository.SaveChangesAsync(cancellationToken);
            return new AddUserToChannelCommandResponse();
        }
    }
}
