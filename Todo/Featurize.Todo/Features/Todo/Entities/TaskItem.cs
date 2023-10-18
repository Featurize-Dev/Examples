using Featurize.Todo.Features.Todo.ValueObjects;
using System.Reflection;

namespace Featurize.Todo.Features.Todo.Entities;

public record TaskItem(TaskItemId Id, string Title, DateTime DueDate, bool Completed, TaskStatus Status)
{
    internal static TaskItem Create(string title, DateTime dueDate)
    {
        return new(new TaskItemId(), title, dueDate, false, TaskStatus.Pending);
    }
}

public enum TaskStatus
{
    Pending,
}
