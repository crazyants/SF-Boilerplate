using System;
using System.Text;

namespace SF.Core.Common
{
    public class ExceptionHelper
    {
        /// <summary>
        /// Returns the messages of all inner exceptions.
        /// </summary>
        /// <param name="ex">The exception to get the messages from.</param>
        /// <returns>A string with the messages of all inner exceptions.</returns>
        public static string GetAllMessages(Exception ex)
        {
            if ( ex == null ) return "null";
            var currentEx = ex;
            var builder = new StringBuilder();
            do
            {
                builder.AppendLine(currentEx.Message);
                currentEx = currentEx.InnerException;
            } while ( currentEx != null );
            return builder.ToString();
        }

        /// <summary>
        /// Returns the stack traces of all inner exceptions.
        /// </summary>
        /// <param name="ex">The exception to get the stacktraces from.</param>
        /// <returns>A string with the stacktraces of all inner exceptions.</returns>
        public static string GetAllStacktraces(Exception ex)
        {
            if ( ex == null ) return "null";
            var currentEx = ex;
            var builder = new StringBuilder();
            do
            {
                builder.AppendLine(currentEx.StackTrace);
                currentEx = currentEx.InnerException;
            } while ( currentEx != null );
            return builder.ToString();
        }

        /// <summary>
        /// Returns the result of ToString() of all inner exceptions (The ToString() method of an exception returns the message and stacktrace).
        /// </summary>
        /// <param name="ex">The exception to get the messages and stacktraces from.</param>
        /// <returns>A string with the messages and stacktraces of all inner exceptions.</returns>
        public static string GetAllToStrings(Exception ex)
        {
            if ( ex == null ) return "null";
            var currentEx = ex;
            var builder = new StringBuilder();
            do
            {
                builder.AppendLine(currentEx.ToString());
                currentEx = currentEx.InnerException;
            } while ( currentEx != null );
            return builder.ToString();
        }
    }
}