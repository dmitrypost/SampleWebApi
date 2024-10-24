namespace SampleWebApi.Shared.Logging
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogError(Exception ex, string message);
    }

    public class Logger(NLog.ILogger logger) : ILogger
    {
        public void LogInformation(string message)
        {
            logger.Info(message);
        }

        public void LogError(Exception ex, string message)
        {
            logger.Error(ex, message);
        }
    }
}
