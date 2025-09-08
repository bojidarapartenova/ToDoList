using ToDoList.Models;
using ToDoList.ViewModels;

public interface IToDoService
{
    Task<IEnumerable<ToDoItem>> GetUserItemsAsync(string userId);
    Task<bool> AddTaskAsync(string userId, AddItemInputModel inputModel);
    Task<DeleteItemViewModel?> GetItemToDeleteAsync(int id);
    Task<bool> SoftDeleteItem(DeleteItemViewModel viewModel);
    Task<EditItemInputModel?> GetItemToEditAsync(int id);
    Task<bool> EditItemAsync(EditItemInputModel inputModel);
}