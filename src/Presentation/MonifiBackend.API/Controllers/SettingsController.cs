using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.WalletModule.Application.Settings.Queries.GetMaintenanceMode;

namespace MonifiBackend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("maintenance-mode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMaintenanceModeAsync()
        {
            var request = new GetMaintenanceModeQuery();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
