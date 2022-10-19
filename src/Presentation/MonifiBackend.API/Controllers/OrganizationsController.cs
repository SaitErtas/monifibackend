using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.UserModule.Application.Users.Commands.ConfirmUser;
using MonifiBackend.UserModule.Application.Users.Queries.GetOrganizationalCharts;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Role.Administrator)]
        [HttpGet("GetOrganizationalCharts/{userId}")]
        public async Task<IActionResult> GetOrganizationalCharts(int userId)
        {
            var request = new GetOrganizationalChartQuery(userId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
