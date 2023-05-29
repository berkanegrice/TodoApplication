using System.ComponentModel.DataAnnotations;
using Todo.WebUI.Services;

namespace Todo.WebUI.Helpers;

public class IsUniqueAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
    {
        var todoItemService = (TodoItemService)validationContext.GetService(typeof(ITodoItemService))!;
        var item = todoItemService.IsExistAsync(value.ToString());
        return (item.Result ? new ValidationResult("Name exists") : ValidationResult.Success)!;
    }
}