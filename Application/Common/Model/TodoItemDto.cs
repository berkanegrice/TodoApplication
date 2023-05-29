using Todo.Domain.Enums;

namespace Todo.Application.TaskItems;

public class TodoItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
    public PriorityLevel Priority { get; set; }
    public bool Done { get; set; }
}
