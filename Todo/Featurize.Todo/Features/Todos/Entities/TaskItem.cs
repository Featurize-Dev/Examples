using Featurize.Todo.Features.Todos.ValueObjects;

namespace Featurize.Todo.Features.Todos.Entities;

public record TaskItem(TaskId Id, string Title, DateTime DueDate, bool Completed, TaskStatus Status)
{
    internal static TaskItem Create(string title, DateTime dueDate)
    {
        return new(new TaskId(), title, dueDate, false, TaskStatus.Pending);
    }
}

public enum TaskStatus
{
    Pending,
}
