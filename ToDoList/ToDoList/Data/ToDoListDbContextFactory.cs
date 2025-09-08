using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ToDoList.Data
{
    public class ToDoListDbContextFactory : IDesignTimeDbContextFactory<ToDoListDbContext>
    {
        public ToDoListDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoListDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ToDoListDb;User Id=SA;Password=yourStrong(!)Password;Encrypt=True;TrustServerCertificate=True;");

            return new ToDoListDbContext(optionsBuilder.Options);
        }
    }
}