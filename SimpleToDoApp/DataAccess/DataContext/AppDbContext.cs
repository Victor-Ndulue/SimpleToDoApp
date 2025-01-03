using Microsoft.EntityFrameworkCore;

namespace SimpleToDoApp.DataAccess.DataContext;

public sealed class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
    {

    } 
}
