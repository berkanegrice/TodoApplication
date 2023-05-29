using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Entities;
using Todo.WebUI.Models;
using Todo.WebUI.Models.TodoItem;
using Todo.WebUI.Services;

namespace Todo.WebUI.Controllers;

public class TodoItemsController : Controller
{
    private readonly ITodoItemService _todoItemService;

    public TodoItemsController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _todoItemService.GetAll();
        var viewModel = new TodoViewModel()
        {
            Todos = items
        };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _todoItemService.GetItemAsync(id);
        var editViewModel = new EditViewModel()
        {
            Id = item.Id,
            Name = item.Name,
            Status = item.Status,
            Priority = item.Priority
        };
        return View(editViewModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditViewModel todo)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Index");
        
        var successful = await _todoItemService.UpdateTodoAsync(
            new TodoItem()
            {
                Id = todo.Id,
                Name = todo.Name,
                Status = todo.Status,
                Priority = todo.Priority
            });

        if (!successful.Status)
            return BadRequest("Could not update todo.");
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _todoItemService.DeleteItemAsync(id);
        return response.Status ? RedirectToAction("Index") : BadRequest(response.Message);
    }

    public ViewResult Create() => View();
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name, Priority")]TodoItemCreateViewModel todo)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Index");
        
        var todoItem = new TodoItem
        {
            Name = todo.Name,
            Priority = todo.Priority
        };
        var successful = await _todoItemService.AddItemAsync(todoItem);
    
        if (!successful.Status)
            return BadRequest(new { error = "Could not add item." });
        
        return RedirectToAction("Index");
    }
}