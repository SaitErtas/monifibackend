using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.UserModule.Application.Users.Commands.ConfirmUser;
using MonifiBackend.UserModule.Application.Users.Queries.UserData;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        [Authorize(Role.Administrator, Role.Owner, Role.User)]
        public async Task<IActionResult> MeAsync()
        {
            var currentUser = (User)HttpContext.Items["User"];

            var request = new UserQuery(currentUser.Id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("confirm/{confirmationCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmAsync(string confirmationCode)
        {
            var request = new ConfirmUserCommand(confirmationCode);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
