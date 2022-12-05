using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
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
    public class GetDirectMessagesListByUserQueryHandler : IRequestHandler<GetDirectMessagesListByUserQueryRequest, IEnumerable<GetDirectMessagesListByUserQueryRespose>>
    {
        private readonly IAuthService _authService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;
        public GetDirectMessagesListByUserQueryHandler(IAuthService authService, IConnectionRepository connectionRepository, IMapper mapper)
        {
            _authService = authService;
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDirectMessagesListByUserQueryRespose>> Handle(GetDirectMessagesListByUserQueryRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            return _mapper.Map<IEnumerable<GetDirectMessagesListByUserQueryRespose>>(await _connectionRepository.
                GetAllAsync(x => x.SenderId == employee.Id || x.ReciverId == employee.Id, true, "Sender", "Reciver")); ;
        }
    }
}
