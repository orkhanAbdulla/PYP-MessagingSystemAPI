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
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IPostRepository postRepository)
        {
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _postRepository = postRepository;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetAsync(x => x.Id == request.Id,true,nameof(Connection));
            if (post == null) throw new NotFoundException(nameof(Post), request.Id);
            var isExsistPostByUser = await _postRepository.IsExistAsync(x => x.EmployeeId == request.EmployeeId);
            if (!isExsistPostByUser) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created by UserId:\"{request.Id}\"");
            var isExsistUserInConnection = true;
            if (post.Connection.IsChannel == true)
            {
                isExsistUserInConnection = await _employeeChannelRepository.
                    IsExistAsync(x => x.ChannelId == request.ConnectionId && x.EmployeeId == request.EmployeeId);
                if (!isExsistUserInConnection) throw new BadRequestException
                        ($"The EmployeeId:\"{request.EmployeeId}\"is not in Connection:\"{request.ConnectionId}\"");
            }
            if (post.Connection.IsPrivate == true)
            {
                isExsistUserInConnection = await _connectionRepository.
                 IsExistAsync(x => x.SenderId == request.EmployeeId && x.ReciverId == request.EmployeeId);
                if (isExsistUserInConnection) throw new BadRequestException
                        ($"The EmployeeId:\"{request.EmployeeId}\"is not in Connection:\"{request.ConnectionId}\"");
            }
            post.Message = request.Message;
            post.IsReply = true;
            _postRepository.Update(post);
            await _postRepository.SaveChangesAsync(cancellationToken);
            return new UpdatePostCommandResponse();
        }
    }
}
