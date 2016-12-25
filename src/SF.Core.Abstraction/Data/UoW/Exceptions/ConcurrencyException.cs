using System;

namespace SF.Core.Abstraction.UoW.Exceptions
{
 
    /// <summary>
    /// Exception to be used when the work could not be committed due to concurrency conflicts.
    /// </summary>
    public class ConcurrencyException : UnitOfWorkException
    {
        private const string DefaultMessage =
            "Failed to commit the current work unit due to conflicting changes made concurrently.";

        /// <summary>
        /// Creates a new instance with the default message
        /// </summary>
        public ConcurrencyException() : this(DefaultMessage)
        {

        }

        /// <summary>
        /// Creates a new instance with the default message and the related exception
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ConcurrencyException(Exception innerException) : base(DefaultMessage, innerException)
        {

        }

        /// <summary>
        /// Creates a new instance with the given error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ConcurrencyException(string message) : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance with the given error message and the related exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
