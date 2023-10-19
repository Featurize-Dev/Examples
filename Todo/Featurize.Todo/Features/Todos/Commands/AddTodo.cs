using Featurize.Todo.Features.Storage;
using Featurize.Todo.Features.Todos.Entities;
using Featurize.Todo.Features.Todos.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Featurize.Todo.Features.Todos.Commands;

public static class AddTask
{
    public static IEndpointRouteBuilder MapAddTask(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Route, Handle)
            .WithOpenApi(o =>
            {
                o.OperationId = "AddTask";
                o.Summary = "Adds a new task to the specified todo list.";
                o.Description = "This is a description";

                o.Parameters[0].Description = "Todo list identifier";
                o.Parameters[0].Example = new OpenApiString(new TodoId().ToString());
                
                return new(o);
            });
        return builder;
    }

    public static string Route => "add-task";

    public static async Task<Results<Ok, NotFound>> Handle(
         [FromServices] AggregateManager<Todo, TodoId> manager,
         [FromRoute] TodoId id,
         [FromBody] AddTodoRequest request
        )
    {
        var aggregate = await manager.LoadAsync(id)
            ??  new Todo(id);

        aggregate.AddTask(TaskItem.Create(request.Title, request.DueDate));

        await manager.SaveAsync(aggregate);

        return TypedResults.Ok();
    }
}


public record AddTodoRequest(string Title, DateTime DueDate);
