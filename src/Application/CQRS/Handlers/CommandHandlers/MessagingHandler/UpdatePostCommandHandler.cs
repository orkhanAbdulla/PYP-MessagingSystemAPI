using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
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
        private readonly IAuthService _authService;

        public UpdatePostCommandHandler(IPostRepository postRepository, IAuthService authService = null)
        {
            _postRepository = postRepository;
            _authService = authService;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            Post post = await _postRepository.GetAsync(x => x.Id == request.Id &&x.IsReply==false,true,nameof(Connection));
            if (post == null) throw new NotFoundException(nameof(Post), request.Id);
            var result = post.EmployeeId == employee.Id;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created by EmployeeId:\"{employee.Id}\"");
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
