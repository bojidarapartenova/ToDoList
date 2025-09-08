using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.ViewModels;

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
        .Where(i=>i.IsDeleted==false)
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

    public async Task<DeleteItemViewModel?> GetItemToDeleteAsync(int id)
    {
        DeleteItemViewModel? itemToDelete = null;

        ToDoItem? item = await dbContext
        .ToDoItems
        .AsNoTracking()
        .SingleOrDefaultAsync(i => i.Id == id);

        if (item != null)
        {
            itemToDelete = new DeleteItemViewModel()
            {
                Id = item.Id,
                Description = item.Description
            };
        }

        return itemToDelete;
    }

    public async Task<bool> SoftDeleteItem(DeleteItemViewModel viewModel)
    {
        bool result = false;

        ToDoItem? itemToDelete = await dbContext
        .ToDoItems
        .FindAsync(viewModel.Id);

        if (itemToDelete != null)
        {
            itemToDelete.IsDeleted = true;

            await dbContext.SaveChangesAsync();
            result = true;
        }

        return result;
    }
}