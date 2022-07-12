using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.UserModule.Application.Wallets.Queries.GetNetworks;

namespace MonifiBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletsController : BaseApiController
{
    private readonly IMediator _mediator;
    public WalletsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("networks")]
    public async Task<IActionResult> GetNetworksAsync()
    {
        var request = new GetNetworksQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
