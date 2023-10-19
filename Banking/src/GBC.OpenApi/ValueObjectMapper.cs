using Featurize.ValueObjects.Interfaces;
using Featurize.ValueObjects;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GBC.OpenApi;

public static class ValueObjectMapper
{
    public static void MapValueObjects(this SwaggerGenOptions options)
    {
        var types = GetAllTypes(typeof(IValueObject<>)).Union(GetAllTypes());

        foreach (var type in types)
        {
            options.MapType(type, () => new OpenApiSchema()
            {
                Type = "string",
            });
        }

    }

    private static IEnumerable<Type> GetAllTypes()
    {
        return typeof(Country).Assembly
                       .GetTypes()
                       .Where(t => t.GetInterfaces()
                                    .Any(i => i.IsGenericType &&
                                         i.GetGenericTypeDefinition().Equals(typeof(IValueObject<>))));
    }

    private static IEnumerable<Type> GetAllTypes(Type genericType)
    {
        if (!genericType.IsGenericTypeDefinition)
            throw new ArgumentException("Specified type must be a generic type definition.", nameof(genericType));

        return Assembly.GetExecutingAssembly()
                       .GetTypes()
                       .Where(t => t.GetInterfaces()
                                    .Any(i => i.IsGenericType &&
                                         i.GetGenericTypeDefinition().Equals(genericType)));
    }
}
