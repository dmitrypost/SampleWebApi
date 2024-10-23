namespace SampleWebApi.Shared.Logging
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogError(string message, Exception ex);
    }

    public class Logger(NLog.ILogger logger) : ILogger
    {
        public void LogInformation(string message)
        {
            logger.Info(message);
        }

        public void LogError(string message, Exception ex)
        {
            logger.Error(ex, message);
        }
    }
}
