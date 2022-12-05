using MediatR;
using MessagingSystemApp.Application.CQRS.Commands.Response.ConnectionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest
{
    public class UpdateConnectionCommandRequest:IRequest<UpdateConnectionCommandResponse>
    {
        public int Id { get; set; }
        public string ChannelName { get; set; } = null!;

    }
}
