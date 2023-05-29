using MediatR;
using Todo.Application.Common.Interfaces;
using Todo.Application.Common.Model;
using Todo.Domain.Enums;

namespace Todo.Application.TaskItems.Commands.UpdateTodoItem;

public class ResponseUpdateTodoItem : ResponseModel
{
    public ResponseUpdateTodoItem(bool status, string message)
        : base(status, message) { }
}

public record UpdateTodoItemCommand : IRequest<ResponseUpdateTodoItem>
{
    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public Status Status { get; set; } = Status.NotStarted;
    public PriorityLevel PriorityLevel { get; set; } = PriorityLevel.None;
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, ResponseUpdateTodoItem>
{
    private readonly ITaskItemRepository _repository;

    public UpdateTodoItemCommandHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public Task<ResponseUpdateTodoItem> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var item = _repository.GetById(request.Id);

        var updatedItem = new Domain.Entities.TodoItem()
        {
            Id = item.Id,
            Name = request.Name,
            Status = request.Status,
            Priority = request.PriorityLevel
        };
        
        _repository.Delete(item);
        
        return Task.FromResult(result: _repository.Insert(updatedItem).Result
            ? new ResponseUpdateTodoItem(status: true,
                message: "To-do item is successfully updated.")
            : new ResponseUpdateTodoItem(status: false,
                message: "An error occurred while updating a to-do item."));
    }
}
