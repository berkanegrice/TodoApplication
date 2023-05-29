using Todo.Application.TaskItems;

namespace Todo.WebUI.Models.TodoItem;

public class TodoViewModel
{
    public IEnumerable<TodoItemDto>? Todos { get; set; }
}