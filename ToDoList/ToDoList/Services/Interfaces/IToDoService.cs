using ToDoList.Models;

public interface IToDoService
{
    Task<IEnumerable<ToDoItem>> GetUserItemsAsync(string userId);
    Task<bool> AddTaskAsync(string userId, AddItemInputModel inputModel);
}