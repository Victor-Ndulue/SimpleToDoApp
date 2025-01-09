using Microsoft.Extensions.Options;
using SimpleToDoApp.Extensions;
using SimpleToDoApp.LogConfiguration;
using SimpleToDoApp.Middlewares;

LogConfigurator.ConfigureLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureControllers();
builder.Services.ConfigureApiVersioning();
builder.Services.AddApplicationServices();
builder.Services.ConfigureSwaggerGen();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureEmailConfig(builder.Configuration);
builder.Services.ConfigureAuthServices();
builder.Services.RegisterFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEventLogMiddleware();
app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunMigrationAsync();

app.Run();
