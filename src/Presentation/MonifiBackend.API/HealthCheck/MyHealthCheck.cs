using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace MonifiBackend.API.HealthCheck
{
    public class MyHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var catUrl = "https://localhost:7161/api/Health";

            var client = new HttpClient();

            client.BaseAddress = new Uri(catUrl);

            HttpResponseMessage response = await client.GetAsync("");

            return response.StatusCode == HttpStatusCode.OK ?
                await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "The API is healthy")) :
                await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Unhealthy,
                      description: "The API is sick"));
        }
    }
}
