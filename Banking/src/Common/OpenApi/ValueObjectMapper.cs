using Featurize.ValueObjects.Interfaces;
using Featurize.ValueObjects;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Common.OpenApi;

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
                Example = new OpenApiString(type.GetDefaultValue().ToString() ?? string.Empty)
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



        return Assembly.GetEntryAssembly()!
                       .GetTypes()
                       .Union(Assembly.GetExecutingAssembly().GetTypes())
                       .Where(t => t.GetInterfaces()
                                    .Any(i => i.IsGenericType &&
                                         i.GetGenericTypeDefinition().Equals(genericType)));
    }
}
