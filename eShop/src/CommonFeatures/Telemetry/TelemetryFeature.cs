using Featurize;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using System.Reflection;

namespace CommonFeatures.Telemetry;
public class OpenTelemetryFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(builder => {
                builder.AddService(serviceName: Assembly.GetEntryAssembly()?.GetName()?.Name ?? string.Empty);
            }).WithMetrics(builder =>
            {
                builder.AddAspNetCoreInstrumentation();
                builder.AddConsoleExporter();
            });
    }
}
