using NLog;
using ILogger = NLog.ILogger;

namespace SimpleToDoApp.LogConfiguration;

public static class LoggerHelper
{
    public static ILogger Logger { get; } = LogManager.GetCurrentClassLogger();
}
