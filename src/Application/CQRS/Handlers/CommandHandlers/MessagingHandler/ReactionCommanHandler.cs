using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.Common.Helpers;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Enums;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.MessagingHandler
{
    public class ReactionCommanHandler : IRequestHandler<ReactionCommandRequest, ReactionCommandResponse>
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;

        public ReactionCommanHandler(IReactionRepository reactionRepository, IPostRepository postRepository, IUserService userService)
        {
            _reactionRepository = reactionRepository;
            _postRepository = postRepository;
            _userService = userService;
        }

        public async Task<ReactionCommandResponse> Handle(ReactionCommandRequest request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetAsync(x => x.Id == request.PostId, true, nameof(Connection));
            if (post == null) throw new NotFoundException(nameof(Post), request.PostId);
            bool isExsist = await _userService.IsExistAsync(x => x.Id == request.EmployeeId);
            if (!isExsist) throw new NotFoundException(nameof(Employee), request.EmployeeId);
            var result = post.Connection.Id != request.ConnectionId;
            if (result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created in ConnectionId:\"{request.ConnectionId}\"");
            var emoji=Helper.IsExsistEmoji(request.Emoji);
            if (emoji==0) throw new NotFoundException(nameof(emoji),nameof(emoji));
            Reaction reaction = new Reaction() { Emoji = emoji , PostId=post.Id};
            await _reactionRepository.AddAsync(reaction);
            await _reactionRepository.SaveChangesAsync(cancellationToken);
            return new ReactionCommandResponse();
        }
    }
}
