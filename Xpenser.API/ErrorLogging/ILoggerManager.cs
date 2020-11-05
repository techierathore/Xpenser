
namespace Xpenser.API.ErrorLogging
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogCritical(string message);
    }
}
