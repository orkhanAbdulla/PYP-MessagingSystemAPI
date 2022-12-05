using MediatR;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
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
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommandRequest, DeleteReplyCommandResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthService _authService;

        public DeleteReplyCommandHandler(IPostRepository postRepository, IAuthService authService)
        {
            _postRepository = postRepository;
            _authService = authService;
        }

        public async Task<DeleteReplyCommandResponse> Handle(DeleteReplyCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            Post reply = await _postRepository.GetAsync(x => x.Id == request.Id, true, nameof(Connection));
            if (reply == null) throw new NotFoundException(nameof(Post), request.Id);
            var result = reply.EmployeeId == employee.Id;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{reply.Id}\"is not created by EmployeeId:\"{employee.Id}\"");
            result = reply.Connection.Id == request.ConnectionId;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{reply.Id}\"is not created in ConnectionId:\"{request.ConnectionId}\"");
            _postRepository.Remove(reply);
            await _postRepository.SaveChangesAsync(cancellationToken);
            return new DeleteReplyCommandResponse() { Id = reply.Id };
        }
    }
}
