namespace MonifiBackend.Core.Infrastructure.Environments
{
    public class ApplicationSettings
    {
        public Secret Secret { get; set; }
        public MssqlSettings MssqlSettings { get; set; }
    }
}
