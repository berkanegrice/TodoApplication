using Todo.Application.TaskItems;
using Todo.Application.TaskItems.Commands.CreateTodoItem;
using Todo.Application.TaskItems.Commands.DeleteTodoItem;
using Todo.Application.TaskItems.Commands.UpdateTodoItem;
using Todo.Domain.Entities;

namespace Todo.WebUI.Services;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItemDto>> GetAll();
    Task<ResponseCreateTodoItem> AddItemAsync(TodoItem todoItem);
    Task<TodoItemDto> GetItemAsync(int id);
    Task<ResponseUpdateTodoItem> UpdateTodoAsync(TodoItem todoItem);
    Task<bool> IsExistAsync(string? name);
    Task<ResponseDeleteItem> DeleteItemAsync(int id);
}