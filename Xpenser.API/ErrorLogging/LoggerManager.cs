using NLog;
namespace Xpenser.API.ErrorLogging
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger NLogLogger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogCritical(string message)
        {
            NLogLogger.Fatal(message);
        }

        public void LogDebug(string message)
        {
            NLogLogger.Debug(message);
        }

        public void LogError(string message)
        {
            NLogLogger.Error(message);
        }

        public void LogInfo(string message)
        {
            NLogLogger.Info(message);
        }

        public void LogWarn(string message)
        {
            NLogLogger.Warn(message);
        }
    }
}
}
