using Microsoft.AspNetCore.Diagnostics;
using SimpleToDoApp.Helpers.ObjectWrapper;
using SimpleToDoApp.LogConfiguration;

namespace SimpleToDoApp.Middlewares;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        var logger = LoggerHelper.Logger;
        app.UseExceptionHandler(
            appError => appError.Run(
                async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionFeature is not null)
                    {
                        var error = exceptionFeature.Error;
                        var endPoint = exceptionFeature.Endpoint;
                        logger.Error(error, $"An error occurred processing request {endPoint}.");
                        await context.Response.WriteAsJsonAsync(StandardResponse<string>.Failed
                            (data: null, errorMessage: $"System currently unavailable to process your request at the moment. " +
                            $"\n Error Log: {error.Message}", statusCode: 500));

                    }
                })
            );
    }
}
