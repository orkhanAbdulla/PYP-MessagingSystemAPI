using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
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
    public class GetDirectMessagesListByUserQueryHandler : IRequestHandler<GetDirectMessagesListByUserQueryRequest, IEnumerable<GetDirectMessagesListByUserQueryRespose>>
    {
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;

        public GetDirectMessagesListByUserQueryHandler(IUserService userService, IConnectionRepository connectionRepository, IMapper mapper)
        {
            _userService = userService;
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDirectMessagesListByUserQueryRespose>> Handle(GetDirectMessagesListByUserQueryRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.UserName);
            if (employee == null) throw new NotFoundException(nameof(Employee),request.UserName);
            return _mapper.Map<IEnumerable<GetDirectMessagesListByUserQueryRespose>>(await _connectionRepository.
                GetAllAsync(x => x.SenderId == employee.Id || x.ReciverId == employee.Id));
        }
    }
}
