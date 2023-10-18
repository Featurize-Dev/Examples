using Featurize.AspNetCore;

namespace Featurize.Todo.Features.Authorization;

public class AuthotrizationFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddAuthorization();
    }

    public void Use(WebApplication app)
    {
        app.UseAuthorization();
    }
}
