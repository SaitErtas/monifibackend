using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.Core.Domain.Responses;
using MonifiBackend.PackageModule.Application.Packages.Commands.CreatePackage;
using Swashbuckle.AspNetCore.Annotations;

namespace MonifiBackend.API.Controllers;

//TODO: [Authorize]
[Route("api/[controller]")]
[SwaggerTag("Integration information with Auth transactions.")]
[ApiController]
public class PackagesController : BaseApiController
{
    private readonly IMediator _mediator;
    public PackagesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [SwaggerOperation(
    Summary = "Package",
    Description = "Create Package",
    OperationId = "Package",
    Tags = new[] { "Package" })]
    [SwaggerResponse(200, "Create Package Response", typeof(ResponseWrapper<CreatePackageCommandResponse>), "application/json")]
    [AllowAnonymous]
    [HttpPost("create-package")]
    public async Task<IActionResult> CreatePackageAsync([FromBody, SwaggerRequestBody("Login", Required = true)] CreatePackageCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
