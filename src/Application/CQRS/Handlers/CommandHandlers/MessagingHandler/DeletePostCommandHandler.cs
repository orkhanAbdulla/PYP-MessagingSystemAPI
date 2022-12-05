using MediatR;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
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
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IAuthService _authService;
        private readonly IStorage _storage;

        public DeletePostCommandHandler(IPostRepository postRepository, IStorage storage, IAttachmentRepository attachmentRepository)
        {
            _postRepository = postRepository;
            _storage = storage;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            Employee employee = await _authService.GetUserAuthAsync();
            Post post = await _postRepository.GetAsync(x => x.Id == request.Id, true, nameof(Connection));
            if (post == null) throw new NotFoundException(nameof(Post), request.Id);
            var result = post.EmployeeId == employee.Id;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created by EmployeeId:\"{employee.Id}\"");
            result = post.Connection.Id == request.ConnectionId;
            if (!result) throw new BadRequestException
                        ($"The PostId:\"{post.Id}\"is not created in ConnectionId:\"{request.ConnectionId}\"");
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
