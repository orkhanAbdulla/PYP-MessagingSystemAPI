using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Enums;


namespace MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse
{
    public class CreatePostCommandResponse:IMapFrom<Post>
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public ICollection<AttachmentGetDto>? AttachmentGetDtos { get; set; }
    }
}
