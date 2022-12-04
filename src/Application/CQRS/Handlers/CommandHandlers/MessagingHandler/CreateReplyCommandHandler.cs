using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Hubs;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.MessagingHandler
{
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommandRequest, CreateReplyCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IStorageService _storageService;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessagingHubService _messagingHubService;
        private readonly IMapper _mapper;

        public CreateReplyCommandHandler(IUserService userService, IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IStorageService storageService, IPostRepository postRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMessagingHubService messagingHubService)
        {
            _userService = userService;
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _storageService = storageService;
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _messagingHubService = messagingHubService;
        }

        public async Task<CreateReplyCommandResponse> Handle(CreateReplyCommandRequest request, CancellationToken cancellationToken)
        {
            string userName= _httpContextAccessor.HttpContext.User?.Identity?.Name ?? throw new BadRequestException();
            Employee employee = await _userService.GetUserAsync(x => x.UserName == userName);
            if (employee==null) throw new NotFoundException(nameof(Employee), userName);
            Post post = await _postRepository.GetAsync(x => x.Id == request.ReplyPostId, true, nameof(Connection));
            if (post == null)throw new NotFoundException(nameof(Post), request.ReplyPostId);
            var result = post.Connection.Id != request.ConnectionId;
            if (result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created in ConnectionId:\"{request.ConnectionId}\"");
            if (post.IsReply) throw new BadRequestException($"The PostId:\"{post.Id} is reply\"");
            Post ReplyPost = _mapper.Map<Post>(request);
            ReplyPost.EmployeeId=employee.Id;
            ReplyPost.IsReply = true;
            await _postRepository.AddAsync(ReplyPost);
            await _postRepository.SaveChangesAsync(cancellationToken);
            await _messagingHubService.CreateReplyInPost(post.Id, request.Message, userName);

            return new CreateReplyCommandResponse() { Id = ReplyPost.Id };
        }
    }
}
