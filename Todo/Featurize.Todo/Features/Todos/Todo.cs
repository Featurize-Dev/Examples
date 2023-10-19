using Featurize.DomainModel;
using Featurize.Todo.Features.Todos.Entities;
using Featurize.Todo.Features.Todos.ValueObjects;

namespace Featurize.Todo.Features.Todos;

public class Todo : AggregateRoot<Todo, TodoId>
{
    private readonly List<TaskItem> _tasks = new();

    public Todo(TodoId id) : base(id)
    {
        RecordEvent(new TodoCreated());
    }

    public TaskItem[] Tasks => _tasks.ToArray();

    public void AddTask(TaskItem item)
    {
        RecordEvent(new TodoTaskAdded(item));
    }

    public void ClearAll()
    {
        RecordEvent(new AllTasksCleared());
    }

    public bool RemoveTask(TaskId taskId)
    {
        if(_tasks.Any(x=>x.Id == taskId))
        {
            RecordEvent(new TodoTaskRemoved(taskId));
            return true;
        }
        
        return false;
    }

    public bool UpdateTask(TaskItem taskItem)
    {
        if (_tasks.Any(x => x.Id == taskItem.Id))
        {
            RecordEvent(new TodoTaskUpdated(taskItem));
            return true;
        }

        return false;
    }
    internal void Apply(TodoCreated e)
    {
        _tasks.Clear();
    }

    internal void Apply(TodoTaskAdded e)
    {
        _tasks.Add(e.Item);
    }

    internal void Apply(AllTasksCleared e)
    {
        _tasks.Clear();
    }

    internal void Apply(TodoTaskRemoved e)
    {
        _tasks.RemoveAll(x => x.Id == e.Id);
    }

    internal void Apply(TodoTaskUpdated e)
    {
        _tasks.RemoveAll(x => x.Id == e.Item.Id);
        _tasks.Add(e.Item);
    }

    
}

internal record TodoCreated() : EventRecord;
internal record TodoTaskAdded(TaskItem Item) : EventRecord;
internal record AllTasksCleared() : EventRecord;
internal record TodoTaskRemoved(TaskId Id) : EventRecord;
internal record TodoTaskUpdated(TaskItem Item) : EventRecord;
