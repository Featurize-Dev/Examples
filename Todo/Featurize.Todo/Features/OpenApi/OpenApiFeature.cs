using Featurize.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                { Name = "Featurize", Url = new Uri("https://www.featurize-dev.io/") },
                License = new OpenApiLicense()
                {
                    Name = "MIT",
                    Url = new Uri("https://www.featurize-dev.io/")
                }
            };

            OpenApiSecurityScheme securityDefinition = new()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri("https://localhost:5001/.well-known/openid-configuration")
            };

            //var authority = "https://localhost:5001/";

            options.SwaggerDoc("v1", apiinfo);
            options.AddSecurityDefinition("bearer", securityDefinition);
            // Make sure swagger UI requires a Bearer token to be specified
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { securityDefinition, Array.Empty<string>() }
                });
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
