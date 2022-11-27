using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Application.Common.Dtos.PostDtos;
using MessagingSystemApp.Application.Common.Dtos.ReactionDtos;
using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse
{
    public class GetPostByConnectionIdQueryResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string IsReply { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public ICollection<AttachmentGetDto>? Attachments { get; set; }
        public ICollection<ReplyPostGetDto>? ReplyPosts { get; set; }
        public ICollection<ReactionGetDto>? Reactions { get; set; }

    }


}
