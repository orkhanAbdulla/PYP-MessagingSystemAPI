using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
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
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDirectMessagesListByUserQueryHandler(IUserService userService, IConnectionRepository connectionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _connectionRepository = connectionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            
        }

        public async Task<IEnumerable<GetDirectMessagesListByUserQueryRespose>> Handle(GetDirectMessagesListByUserQueryRequest request, CancellationToken cancellationToken)
        {
            string userName = _httpContextAccessor.HttpContext.User?.Identity?.Name ?? throw new BadRequestException();
            Employee employee = await _userService.GetUserAsync(x => x.UserName == userName);
            if (employee == null) throw new NotFoundException(nameof(Employee),userName);
            return _mapper.Map<IEnumerable<GetDirectMessagesListByUserQueryRespose>>(await _connectionRepository.
                GetAllAsync(x => x.SenderId == employee.Id || x.ReciverId == employee.Id, true, "Sender", "Reciver")); ;
        }
    }
}
