using MediatR;
using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Application.CQRS.Commands.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest
{
    public class CreatePostCommandRequest:IRequest<CreatePostCommandResponse>,IMapFrom<Post>
    {
        public int ConnectionId { get; set; }
        public string Message { get; set; } = null!;
        public IFormFileCollection? FormCollection { get; set; }
    }
}
