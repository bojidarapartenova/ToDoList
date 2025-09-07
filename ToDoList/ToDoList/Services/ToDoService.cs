using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

public class ToDoService : IToDoService
{
    private readonly ToDoListDbContext dbContext;

    public ToDoService(ToDoListDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<ToDoItem>> GetUserItemsAsync(string userId)
    {
        IEnumerable<ToDoItem> items = await dbContext
        .ToDoItems
        .ToListAsync();

        return items;
    }

    public async Task<bool> AddTaskAsync(string userId, AddItemInputModel inputModel)
    {
        bool result = false;

        if (userId != null)
        {
            ToDoItem item = new ToDoItem()
            {
                Description = inputModel.Description
            };

            await dbContext.ToDoItems.AddAsync(item);
            await dbContext.SaveChangesAsync();

            result = true;
        }
        return result;
    }
}