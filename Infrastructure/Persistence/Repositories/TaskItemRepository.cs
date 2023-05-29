using Todo.Application.Common.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private IList<TodoItem> _taskItems { get; } = new List<TodoItem>();
    
    public Task<bool> Insert(TodoItem entity)
    {
        if (GetByName(entity.Name) != null)
            return Task.FromResult(false);

        if (entity.Id == 0)
            entity.Id = NextId();
        
        _taskItems.Add(entity);
        
        return Task.FromResult(true);
    }

    public Task<bool> Delete(TodoItem entity)
    {
        _taskItems.Remove(entity);
        return Task.FromResult(!_taskItems.Contains(entity));
    }

    public IQueryable<TodoItem> GetAll()
    {
        return _taskItems.AsQueryable();
    }

    public TodoItem? GetByName(string name)
    {
        return _taskItems.FirstOrDefault(x => x.Name == name);
    }

    public TodoItem GetById(int id)
    {
        return _taskItems.First(x => x.Id == id);
    }

    public Task<bool> IsExist(string name)
    {
        return Task.FromResult(_taskItems.Any(x => x.Name == name));
    }

    #region Helper Methods
    private int NextId()
    {
        var currentId = _taskItems.Any() ? _taskItems.Max(x => x.Id) : 0;
        return currentId + 1;
    }
    #endregion
}
