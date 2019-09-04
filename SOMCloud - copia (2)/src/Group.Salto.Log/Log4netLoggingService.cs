using log4net;
using System;
using System.Reflection;

namespace Group.Salto.Log
{
    public class Log4netLoggingService : ILoggingService
    {
        /// <summary>
        /// The real log4net logger instance.
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4netLoggingService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <remarks>
        /// ILog could be initialized as: LogManager.GetLogger("AE.Eventing.Listener"), where the "AE.Eventing.Listener" should be Logger set of appenders.
        /// 
        /// More info at: https://logging.apache.org/log4net/release/manual/configuration.html 
        /// </remarks>
        public Log4netLoggingService(Assembly assembly, string loggerName)
        {
            _logger = LogManager.GetLogger(assembly, loggerName);
        }

        /// <summary>
        /// Logs an <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogException(Exception exception)
        {
            _logger.Fatal(exception.Message, exception);
        }

        /// <summary>
        /// Logs an <paramref name="exception" /> with an explanation specified by <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">The exception.</param>
        public void LogException(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        /// <summary>
        /// Logs information message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Logs warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Logs verbose message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogVerbose(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Logs error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogError(string message)
        {
            _logger.Error(message);
        }
    }
}