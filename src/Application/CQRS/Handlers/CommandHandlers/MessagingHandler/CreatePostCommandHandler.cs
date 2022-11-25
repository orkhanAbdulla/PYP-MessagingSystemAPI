using AutoMapper;
using MediatR;
using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Handlers.CommandHandlers.MessagingHandler
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest,ICollection<CreatePostCommandResponse>>
    {
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IStorageService _storageService;
        private readonly IPostRepository _postRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IUserService userService, IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IStorageService storageService, IMapper mapper, IPostRepository postRepository, IAttachmentRepository attachmentRepository)
        {
            _userService = userService;
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _storageService = storageService;
            _mapper = mapper;
            _postRepository = postRepository;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<ICollection<CreatePostCommandResponse>> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            Connection connection = await _connectionRepository.GetAsync(x => x.Id == request.ConnectionId);
            if (connection == null) throw new NotFoundException(nameof(Connection), request.ConnectionId);
            Employee employee = await _userService.GetUserAsync(x => x.UserName == request.UserName);
            if (employee == null) throw new NotFoundException(nameof(Employee), request.UserName);
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
                        ($"The EmployeeId:\"{employee.Id}\"is not in Connection:\"{request.ConnectionId}\"");
            }
            Post post = _mapper.Map<Post>(request);
            post.EmployeeId = employee.Id;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync(cancellationToken);
            // File Upload
            List<AttachmentDto> attachmentDto = new List<AttachmentDto>();
            if (request.FormCollection != null)
            {
                foreach (var file in request.FormCollection)
                {
                    var isValidSize = _storageService.IsValidSize(file.Length, 1024);
                    if (!isValidSize) throw new FileFormatException("File must be less than 10 kb");
                    var fileType = _storageService.GetFileType(file.ContentType);
                    if (fileType == 0) throw new FileFormatException($"can't upload {file.ContentType} file");
                    var result = await _storageService.UploadAsync("attachments", file);
                    attachmentDto.Add(new AttachmentDto { FileName = result.fileName, FileType = fileType, Path = result.path, PostId = post.Id });
                }
                IEnumerable<Attachment> attachments = _mapper.Map<IEnumerable<Attachment>>(attachmentDto);
                await _attachmentRepository.AddRangeAsync(attachments);
                await _attachmentRepository.SaveChangesAsync(cancellationToken);
            }
            // TODO: Duzelish edersen!!!
            List<CreatePostCommandResponse> CreatePostCommandResponse = new();
            foreach (var x in attachmentDto)
            {
                CreatePostCommandResponse.Add(new CreatePostCommandResponse()
                { FileName = x.FileName, Path = x.Path, PostId = x.PostId, Type = x.FileType.ToString() });
            }

            return CreatePostCommandResponse;
        }
    }
}
