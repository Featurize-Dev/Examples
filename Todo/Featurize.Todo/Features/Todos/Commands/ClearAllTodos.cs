using Featurize.Todo.Features.Storage;
using Featurize.Todo.Features.Todos.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace Featurize.Todo.Features.Todos.Commands;

public static class ClearAllTodos
{
    public static IEndpointRouteBuilder MapClearTasks(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete(Route, Handle)
            .WithOpenApi(o =>
            {
                o.Summary = "Clears all tasks from the specified todo list.";
                o.Description = "This is a description";

                o.Parameters[0].Description = "Todo list identifier";
                o.Parameters[0].Example = new OpenApiString(new TodoId().ToString());

                return new(o);
            });
        return builder;
    }

    public static string Route => "clear-all";

    public static async Task<Results<Ok, NotFound<TodoId>>> Handle(
        [FromServices] AggregateManager<Todo, TodoId> manager,
        [FromRoute] TodoId id
        )
    {
        var aggregate = await manager.LoadAsync( id );

        if (aggregate is null) 
        {
            return TypedResults.NotFound(id);
        }

        aggregate.ClearAll();

        await manager.SaveAsync( aggregate );

        return TypedResults.Ok();
    }
}
