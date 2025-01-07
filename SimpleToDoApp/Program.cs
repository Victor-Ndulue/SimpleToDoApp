using SimpleToDoApp.Extensions;
using SimpleToDoApp.LogConfiguration;
using SimpleToDoApp.Middlewares;

LogConfigurator.ConfigureLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureSwaggerGen();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
app.UseEventLogMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunMigrationAsync();

app.Run();
