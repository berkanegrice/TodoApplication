using Todo.Domain.Enums;

namespace Todo.Domain.Entities;

public class TodoItem : EntityBase
{
    public string Name { get; set; }
    public Status Status { get; set; }
    public PriorityLevel Priority { get; set; }
    public bool Done { get; set; }
}