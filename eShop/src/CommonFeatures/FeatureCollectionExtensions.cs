using CommonFeatures.HealthChecks;
using CommonFeatures.Storage;
using CommonFeatures.Telemetry;
using Featurize;
using Featurize.Repositories;
using Ordering.Features.OpenApi;

namespace CommonFeatures;
public static class FeatureCollectionExtensions
{
    public static IFeatureCollection AddDefaultFeatures(this IFeatureCollection features)
    {
        return features
            .AddOpenTelemetry()
            .AddHealthChecks()
            .AddRepositories()
            .AddStorage()
            .AddOpenApi();
    }

    public static IFeatureCollection AddOpenApi(this IFeatureCollection features)
    {
        return features.Add(new OpenApiFeature());
    }

    public static IFeatureCollection AddHealthChecks(this IFeatureCollection features)
    {
        return features.Add(new HealthChecksFeature());
    }

    public static IFeatureCollection AddOpenTelemetry(this IFeatureCollection features)
    {
        return features.Add(new OpenTelemetryFeature());
    }

    public static IFeatureCollection AddStorage(this IFeatureCollection features)
    {
        return features.Add(new StorageFeature());
    }
}
