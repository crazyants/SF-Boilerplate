using System;

namespace SF.Core.Abstraction.UoW.Exceptions
{
   
    /// <summary>
    /// Exception thrown when a commit to a <see cref="IUnitOfWork"/> fails.
    /// </summary>
    public class CommitException : UnitOfWorkException
    {
        private const string DefaultMessage =
            "Failed to commit the current work. See inner exception for details.";

        /// <summary>
        /// Creates a new instance with the default message
        /// </summary>
        public CommitException() : this(DefaultMessage)
        {

        }

        /// <summary>
        /// Creates a new instance with the default message and the related exception
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CommitException(Exception innerException) : base(DefaultMessage, innerException)
        {

        }

        /// <summary>
        /// Creates a new instance with the given error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CommitException(string message) : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance with the given error message and the related exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CommitException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}