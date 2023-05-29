using Microsoft.AspNetCore.Mvc;
using Todo.Application.TaskItems;
using Todo.Application.TaskItems.Commands.CreateTodoItem;
using Todo.Application.TaskItems.Commands.DeleteTodoItem;
using Todo.Application.TaskItems.Commands.UpdateTodoItem;
using Todo.Application.TaskItems.Queries;

namespace Todo.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsApiController : ApiControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> Get(CancellationToken cancellationToken)
    {
        var foundItem = await Mediator.Send(new GetAllTodosCommand(), cancellationToken);
        return Ok(foundItem);
    }
    
    [HttpPost("CreateTodoItem")]
    public async Task<ActionResult<ResponseCreateTodoItem>> Create(CreateTodoItemCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("Get/{id}")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetItemById(int id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(
            new GetTodoByIdCommand(){Id = id}, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("IsExist/{name}")]
    public async Task<ActionResult<bool>> IsExist(string name,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(
            new GetTodoByNameCommand(){Name = name}, cancellationToken);
        return result;
    }
    
    [HttpPost("UpdateTodoItem")]
    public async Task<IActionResult> Update(UpdateTodoItemCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
    
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseDeleteItem>> Delete(int id, 
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteTodoItemCommand() { Id = id },
            cancellationToken);
        return Ok(result);
    }
}