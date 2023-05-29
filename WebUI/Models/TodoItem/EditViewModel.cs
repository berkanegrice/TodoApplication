using System.ComponentModel.DataAnnotations;
using Todo.Domain.Enums;

namespace Todo.WebUI.Models.TodoItem;

public class EditViewModel
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    public PriorityLevel Priority { get; set; }
    public Status Status { get; set; }
}