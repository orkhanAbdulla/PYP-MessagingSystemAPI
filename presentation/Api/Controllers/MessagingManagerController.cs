using MessagingSystemApp.Application.CQRS.Commands.Request.MessagingRequest;
using MessagingSystemApp.Application.CQRS.Queries.Request.MessagingRequest;
using MessagingSystemApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MessagingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingManagerController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPostsByConnectionId([FromQuery] GetPostByConnectionIdQueryRequest query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRepliesByPostId([FromQuery] GetRepliesByPostIdQueryRequest query)
        {
            return Ok(await Mediator.Send(query));
        }
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
        [HttpDelete("Post/[action]")]
        public async Task<IActionResult> Delete([FromForm] DeletePostCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Post/[action]")]
        public async Task<IActionResult> CreateReply([FromForm] CreateReplyCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("Post/[action]")]
        public async Task<IActionResult> UpdateReply([FromForm] UpdateReplyCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("Post/[action]")]
        public async Task<IActionResult> DeleteReply([FromForm] DeleteReplyCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Post/[action]")]
        public async Task<IActionResult> Reaction([FromForm] ReactionCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
