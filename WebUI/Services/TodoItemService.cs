using System.Text;
using Newtonsoft.Json;
using Todo.Application.TaskItems;
using Todo.Application.TaskItems.Commands.CreateTodoItem;
using Todo.Application.TaskItems.Commands.DeleteTodoItem;
using Todo.Application.TaskItems.Commands.UpdateTodoItem;
using Todo.Domain.Entities;
using Todo.WebUI.Helpers;

namespace Todo.WebUI.Services;

public class TodoItemService : ITodoItemService
{
    private readonly HttpClient _client;
    private const string BasePath = "TodoItemsApi";

    public TodoItemService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    public async Task<IEnumerable<TodoItemDto>> GetAll()
    {
        var response = await _client.GetAsync(BasePath+"/GetAll");
        return await response.ReadContentAsync<List<TodoItemDto>>();
    }

    public async Task<ResponseCreateTodoItem> AddItemAsync(TodoItem todoItem)
    {
        var json = JsonConvert.SerializeObject(
            new CreateTodoItemCommand()
        {
            Name = todoItem.Name,
            PriorityLevel = todoItem.Priority,
            Status = todoItem.Status
        });
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(BasePath+"/CreateTodoItem", data);
        return await response.ReadContentAsync<ResponseCreateTodoItem>();
    }

    public async Task<TodoItemDto> GetItemAsync(int id)
    {
        var response = await _client.GetAsync(BasePath + $"/Get/{id}");
        return await response.ReadContentAsync<TodoItemDto>();
    }

    public async Task<ResponseUpdateTodoItem> UpdateTodoAsync(TodoItem todoItem)
    {
        var json = JsonConvert.SerializeObject(
            new UpdateTodoItemCommand()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                PriorityLevel = todoItem.Priority,
                Status = todoItem.Status
            });
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(BasePath+"/UpdateTodoItem", data);
        return await response.ReadContentAsync<ResponseUpdateTodoItem>();    
    }
    
    public async Task<bool> IsExistAsync(string? name)
    {
        var response = await _client.GetAsync(BasePath + $"/IsExist/{name}");
        return await response.ReadContentAsync<bool>();
    }

    public async Task<ResponseDeleteItem> DeleteItemAsync(int id)
    {
        var response = await _client.DeleteAsync(BasePath + $"/Delete/{id}");
        return await response.ReadContentAsync<ResponseDeleteItem>();    
    }
}