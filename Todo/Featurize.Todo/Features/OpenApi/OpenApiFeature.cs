using Featurize.AspNetCore;
using Featurize.Todo.Features.OpenApi.Filters;
using Microsoft.OpenApi.Models;

namespace Featurize.Todo.Features.OpenApi;

public class OpenApiFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            OpenApiInfo apiinfo = new()
            {
                Title = "My Todo App",
                Version = "v1",
                Description = "Api's for a Todo app",
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

            c.SwaggerEndpoint("/api-docs/v1/swagger.json", "My Todo App");
        });
    }
}
