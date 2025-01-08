using Microsoft.EntityFrameworkCore;
using SimpleToDoApp.DataAccess.DataContext;

namespace SimpleToDoApp.Middlewares;

public static class MigrationHandler
{
    public static async Task RunMigrationAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        await using var db = scope.ServiceProvider.GetService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
}
