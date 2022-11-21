﻿using MediatR;
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
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.ConnectionHandler
{
    public class UpdateConnectionCommandHandler : IRequestHandler<UpdateConnectionCommandRequest, UpdateConnectionCommandResponse>
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IUserService _userService;

        public UpdateConnectionCommandHandler(IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IUserService userService)
        {
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _userService = userService;
        }

        public async Task<UpdateConnectionCommandResponse> Handle(UpdateConnectionCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.UserName);
            if (employee == null) throw new NotFoundException(nameof(Employee), request.UserName);
            // TODO:Connectionı yalnız o connectionı yaradan deyishe biler!!!
            var connection = await _connectionRepository.
                GetAsync(x => x.Id == request.Id);
            if (connection == null) throw new NotFoundException(nameof(Connection), request.Id);
            bool IsExistChanelName = await _employeeChannelRepository.
                 IsExistAsync(x => x.Channel.Name == request.ChannelName, x => x.EmployeeId == employee.Id 
                 && x.ChannelId!= connection.Id);
            if (IsExistChanelName) throw new BadRequestException($"ChannelName: \"{request.ChannelName}\" already exists");
            connection.Name=request.ChannelName;
            _connectionRepository.Update(connection);
            await _connectionRepository.SaveChangesAsync(cancellationToken);
            return new UpdateConnectionCommandResponse();
        }
    }
}
