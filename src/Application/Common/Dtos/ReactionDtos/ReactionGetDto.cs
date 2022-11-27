using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Dtos.ReactionDtos
{
    public class ReactionGetDto
    {
        public string? Emoji { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
