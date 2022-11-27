using MessagingSystemApp.Application.Common.Dtos.ReactionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse
{
    public class GetRepliesByPostIdQueryResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public bool IsEdited { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public int ReplyPostId { get; set; }
        public ICollection<ReactionGetDto>? Reactions { get; set; }
    }
}
