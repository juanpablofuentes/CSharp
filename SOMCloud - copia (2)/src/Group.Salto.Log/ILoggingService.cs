using System;

namespace Group.Salto.Log
{
    /// <summary>
	/// The Logging Service interface.
	/// </summary>
	public interface ILoggingService
    {
        /// <summary>
        /// Logs an <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void LogException(Exception exception);

        /// <summary>
        /// Logs an <paramref name="exception"/>, with an explanation specified by <paramref name="message"/>.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void LogException(string message, Exception exception);

        /// <summary>
        /// Logs information message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs verbose message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogVerbose(string message);
        /// <summary>
        /// Logs handler errors
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);

    }
}