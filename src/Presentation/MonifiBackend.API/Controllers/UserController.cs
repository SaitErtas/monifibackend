using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.Core.Domain.Responses;
using MonifiBackend.UserModule.Application.Users.Queries.UserData;
using Swashbuckle.AspNetCore.Annotations;

namespace MonifiBackend.API.Controllers
{
    public class UserController : BaseApiController
    {

        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


    }
}
