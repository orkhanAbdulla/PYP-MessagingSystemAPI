using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse
{
    public class GetChannelListByUserQueryResponse:IMapFrom<Connection>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
