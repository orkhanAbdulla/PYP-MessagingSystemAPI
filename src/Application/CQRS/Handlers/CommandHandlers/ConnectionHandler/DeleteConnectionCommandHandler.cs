using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
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
        private readonly IUserService _userService;
        private readonly IApplicationDbContext _applicationDbContext;
        public DeleteConnectionCommandHandler(IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IUserService userService, IApplicationDbContext applicationDbContext)
        {
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _userService = userService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Handle(DeleteConnectionCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.UserName);
            if (employee == null) throw new NotFoundException(nameof(Employee), request.UserName);
            if (request.IsChannel)
            {
                // TODO: Only remove that emoloyee created this connection
                EmployeeChannel employeeChannel = await _employeeChannelRepository.
                    GetAsync(x => x.ChannelId == request.Id && x.EmployeeId == employee.Id, true,"Channel");
                if (employeeChannel==null) throw new NotFoundException(nameof(Connection), $"Id:{request.Id}");
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
