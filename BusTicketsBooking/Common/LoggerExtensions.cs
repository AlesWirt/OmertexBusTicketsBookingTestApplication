using System;

namespace Common
{
    public static class LoggerExtensions
    {
        public static void LogDebug(this ILog logger, string message, Exception exception)
        {
            logger.Log(LogLevel.Debug, exception, message);
        }

        public static void LogDebug(this ILog logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        public static void LogInformation(this ILog logger, string message, Exception exception)
        {
            logger.Log(LogLevel.Info, exception, message);
        }

        public static void LogInformation(this ILog logger, string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        public static void LogWarning(this ILog logger, string message, Exception exception)
        {
            logger.Log(LogLevel.Warning, exception, message);
        }

        public static void LogWarning(this ILog logger, string message)
        {
            logger.Log(LogLevel.Warning, message);
        }

        public static void LogError(this ILog logger, string message, Exception exception)
        {
            logger.Log(LogLevel.Error, exception, message);
        }

        public static void LogError(this ILog logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        public static void LogFatal(this ILog logger, string message, Exception exception)
        {
            logger.Log(LogLevel.Fatal, exception, message);
        }

        public static void LogFatal(this ILog logger, string message)
        {
            logger.Log(LogLevel.Fatal, message);
        }
    }
}