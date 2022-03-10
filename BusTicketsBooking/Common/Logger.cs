using Serilog;
using Serilog.Events;
using System;

namespace Common
{
    public class Logger : ILog
    {
        private ILogger _logger;


        public Logger(ILogger logger)
        {
            _logger = logger;
        }


        public void Log(LogLevel logLevel, Exception exception, string message)
        {
            _logger.Write((LogEventLevel)logLevel, exception, message);
        }

        public void Log(LogLevel logLevel, string message)
        {
            _logger.Write((LogEventLevel)logLevel, message);
        }
    }
}
