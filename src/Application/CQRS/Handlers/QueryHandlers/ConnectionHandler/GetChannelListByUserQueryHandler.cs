using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.QueryHandlers.ConnectionHandler
{
    internal class GetChannelListByUserQueryHandler : IRequestHandler<GetChannelListByUserQueryRequest, IEnumerable<GetChannelListByUserQueryResponse>>
    {
        private readonly IUserService _userService;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IMapper _mapper;

        public GetChannelListByUserQueryHandler(IUserService userService, IEmployeeChannelRepository employeeChannelRepository, IMapper mapper)
        {
            _userService = userService;
            _employeeChannelRepository = employeeChannelRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetChannelListByUserQueryResponse>> Handle(GetChannelListByUserQueryRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.UserName);
            if (employee == null) throw new NotFoundException(nameof(Employee), request.UserName);
            var employeeChannels = await _employeeChannelRepository.GetAllAsync(x => x.EmployeeId == employee.Id, true, "Channel");
            return _mapper.Map<IEnumerable<GetChannelListByUserQueryResponse>>(employeeChannels.Select(x => x.Channel).ToList());
        }
    }
}
