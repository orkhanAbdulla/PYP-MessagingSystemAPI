using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.QueryHandlers.ConnectionHandler
{
    internal class GetChannelListByUserQueryHandler : IRequestHandler<GetChannelListByUserQueryRequest, IEnumerable<GetChannelListByUserQueryResponse>>
    {
        private readonly IAuthService  _authService;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IMapper _mapper;

        public GetChannelListByUserQueryHandler(IEmployeeChannelRepository employeeChannelRepository, IMapper mapper, IAuthService authService)
        {
            _employeeChannelRepository = employeeChannelRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IEnumerable<GetChannelListByUserQueryResponse>> Handle(GetChannelListByUserQueryRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            var employeeChannels = await _employeeChannelRepository.GetAllAsync(x => x.EmployeeId == employee.Id, true, "Channel");
            return _mapper.Map<IEnumerable<GetChannelListByUserQueryResponse>>(employeeChannels.Select(x => x.Channel).ToList());
        }
    }
}
