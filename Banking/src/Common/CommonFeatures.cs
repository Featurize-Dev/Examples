using Common.Aspire;
using Common.HealthChecks;
using Common.OpenApi;
using Common.Serialization;
using Common.Storage;
using Common.Telemetry;
using Featurize;

namespace Common;

public static class CommonFeatures
{
    public static IFeatureCollection AddCommonFeatures(this IFeatureCollection features)
    {
        features
            .AddAspire()
            .AddHealthChecks()
            .AddOpenTelemetry()
            .AddSerializaion()
            .AddOpenApi()
            .AddStorage();
        return features;
    }

    public static IFeatureCollection AddOpenApi(this IFeatureCollection features)
    {
        features.Add<OpenApiFeature>();
        return features;
    }

    public static IFeatureCollection AddStorage(this IFeatureCollection features)
    {
        features.Add<StorageFeature>();
        return features;
    }

    public static IFeatureCollection AddSerializaion(this IFeatureCollection features)
    {
        features.Add<SerializationFeature>();
        return features;
    }

    public static IFeatureCollection AddHealthChecks(this IFeatureCollection features)
    {
        features.Add<HealthChecksFeature>();
        return features;
    }

    public static IFeatureCollection AddOpenTelemetry(this IFeatureCollection features)
    {
        features.Add<OpenTelemetryFeature>();
        return features;
    }

    public static IFeatureCollection AddAspire(this IFeatureCollection features)
    {
        features.Add<AspireFeature>();
        return features;
    }
}
