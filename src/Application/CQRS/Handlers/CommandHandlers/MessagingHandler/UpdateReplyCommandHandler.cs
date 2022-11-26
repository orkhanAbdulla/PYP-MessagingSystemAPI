using MediatR;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
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

        public UpdateReplyCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<UpdateReplyCommandResponse> Handle(UpdateReplyCommandRequest request, CancellationToken cancellationToken)
        {
            Post Reply = await _postRepository.GetAsync(x => x.Id == request.Id && x.IsReply == true, true, nameof(Connection));
            if (Reply == null) throw new NotFoundException(nameof(Post), request.Id);
            var result = Reply.EmployeeId == request.EmployeeId;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{Reply.Id}\"is not created by EmployeeId:\"{request.EmployeeId}\"");
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
