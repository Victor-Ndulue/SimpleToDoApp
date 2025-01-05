using Microsoft.EntityFrameworkCore;
using SimpleToDoApp.Models;

namespace SimpleToDoApp.DataAccess.DataContext;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.EnableSensitiveDataLogging();
    }
    private DbSet<ToDoTask> ToDoTasks { get; set; }
}
