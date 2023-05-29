
using Todo.Domain.Entities;

namespace Todo.Application.Common.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<bool> Insert(T entity);
    Task<bool> Delete(T entity);
    IQueryable<T> GetAll();
}