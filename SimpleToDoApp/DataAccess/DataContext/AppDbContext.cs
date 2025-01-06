using Microsoft.EntityFrameworkCore;
using SimpleToDoApp.Models;

namespace SimpleToDoApp.DataAccess.DataContext;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
    {
    }
    protected override void 
        OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void 
        OnModelCreating
        (ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ToDoTask>(toDOTask =>
        {
            toDOTask.Property(x => x.TaskStatus)
                .HasConversion<string>();
            toDOTask.Property(x => x.RecurringInterval)
                .HasConversion<string>();
        });
        modelBuilder.Entity<AppUser>(AppUser =>
        {
            AppUser.HasMany(x => x.ToDoTasks)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId);
        });
    }
    public DbSet<ToDoTask> ToDoTasks { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
}
