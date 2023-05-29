using MediatR;
using Todo.Application.Common.Interfaces;

namespace Todo.Application.TaskItems.Queries;

public record GetAllTodosCommand : IRequest<List<TodoItemDto>>;

public record GetTodoByIdCommand : IRequest<TodoItemDto>
{
    public int Id { get; set; }
}
public record GetTodoByNameCommand : IRequest<bool>
{
    public string Name { get; set; }
}

public class GetTodosQueryHandler :
    IRequestHandler<GetAllTodosCommand, List<TodoItemDto>>,
    IRequestHandler<GetTodoByIdCommand, TodoItemDto>,
    IRequestHandler<GetTodoByNameCommand, bool>

{
    private readonly ITaskItemRepository _repository;

    public GetTodosQueryHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TodoItemDto>> Handle(GetAllTodosCommand request, CancellationToken cancellationToken)
    {
        var taskItemDtos = _repository.GetAll()
            .ToList()
            .Select(item => new TodoItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Priority = item.Priority, 
                Status = item.Status 
            }).ToList();
        return Task.FromResult(taskItemDtos);
    }

    public Task<TodoItemDto> Handle(GetTodoByIdCommand request, CancellationToken cancellationToken)
    {
        var item = _repository.GetById(request.Id);
        return Task.FromResult(new TodoItemDto()
        {
            Id = item.Id,
            Name = item.Name,
            Status = item.Status,
            Priority = item.Priority
        });
    }

    public Task<bool> Handle(GetTodoByNameCommand request, CancellationToken cancellationToken)
    {
        return _repository.IsExist(request.Name);
    }
}
