using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.Core.Domain.Responses;
using MonifiBackend.UserModule.Application.Users.Commands.ChangedPassword;
using MonifiBackend.UserModule.Application.Users.Commands.RegisterUser;
using MonifiBackend.UserModule.Application.Users.Commands.ResetPassword;
using MonifiBackend.UserModule.Application.Users.Queries.AuthenticateUser;
using Swashbuckle.AspNetCore.Annotations;

namespace MonifiBackend.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[SwaggerTag("Integration information with Auth transactions.")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(
    Summary = "Login",
    Description = "Basic user login",
    OperationId = "Login",
    Tags = new[] { "Auth" })]
    [SwaggerResponse(200, "Basic user login", typeof(ResponseWrapper<AuthenticateUserQueryResponse>), "application/json")]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody, SwaggerRequestBody("Login", Required = true)] AuthenticateUserQuery request)
    {
        request.SetIpAddress(Request.HttpContext.Connection.RemoteIpAddress.ToString());

        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [SwaggerOperation(
    Summary = "Signup",
    Description = "Basic user registeration",
    OperationId = "Register",
    Tags = new[] { "Auth" })]
    [SwaggerResponse(200, "Basic user registeration", typeof(ResponseWrapper<RegisterUserCommandResponse>), "application/json")]
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignupAsync([FromBody, SwaggerRequestBody("Signup", Required = true)] RegisterUserCommand request)
    {
        request.SetIpAddress(Request.HttpContext.Connection.RemoteIpAddress.ToString());
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [SwaggerOperation(
    Summary = "Reset Password",
    Description = "User reset password",
    OperationId = "ResetPassword",
    Tags = new[] { "Auth" })]
    [SwaggerResponse(200, "User reset password", typeof(ResponseWrapper<ResetPasswordCommandResponse>), "application/json")]
    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody, SwaggerRequestBody("ResetPassword", Required = true)] ResetPasswordCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [SwaggerOperation(
    Summary = "Reset Password Confirm",
    Description = "User reset password confirm",
    OperationId = "ResetPasswordConfirm",
    Tags = new[] { "Auth" })]
    [SwaggerResponse(200, "User reset password confirm", typeof(ResponseWrapper<ChangedPasswordCommandResponse>), "application/json")]
    [AllowAnonymous]
    [HttpPost("reset-password-confirm")]
    public async Task<IActionResult> ResetPasswordConfirmAsync([FromBody, SwaggerRequestBody("ResetPasswordConfirm", Required = true)] ChangedPasswordCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
