using Featurize.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ordering.Features.OpenApi.Filters;
using System.Reflection;

namespace Ordering.Features.OpenApi;

public class OpenApiFeature : IWebApplicationFeature
{
    private readonly Assembly _assembly = Assembly.GetEntryAssembly()!;

    public void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var assemblyName = _assembly.GetName();
            OpenApiInfo apiinfo = new()
            {
                Title = assemblyName?.Name,
                Version = $"v{assemblyName?.Version?.Major}",
                Description = $"{assemblyName?.Name} {assemblyName?.Version?.ToString()} api's",
                Contact = new OpenApiContact
                {
                    Name = "Featurize",
                    Url = new Uri("https://www.featurize-dev.io/")
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT",
                    Url = new Uri("https://www.featurize-dev.io/")
                }
            };

            options.SchemaFilter<EnumSchemaFilter>();
            options.MapValueObjects();

            options.SwaggerDoc(apiinfo.Version, apiinfo);
        });
    }

    public void Use(WebApplication app)
    {
        app.UseSwagger(x => x.RouteTemplate = "api-docs/{documentName}/swagger.json");
        app.UseSwaggerUI(c =>
        {

            c.SwaggerEndpoint("/api-docs/v1/swagger.json", _assembly.GetName().Name);
        });
    }
}
