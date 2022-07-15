namespace MonifiBackend.Core.Infrastructure.Environments
{
    public class ApplicationSettings
    {
        public Secret Secret { get; set; }
        public MssqlSettings MssqlSettings { get; set; }
        public EmailConfiguration EmailConfigurations { get; set; }
    }
}
