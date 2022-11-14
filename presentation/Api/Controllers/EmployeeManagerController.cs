using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using MessagingSystemApp.Application.CQRS.Commands.Request.UserRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagerController : ApiBaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] CreateEmployeeCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("EmployeeEmailConfirmation")]
        public async Task<IActionResult> EmployeeEmailConfirmation([FromForm]EmployeeEmailConfirmationCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginEmployeeCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromForm] ForgetPasswordCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("VerifyForgetPassword")]
        public async Task<IActionResult> VerifyForgetToken([FromForm] VerifyForgetPasswordCommandRequest command) 
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromForm] UpdatePasswordCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordCommandRequest command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
