using Featurize.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFeatures.HealthChecks;
public class HealthChecksFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
    }

    public void Use(WebApplication app)
    {
        app.MapHealthChecks("/health");
        app.MapHealthChecks("/liveness", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live")
        });
    }
}
