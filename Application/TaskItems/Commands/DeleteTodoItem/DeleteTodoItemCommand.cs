using MediatR;
using Todo.Application.Common.Interfaces;
using Todo.Application.Common.Model;
using Todo.Domain.Enums;
namespace Todo.Application.TaskItems.Commands.DeleteTodoItem;

public class ResponseDeleteItem : ResponseModel
{
    public ResponseDeleteItem(bool status, string message)
        : base(status, message) { }
}

public record DeleteTodoItemCommand() : IRequest<ResponseDeleteItem>
{
    public int Id { get; set; }
}

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, ResponseDeleteItem>
{
    private readonly ITaskItemRepository _repository;

    public DeleteTodoItemCommandHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseDeleteItem> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var item = _repository.GetById(request.Id);
        if (item.Status != Status.Completed)
            return new ResponseDeleteItem(status: false, message: "An error occurred while removing a to-do item.");
        await _repository.Delete(item);
        return new ResponseDeleteItem(status: true, message: "To-do item is successfully deleted.");
    }
}
