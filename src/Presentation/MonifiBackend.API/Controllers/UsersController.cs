using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.UserModule.Application.Notifications.Commands.CreateNotification;
using MonifiBackend.UserModule.Application.Notifications.Queries.GetNotifications;
using MonifiBackend.UserModule.Application.Users.Commands.ConfirmUser;
using MonifiBackend.UserModule.Application.Users.Commands.RegistrationCompletion;
using MonifiBackend.UserModule.Application.Users.Queries.GetNetworkUsers;
using MonifiBackend.UserModule.Application.Users.Queries.GetUser;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.WalletModule.Application.AccountMovements.Queries.BscScanAddress;
using MonifiBackend.WalletModule.Application.AccountMovements.Queries.BscScanNormalTransaction;

namespace MonifiBackend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
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

            var request = new GetUserQuery(currentUser.Id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("notifications")]
        [Authorize(Role.Administrator, Role.Owner, Role.User)]
        public async Task<IActionResult> NotificationsAsync()
        {
            var currentUser = (User)HttpContext.Items["User"];

            var request = new GetNotificationsQuery(currentUser.Id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("notifications")]
        [Authorize(Role.Administrator, Role.Owner, Role.User)]
        public async Task<IActionResult> CreateNotificationsAsync(CreateNotificationCommand request)
        {
            var currentUser = (User)HttpContext.Items["User"];

            request.UserId = currentUser.Id;
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("network")]
        [Authorize(Role.Administrator, Role.Owner, Role.User)]
        public async Task<IActionResult> NetworkAsync()
        {
            var currentUser = (User)HttpContext.Items["User"];

            var request = new GetNetworkUsersQuery(currentUser.Id);
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
        [HttpPost("registration-completion")]
        [Authorize(Role.User)]
        public async Task<IActionResult> RegistrationCompletionAsync([FromBody] RegistrationCompletionCommand request)
        {
            var currentUser = (User)HttpContext.Items["User"];
            request.UserId = currentUser.Id;
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("BscScanAddress")]
        [AllowAnonymous]
        public async Task<IActionResult> BscScanAddressAsync([FromBody] BscScanAddressQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("BscScanNormalTransaction")]
        [AllowAnonymous]
        public async Task<IActionResult> BscScanNormalTransactionAsync([FromBody] BscScanNormalTransactionQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
