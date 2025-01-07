using SimpleToDoApp.LogConfiguration;

namespace SimpleToDoApp.Middlewares;

public static class EventLogMiddleware
{
    public static void UseEventLogMiddleware(this IApplicationBuilder app)
    {
        var logger = LoggerHelper.Logger;
        app.Use(async (context, next) =>
        {
            var request = context.Request;
            var method = request.Method;
            var path = request.Path;
            var remoteIP = context.Connection.RemoteIpAddress;

            await next();

            logger.Info("Request: {Method} {Path} from {RemoteIP}, Response: {StatusCode}",
                method,
                path,
                remoteIP,
                context.Response.StatusCode);
        });
    }
}