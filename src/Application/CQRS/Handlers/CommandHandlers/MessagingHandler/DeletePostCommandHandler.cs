using MediatR;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
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
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEmployeeChannelRepository _employeeChannelRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IStorage _storage;

        public DeletePostCommandHandler(IConnectionRepository connectionRepository, IEmployeeChannelRepository employeeChannelRepository, IPostRepository postRepository, IStorage storage, IAttachmentRepository attachmentRepository)
        {
            _connectionRepository = connectionRepository;
            _employeeChannelRepository = employeeChannelRepository;
            _postRepository = postRepository;
            _storage = storage;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetAsync(x => x.Id == request.Id, true, nameof(Connection));
            if (post == null) throw new NotFoundException(nameof(Post), request.Id);
            if (post.EmployeeId!=request.EmployeeId) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created by EmployeeId:\"{request.EmployeeId}\"");
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
            IEnumerable<Attachment> attachments = await _attachmentRepository.GetAllAsync(x => x.PostId == post.Id);
            if (attachments!=null)
            {
                foreach (var att in attachments)
                {
                    await _storage.DeleteAsync("attachments",att.FileName);
                }
            }
            _postRepository.Remove(post);
            await _postRepository.SaveChangesAsync(cancellationToken);
            return new DeletePostCommandResponse() {Id=post.Id};
           
        }
    }
}
