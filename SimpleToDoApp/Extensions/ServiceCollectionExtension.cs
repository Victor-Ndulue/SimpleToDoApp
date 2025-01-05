using Microsoft.EntityFrameworkCore;
using SimpleToDoApp.DataAccess.DataContext;
using SimpleToDoApp.IServiceRepo;
using SimpleToDoApp.ServiceRepo;

namespace SimpleToDoApp.Extensions;

public static class ServiceCollectionExtension
{
    public static void 
        ConfigureDbContext
        (this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = Environment.GetEnvironmentVariable("SimpleToDoAppDB");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void 
        AddApplicationServices
        (this IServiceCollection services)
    {
        services.AddScoped<ITaskServiceRepo, TaskServiceRepo>();
    }
}
