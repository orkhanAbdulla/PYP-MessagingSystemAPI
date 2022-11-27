using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Dtos.PostDtos
{
    public class ReplyPostGetDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public bool IsEdited { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
