using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Queries.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.QueryHandlers.MessagingHandler
{
    public class GetPostByIdConnectionQueryHandler : IRequestHandler<GetPostByConnectionIdQueryRequest, IEnumerable<GetPostByConnectionIdQueryResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;

        public GetPostByIdConnectionQueryHandler(IPostRepository postRepository, IConnectionRepository connectionRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPostByConnectionIdQueryResponse>> Handle(GetPostByConnectionIdQueryRequest request, CancellationToken cancellationToken)
        {
            var IsExsist = await _connectionRepository.IsExistAsync(x => x.Id == request.ConnectionId);
            if (!IsExsist) throw new NotFoundException(nameof(Connection),request.ConnectionId);
            var posts = await _postRepository.GetPostByConnectionId(request.ConnectionId, request.RepliesCount, request.ReactionsCount);
            return _mapper.Map<IEnumerable<GetPostByConnectionIdQueryResponse>>(posts); ;
        }
    }
}
