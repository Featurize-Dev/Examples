using CommonFeatures.Aspire;
using CommonFeatures.HealthChecks;
using CommonFeatures.Storage;
using CommonFeatures.Telemetry;
using Featurize;
using Featurize.Repositories;
using Kafka;
using Ordering.Features.OpenApi;

namespace CommonFeatures;
public static class FeatureCollectionExtensions
{
    public static IFeatureCollection AddDefaultFeatures(this IFeatureCollection features)
    {
        return features
            .AddAspire()
            .AddKafka(options =>
            {
                options.BootstrapServers = "nas.home.lab:9092";
            })
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

    public static IFeatureCollection AddAspire(this IFeatureCollection features)
    {
        return features.Add(new AspireFeature());
    }
}
