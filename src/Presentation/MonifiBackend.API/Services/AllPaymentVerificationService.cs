using MediatR;
using MonifiBackend.WalletModule.Application.AccountMovements.Events.AllPaymentVerification;
using Sgbj.Cron;

namespace MonifiBackend.API.Services;

public class AllPaymentVerificationService : BackgroundService
{
    private readonly ILogger<AllPaymentVerificationService> _logger;
    private readonly IMediator _mediator;
    public AllPaymentVerificationService(ILogger<AllPaymentVerificationService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // At minute 0
        using var timer = new CronTimer("*/5 * * * *");
        try
        {
            while (await timer.WaitForNextTickAsync())
            {
                _logger.LogInformation($"Worker running at: {DateTime.Now}");
                var request = new AllPaymentVerificationEvent();
                await _mediator.Publish(request);
            }
        }
        catch (Exception e)
        {

        }
        await Task.CompletedTask;
    }
}
