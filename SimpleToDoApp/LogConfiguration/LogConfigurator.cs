using NLog;

namespace SimpleToDoApp.LogConfiguration;

public static class LogConfigurator
{
    public static void ConfigureLogger()
    {
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/LoggerConfiguration/nlog.config"));
    }
}
