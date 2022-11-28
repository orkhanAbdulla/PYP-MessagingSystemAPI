using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Channels;

namespace MessagingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConnectionManagerController : ApiBaseController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromForm]CreateConnectionCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] UpdateConnectionCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromForm] DeleteConnectionCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserToChannel([FromForm] AddUserToChannelCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetChannelListByUser([FromQuery] GetChannelListByUserQueryRequest query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult>GetDirectMessagesListByUser([FromQuery]GetDirectMessagesListByUserQueryRequest query)
        {
            return Ok(await Mediator.Send(query));
        }

    }
}
