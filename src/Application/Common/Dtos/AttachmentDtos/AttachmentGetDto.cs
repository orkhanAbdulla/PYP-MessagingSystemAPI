using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Dtos.AttachmentDtos
{
    public class AttachmentGetDto
    {
        public string FileName { get; set; } = null!;
        public FileType FileType { get; set; }
        public string Path { get; set; } = null!;
    }
}
