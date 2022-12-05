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
    public class UpdateReplyCommandHandler : IRequestHandler<UpdateReplyCommandRequest,UpdateReplyCommandResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthService _authService;

        public UpdateReplyCommandHandler(IPostRepository postRepository, IAuthService authService)
        {
            _postRepository = postRepository;
            _authService = authService;
        }

        public async Task<UpdateReplyCommandResponse> Handle(UpdateReplyCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            Post Reply = await _postRepository.GetAsync(x => x.Id == request.Id && x.IsReply == true, true, nameof(Connection));
            if (Reply == null) throw new NotFoundException(nameof(Post), request.Id);
            var result = Reply.EmployeeId == employee.Id;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{Reply.Id}\"is not created by EmployeeId:\"{employee.Id}\"");
            result = Reply.Connection.Id != request.ConnectionId;
            if (result) throw new BadRequestException
                        ($"The PostId:\"{Reply.Id}\"is not created in ConnectionId:\"{request.ConnectionId}\"");
            Reply.Message = request.Message;
            Reply.IsEdited = true;
            _postRepository.Update(Reply);
            await _postRepository.SaveChangesAsync(cancellationToken);
            return new() { ReplyId = Reply.Id };
        }
    }
}
