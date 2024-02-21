using Common.OpenApi.Filters;
using Featurize.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Common.OpenApi;

public class OpenApiFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            OpenApiInfo apiinfo = new()
            {
                Title = "Gold Credit Bank inc.",
                Version = "v1",
                Description = "Api's for Gold Credit Bank inc.",
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

            options.SwaggerDoc("v1", apiinfo);
        });
    }

    public void Use(WebApplication app)
    {
        app.UseSwagger(x => x.RouteTemplate = "api-docs/{documentName}/swagger.json");
        app.UseSwaggerUI(c =>
        {

            c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Gold Credit Bank inc");
        });
    }
}
