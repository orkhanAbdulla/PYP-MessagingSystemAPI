using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Queries.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.QueryHandlers.MessagingHandler
{
    public class GetRepliesByPostIdQueryHandler : IRequestHandler<GetRepliesByPostIdQueryRequest, IEnumerable<GetRepliesByPostIdQueryResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetRepliesByPostIdQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetRepliesByPostIdQueryResponse>> Handle(GetRepliesByPostIdQueryRequest request, CancellationToken cancellationToken)
        {
            var IsExsist = await _postRepository.IsExistAsync(x=>x.Id==request.PostId && x.IsReply==false);
            if (!IsExsist) throw new NotFoundException(nameof(Post), request.PostId);
            var replies = await _postRepository.GetRepliesByPostId(request.PostId,request.ReactionsCount);
            return _mapper.Map<IEnumerable<GetRepliesByPostIdQueryResponse>>(replies);
        }
    }
}
