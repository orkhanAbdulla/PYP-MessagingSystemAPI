using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Dtos.AttachmentDtos
{
    public class AttachmentDto: IMapFrom<Attachment>
    {
        public string FileName { get; set; } = null!;
        public FileType FileType { get; set; }
        public string Path { get; set; } = null!;
        public int PostId { get; set; }
    }
}
