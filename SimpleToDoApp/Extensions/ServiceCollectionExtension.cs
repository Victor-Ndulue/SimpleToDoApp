using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using SimpleToDoApp.DataAccess.DataContext;
using SimpleToDoApp.Extensions.Configs;
using SimpleToDoApp.Helpers.DataValidations;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;
using SimpleToDoApp.IServiceRepo;
using SimpleToDoApp.ServiceRepo;
using System.Text;
using System.Text.Json.Serialization;

namespace SimpleToDoApp.Extensions;

public static class ServiceCollectionExtension
{
    public static void
       ConfigureControllers
       (this IServiceCollection services)
    {
        services.AddControllers
            (
            config =>
            {
                config.RespectBrowserAcceptHeader = true;
            })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddOData(options => options
                .AddRouteComponents("odata", GetAppCoreModel())
                .Select().Filter().OrderBy()
                .SetMaxTop(50)
                .Count().Expand())
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(opts =>
                opts.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .SelectMany(ms => ms.Value.Errors
                        .Select(e => new ValidationError
                        {
                            FieldName = ms.Key,
                            ErrorMessage = e.ErrorMessage
                        }))
                        .ToList();

                    return new BadRequestObjectResult
                    (
                        StandardResponse<IEnumerable<ValidationError>>.Failed
                        (data: errors, errorMessage: "One or more validation errors occurred", 400)
                    );
                }); ;
    }

    public static void
    ConfigureApiVersioning
    (this IServiceCollection services)
    {
        services.AddApiVersioning
        (opts =>
        {
            opts.DefaultApiVersion = new ApiVersion(1);
            opts.ReportApiVersions = true;
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.ApiVersionReader = ApiVersionReader.Combine
                (
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version")
                );
        }
        ).AddApiExplorer
        (
            opts =>
            {
                opts.GroupNameFormat = "'v'V";
                opts.SubstituteApiVersionInUrl = true;
            }
        );
    }

    public static void 
        AddApplicationServices
        (this IServiceCollection services)
    {
        services.AddScoped<ITaskServiceRepo, TaskServiceRepo>();
        services.AddScoped<IMailServiceRepo, MailServiceRepo>();
        services.AddScoped<IAuthServiceRepo, AuthServiceRepo>();
    }

    public static void
    ConfigureSwaggerGen
    (this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            //options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.OperationFilter<ODataParameterOperationFilter>();
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            options.AddSecurityDefinition("Bearer", securityScheme);
            // Make sure Swagger UI requires a Bearer token to be passed for authentication
            var securityRequirement = new OpenApiSecurityRequirement
        {
            { securityScheme, new List<string>() }
        };
            options.AddSecurityRequirement(securityRequirement);
        });

    }

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
     ConfigureJWT
     (this IServiceCollection services, IConfigurationBuilder configurationBuilder)
    {
        IConfiguration configuration = configurationBuilder
            .AddEnvironmentVariables("JwtSettings_")
            .Build();
        services.Configure<JwtSettings>(configuration);
    }

    public static void
        ConfigureEmailConfig
        (this IServiceCollection services, IConfigurationBuilder configurationBuilder)
    {
        IConfiguration configuration = configurationBuilder
            .AddEnvironmentVariables("PAYBIGISMTP__")
            .Build();
        services.Configure<EmailConfig>(configuration);
    }

    public static void
        ConfigureAuthServices
        (this IServiceCollection services)
    {
        var validIssuer = Environment.GetEnvironmentVariable("JwtSettings_validIssuer");
        var validAudience = Environment.GetEnvironmentVariable("JwtSettings_validAudience");
        var signingKey = Environment.GetEnvironmentVariable("JwtSettings_TokenKey");

        // Configure JWT authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Set the issuer and audience validation parameters
                options.Authority = validIssuer; // The URL of the issuer (e.g., Identity Provider)
                options.RequireHttpsMetadata = false; // Enforce HTTPS in production
                options.Audience = validAudience; // Audience that the token should be for

                // Define the token validation parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,

                    ValidateAudience = true,
                    ValidAudience = validAudience,

                    ValidateLifetime = true, // Ensure the token is not expired
                    ClockSkew = TimeSpan.Zero, // No tolerance for expiration time (optional)

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };
            });
    }

    public static void
        RegisterFluentValidation
        (this IServiceCollection services)
    {

        services.AddValidatorsFromAssemblyContaining<AddTaskDtoValidation>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

    }

    private static IEdmModel
        GetAppCoreModel()
    {
        var builder = new ODataConventionModelBuilder();

        // Add all get response dtos here following the format below
        //builder.EntitySet<Product>("Products");

        // Specify relationships or additional configurations (if any)

        return builder.GetEdmModel(); // Return the completed EDM model
    }
}
