using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiBaseController
    {
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
