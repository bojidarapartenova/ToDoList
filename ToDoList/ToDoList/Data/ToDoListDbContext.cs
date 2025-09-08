using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ToDoListDbContext : IdentityDbContext<IdentityUser>
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
        : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}