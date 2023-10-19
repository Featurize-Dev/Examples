using Featurize.ValueObjects.Converter;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace Featurize.Todo.Features.Serialization;

public class SerializationFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            o.SerializerOptions.Converters.Add(new ValueObjectJsonConverter());
        });
    }
}
