using Featurize.AspNetCore;
using Featurize.Todo.Features.Todo.Commands;

namespace Featurize.Todo.Features.Todo;

public class TodoFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        
    }

    public void Use(WebApplication app)
    {
        app.MapAddTodo();
    }
}
