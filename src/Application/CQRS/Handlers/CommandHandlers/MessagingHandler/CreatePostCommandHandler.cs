using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Hubs;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Enums;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.MessagingHandler
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IStorageService _storageService;
        private readonly IPostRepository _postRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMessagingHubService _messagingHubService;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IAuthService authService, IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IStorageService storageService, IMapper mapper, IPostRepository postRepository, IAttachmentRepository attachmentRepository, IMessagingHubService messagingHubService)
        {
            _authService = authService;
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _storageService = storageService;
            _mapper = mapper;
            _postRepository = postRepository;
            _attachmentRepository = attachmentRepository;
            _messagingHubService = messagingHubService;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            Connection connection = await _connectionRepository.GetAsync(x => x.Id == request.ConnectionId);
            if (connection == null) throw new NotFoundException(nameof(Connection), request.ConnectionId);
            if (request.FormCollection == null && request.Message == null) throw new BadRequestException();
            var isExsistUserInConnection = true;
            if (connection.IsChannel == true)
            {
                isExsistUserInConnection = await _employeeChannelRepository.
                    IsExistAsync(x => x.ChannelId == request.ConnectionId && x.EmployeeId == employee.Id);
                if (!isExsistUserInConnection) throw new BadRequestException
                        ($"The EmployeeId:\"{employee.Id}\"is not in Connection:\"{request.ConnectionId}\"");
            }
            if (connection.IsPrivate == true)
            {
                isExsistUserInConnection = await _connectionRepository.
                 IsExistAsync(x => x.SenderId == employee.Id && x.ReciverId == employee.Id);
                if (isExsistUserInConnection) throw new BadRequestException
                        ($"The EmployeeId:\"{employee.Id}\"is not in the Connection:\"{request.ConnectionId}\"");
            }
            List<Attachment>? attachments=null;
            if (request.FormCollection != null)
            {
                List<AttachmentPostDto> attachmentDtos = new List<AttachmentPostDto>();
                foreach (var file in request.FormCollection)
                {
                    var isValidSize = _storageService.IsValidSize(file.Length, 1024);
                    if (!isValidSize) throw new FileFormatException("File must be less than 10 kb");
                    var fileType = _storageService.GetFileType(file.ContentType);
                    if (fileType == 0) throw new FileFormatException($"can't upload {file.ContentType} file");
                    var result = await _storageService.UploadAsync("attachments", file);
                    attachmentDtos.Add(new AttachmentPostDto { FileName = result.fileName, FileType = fileType, Path = result.path, });
                }
               attachments = _mapper.Map<List<Attachment>>(attachmentDtos);
            }
            Post post = _mapper.Map<Post>(request);
            post.EmployeeId = employee.Id;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync(cancellationToken);
            if (attachments!=null)
            {
                attachments.ForEach(x=>x.PostId=post.Id);
                await _attachmentRepository.AddRangeAsync(attachments);
                await _attachmentRepository.SaveChangesAsync(cancellationToken);
            }
            if (connection.IsChannel == true)
            {
                await _messagingHubService.CreatePostInChannelAsync(connection.Id, request.Message, employee.UserName);
            }
            else
            {
                await _messagingHubService.CreatePostInDirectlyMessage(connection, request.Message, employee.Id, employee.UserName);
            }

            return new CreatePostCommandResponse() { Id=post.Id};
        }
    }
}