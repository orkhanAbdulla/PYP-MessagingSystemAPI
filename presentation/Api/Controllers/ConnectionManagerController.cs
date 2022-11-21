using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
