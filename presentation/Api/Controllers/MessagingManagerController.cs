using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingManagerController : ApiBaseController
    {
        [HttpPost("Post/[action]")]
        public async Task<IActionResult> Create([FromForm] CreatePostCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("Post/[action]")]
        public async Task<IActionResult> Update([FromForm] UpdatePostCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("Posr/[action]")]
        public async Task<IActionResult> Delete([FromForm] DeletePostCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
