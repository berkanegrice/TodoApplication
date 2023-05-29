using System.ComponentModel.DataAnnotations;
using Todo.Domain.Enums;
using Todo.WebUI.Helpers;

namespace Todo.WebUI.Models.TodoItem;

public class TodoItemCreateViewModel
{
    [Required]
    [IsUnique]
    public string Name { get; set; }
    
    public PriorityLevel Priority { get; set; }
}