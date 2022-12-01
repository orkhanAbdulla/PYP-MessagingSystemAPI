using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse
{
    public class GetDirectMessagesListByUserQueryRespose
    {
        public int Id { get; set; }
        public string? SenderName { get; set; }
        public string? ReciverName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
