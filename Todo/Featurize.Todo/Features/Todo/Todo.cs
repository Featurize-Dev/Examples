using Featurize.DomainModel;
using Featurize.Todo.Features.Todo.Entities;
using Featurize.Todo.Features.Todo.ValueObjects;

namespace Featurize.Todo.Features.Todo;

public class Todos : AggregateRoot<Todos, TodoId>
{
    private readonly List<TaskItem> _tasks = new();

    private Todos(TodoId id) : base(id)
    {
        RecordEvent(new TodoCreated());
    }

    public TaskItem[] Tasks => _tasks.ToArray();

    public void AddTodo(TaskItem item)
    {
        RecordEvent(new TodoTaskAdded(item));
    }

    internal void Apply(TodoCreated e)
    {
        _tasks.Clear();
    }

    internal void Apply(TodoTaskAdded e)
    {
        _tasks.Add(e.Item);
    }
}

internal record TodoCreated() : EventRecord;
internal record TodoTaskAdded(TaskItem Item) : EventRecord;
