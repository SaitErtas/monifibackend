using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonifiBackend.API.Authorization;
using MonifiBackend.API.Controllers.Base;
using MonifiBackend.UserModule.Application.Wallets.Queries.GetNetworks;
using MonifiBackend.UserModule.Domain.Users;
using MonifiBackend.WalletModule.Application.AccountMovements.Commands.BuyMonofi;
using MonifiBackend.WalletModule.Application.AccountMovements.Commands.DeleteAccountMovement;
using MonifiBackend.WalletModule.Application.AccountMovements.Events.UserPaymentVerification;
using MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetAccountMovements;
using MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetPurchasedMovements;
using MonifiBackend.WalletModule.Application.Statistics.Queries.GetDaySaleStatistics;
using MonifiBackend.WalletModule.Application.Statistics.Queries.GetStatistic;

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
    [HttpPost("BuyMonifi")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> BuyMonifiAsync([FromBody] BuyMonofiCommand request)
    {
        var currentUser = (User)HttpContext.Items["User"];
        request.UserId = currentUser.Id;
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("account-movements")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> GetAccountMovementsAsync()
    {
        var currentUser = (User)HttpContext.Items["User"];

        var request = new GetAccountMovementsQuery(currentUser.Id);
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    [HttpGet("purchased-movements")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> GetPurchasedMovementsAsync()
    {
        var currentUser = (User)HttpContext.Items["User"];

        var request = new GetPurchasedMovementsQuery(currentUser.Id);
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("networks")]
    public async Task<IActionResult> GetNetworksAsync()
    {
        var request = new GetNetworksQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("statistics")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> GetStatisticsAsync()
    {
        var currentUser = (User)HttpContext.Items["User"];

        var request = new GetStatisticsQuery(currentUser.Id);
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("day-sale-statistics")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> GetDaySaleStatisticsAsync()
    {
        var request = new GetDaySaleStatisticsQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }


    [HttpGet("user-payment-sync")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> UserPaymentSyncAsync()
    {
        var currentUser = (User)HttpContext.Items["User"];

        var request = new UserPaymentVerificationEvent(currentUser.Id);
        await _mediator.Publish(request);
        return Ok();
    }

    [HttpDelete("{accountMovementId}")]
    [Authorize(Role.Administrator, Role.Owner, Role.User)]
    public async Task<IActionResult> DeleteAccountMovementAsync(int accountMovementId)
    {
        var currentUser = (User)HttpContext.Items["User"];

        var request = new DeleteAccountMovementCommand(currentUser.Id, accountMovementId);
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
