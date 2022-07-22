using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MonifiBackend.Core.Domain.Notifications;
using MonifiBackend.Core.Infrastructure.Environments;
using System.Security.Authentication;

namespace MonifiBackend.Core.Infrastructure.Notifications;

public class YandexEmailAdapter : IEmailPort
{
    private readonly ApplicationSettings _appSettings;
    private readonly IHostingEnvironment _hostingEnvironment;

    public YandexEmailAdapter(IOptions<ApplicationSettings> appSettings, IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
        _appSettings = appSettings.Value;
    }

    public void Send(string to, string subject, string html, string from = null)
    {
        if (!_hostingEnvironment.IsProduction())
            to = _appSettings.EmailConfigurations.DevelopmentTo;
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailConfigurations.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;

        smtp.Connect(_appSettings.EmailConfigurations.SmtpServer, _appSettings.EmailConfigurations.Port, true);
        smtp.Authenticate(_appSettings.EmailConfigurations.UserName, _appSettings.EmailConfigurations.Password);
        smtp.CheckCertificateRevocation = false;
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
