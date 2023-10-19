using Featurize.Todo.Features.Storage;
using Featurize.Todo.Features.Todos.Entities;
using Featurize.Todo.Features.Todos.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace Featurize.Todo.Features.Todos.Commands;

public static class EditTask 
{
    public static IEndpointRouteBuilder MapEditTask(this IEndpointRouteBuilder builder)
    {
        builder.MapPut(Route, Handle)
            .WithOpenApi(o =>
            {
                o.Summary = "Edits a task from the specified todo list.";
                o.Description = "This is a description";

                o.Parameters[0].Description = "Todo list identifier";
                o.Parameters[0].Example = new OpenApiString(new TodoId().ToString());

                o.Parameters[1].Description = "Task id";
                o.Parameters[1].Example = new OpenApiString(new TaskId().ToString());

                return new(o);
            });
        return builder;
    }

    public static string Route => "edit-task/{taskId}";

    public static async Task<Results<Ok, NotFound<TodoId>, NotFound<TaskId>>> Handle(
        [FromServices] AggregateManager<Todo, TodoId> manager,
        [FromRoute] TodoId id,
        [FromRoute] TaskId taskId,
        [FromBody] EditTaskRequest request
        )
    {
        var aggregate = await manager.LoadAsync( id );

        if( aggregate is  null )
        {
            return TypedResults.NotFound(id);
        }

        if(aggregate.UpdateTask(new TaskItem(taskId, request.Title, request.DueDate, request.Completed, request.Status)))
        {
            await manager.SaveAsync( aggregate );
            return TypedResults.Ok();
        };

        return TypedResults.NotFound(taskId);
    }
}

public record EditTaskRequest(string Title, DateTime DueDate, bool Completed, Entities.TaskStatus Status);
