using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.ConnectionResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.ConnectionHandler
{
    public class CreateConnectionCommandHandler : IRequestHandler<CreateConnectionCommandRequest, CreateConnectionCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;

        public CreateConnectionCommandHandler(IUserService userService, IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository)
        {
            _userService = userService;
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
        }

        async Task<CreateConnectionCommandResponse> IRequestHandler<CreateConnectionCommandRequest, CreateConnectionCommandResponse>.Handle(CreateConnectionCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.UserName);
            if (employee == null) throw new NotFoundException(nameof(Employee), request.UserName);
            if (request.IsChannel && request.ChannelName!=null)
            {
                bool result = await _employeeChannelRepository.IsExistChannelNameAsync(employee.Id, request.ChannelName);
                if (!result) throw new BadRequestException("The specified ChannelName already exists");
                Connection connection = new Connection()
                { IsChannel = true ,Name=request.ChannelName};
                await _connectionRepository.AddAsync(connection);
                await _connectionRepository.SaveChangesAsync(cancellationToken);
                EmployeeChannel employeeChannel = new EmployeeChannel() 
                { EmployeeId = employee.Id, ChannelId = connection.Id };
                await _employeeChannelRepository.AddAsync(employeeChannel);
                await _employeeChannelRepository.SaveChangesAsync(cancellationToken);
                return new CreateConnectionCommandResponse();
            }
            if (request.ReciverUserName!= null)
            {
                Employee reciver = await _userService.GetUserAsync(x => x.UserName == request.ReciverUserName);
                if (reciver == null) throw new NotFoundException(nameof(Employee), request.ReciverUserName);
                bool isDirectMessageExsist = await _connectionRepository.IsExistAsync(x => x.IsPrivate == true && x.SenderId == employee.Id && x.ReciverId==reciver.Id || x.SenderId==reciver.Id && x.ReciverId==employee.Id);
                if (isDirectMessageExsist) throw new BadRequestException($"Direct Message with {employee.UserName} and {reciver.UserName} already exists");
                Connection connection = new Connection()
                { IsPrivate = true,SenderId=employee.Id,ReciverId=reciver.Id};
                await _connectionRepository.AddAsync(connection);
                await _connectionRepository.SaveChangesAsync(cancellationToken);
                return new CreateConnectionCommandResponse();
            }
            throw new BadRequestException();
        }
    }
}
