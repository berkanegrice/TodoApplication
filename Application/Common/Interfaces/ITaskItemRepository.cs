namespace Todo.Application.Common.Interfaces;

public interface ITaskItemRepository : IRepository<Domain.Entities.TodoItem>
{
    Domain.Entities.TodoItem? GetByName(string name);
    
    Domain.Entities.TodoItem GetById(int id);

    Task<bool> IsExist(string name);
}