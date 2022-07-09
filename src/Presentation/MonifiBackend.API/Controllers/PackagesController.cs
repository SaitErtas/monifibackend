using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.PackageModule.Application.Packages.Commands.CreatePackage;
using MonifiBackend.PackageModule.Application.Packages.Commands.DeletePackage;
using MonifiBackend.PackageModule.Application.Packages.Commands.UpdatePackage;
using MonifiBackend.PackageModule.Application.Packages.Queries.GetPackages;
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetPackageAsync()
    {
        var request = new GetPackagesQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreatePackageAsync([FromBody] CreatePackageCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdatePackageAsync([FromRoute] int id, [FromBody] UpdatePackageCommand request)
    {
        request.Id = id;
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePackageAsync([FromRoute] int id)
    {
        var request = new DeletePackageCommand(id);
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
