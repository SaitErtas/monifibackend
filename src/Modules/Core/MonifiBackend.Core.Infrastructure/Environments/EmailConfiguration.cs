﻿namespace MonifiBackend.Core.Infrastructure.Environments;

public class EmailConfiguration
{
    public string DevelopmentTo { get; set; }
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
