using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.ConnectionHandler
{
    public class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommandRequest, int>
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IAuthService _authService;
        private readonly IApplicationDbContext _applicationDbContext;
        public DeleteConnectionCommandHandler(IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IAuthService authService, IApplicationDbContext applicationDbContext)
        {
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _authService = authService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Handle(DeleteConnectionCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            if (request.IsChannel)
            {
                bool isExsistChanel = await _connectionRepository.IsExistAsync(x => x.Id == request.Id);
                if (!isExsistChanel) throw new NotFoundException(nameof(Connection), $"Id:{request.Id}");
                EmployeeChannel employeeChannel = await _employeeChannelRepository.
                    GetAsync(x => x.ChannelId == request.Id && x.EmployeeId == employee.Id &&  x.Channel.CreatedBy == employee.UserName, true,"Channel");
                if (employeeChannel == null) throw new UnauthorizedAccessException($"this channel was not created by {employee.UserName}");
                _connectionRepository.Remove(employeeChannel.Channel);
                await _connectionRepository.SaveChangesAsync(cancellationToken);
                return request.Id;
            }
            Connection connection = await _connectionRepository.
                GetAsync(x => x.Id==request.Id && x.ReciverId == employee.Id || x.SenderId == employee.Id);
            if (connection == null) throw new NotFoundException(nameof(Connection),$"Id:{request.Id}");
            _connectionRepository.Remove(connection);
            await _connectionRepository.SaveChangesAsync(cancellationToken);
            return connection.Id;
        }
    }
}
