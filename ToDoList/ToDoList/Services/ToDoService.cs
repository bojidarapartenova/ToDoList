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
}