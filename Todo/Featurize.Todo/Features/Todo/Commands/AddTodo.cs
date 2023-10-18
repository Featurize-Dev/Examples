using Featurize.Todo.Features.Storage;
using Featurize.Todo.Features.Todo.Entities;
using Featurize.Todo.Features.Todo.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Featurize.Todo.Features.Todo.Commands;

public static class AddTodo
{
    public static IEndpointRouteBuilder MapAddTodo(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Route, Handle);
        return builder;
    }

    public static string Route => "{id}";

    public static async Task<Results<Ok, NotFound>> Handle(
         [FromServices] AggregateManager<Todos, TodoId> manager,
         [FromRoute] TodoId id,
         [FromBody] AddTodoRequest request
        )
    {
        var aggregate = await manager.LoadAsync(id);
        
        if(aggregate is null)
        {
            return TypedResults.NotFound();
        }

        aggregate.AddTodo(TaskItem.Create(request.Title, request.DueDate));

        return TypedResults.Ok();
    }
}


public record AddTodoRequest(string Title, DateTime DueDate);
