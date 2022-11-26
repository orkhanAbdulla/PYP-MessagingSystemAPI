using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.MessagingHandler
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, UpdatePostCommandResponse>
    {
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetAsync(x => x.Id == request.Id &&x.IsReply==false,true,nameof(Connection));
            if (post == null) throw new NotFoundException(nameof(Post), request.Id);
            var result = post.EmployeeId == request.EmployeeId;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created by EmployeeId:\"{request.EmployeeId}\"");
            result = post.Connection.Id != request.ConnectionId;
            if (result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created in ConnectionId:\"{request.ConnectionId}\"");
            post.Message = request.Message;
            post.IsEdited = true;
            _postRepository.Update(post);
            await _postRepository.SaveChangesAsync(cancellationToken);
            return new UpdatePostCommandResponse() { PostId=post.Id};
        }
    }
}
