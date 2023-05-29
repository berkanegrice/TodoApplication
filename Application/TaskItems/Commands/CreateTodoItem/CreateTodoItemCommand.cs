using MediatR;
using Todo.Application.Common;
using Todo.Application.Common.Interfaces;
using Todo.Application.Common.Model;
using Todo.Domain.Enums;
namespace Todo.Application.TaskItems.Commands.CreateTodoItem;

public class ResponseCreateTodoItem : ResponseModel
{
    public ResponseCreateTodoItem(bool status, string message)
        : base(status, message) { }
}

public record CreateTodoItemCommand : IRequest<ResponseCreateTodoItem>
{
    public string Name { get; init; }
    public PriorityLevel PriorityLevel { get; init; }
    public Status Status { get; init; } = Status.NotStarted;
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, ResponseCreateTodoItem>
{
    private readonly ITaskItemRepository _repository;

    public CreateTodoItemCommandHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public Task<ResponseCreateTodoItem> Handle(CreateTodoItemCommand request, 
        CancellationToken cancellationToken)
    {
        var item = new Domain.Entities.TodoItem()
        {
            Name = request.Name,
            Priority = request.PriorityLevel,
            Status = request.Status
        };

        return Task.FromResult(result: _repository.Insert(item).Result
            ? new ResponseCreateTodoItem(status: true,
                message: "To-do item is successfully added.")
            : new ResponseCreateTodoItem(status: false,
                message: "There is already todo item exist within same name."));
    }
}

