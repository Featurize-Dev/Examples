using Featurize.AspNetCore;
using Featurize.Repositories;
using Featurize.Todo.Features.Storage;
using Featurize.Todo.Features.Todos.Commands;
using Featurize.Todo.Features.Todos.Queries;
using Featurize.Todo.Features.Todos.ValueObjects;

namespace Featurize.Todo.Features.Todos;

public class TodoFeature : IUseFeature, IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<Todo, TodoId>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/todo/{id}")
            .WithTags("Todos");

        group.MapGetTasks();

        group.MapAddTask();
        group.MapDeleteTask();
        group.MapEditTask();
        
        group.MapClearTasks();
    }
}
