using System;

namespace SF.Core.Abstraction.UoW.Exceptions
{
 
    /// <summary>
    /// Exception to be used when the scope is not defined
    /// </summary>
    public class UndefinedScopeException : UnitOfWorkException
    {
        private const string DefaultMessage =
            "The scope of the current unit of work is undefined. The begin method should be invoked before any commit.";

        /// <summary>
        /// The <seealso cref="ScopeEnabledUnitOfWork"/> scope value
        /// </summary>
        public int Scope { get; }

        /// <summary>
        /// Creates a new instance with the default message
        /// </summary>
        /// <param name="scope">The current scope</param>
        public UndefinedScopeException(int scope) : this(scope, DefaultMessage)
        {

        }

        /// <summary>
        /// Creates a new instance with the default message and the related exception
        /// </summary>
        /// <param name="scope">The current scope</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UndefinedScopeException(int scope, Exception innerException) : base(DefaultMessage, innerException)
        {
            Scope = scope;
        }

        /// <summary>
        /// Creates a new instance with the given error message
        /// </summary>
        /// <param name="scope">The current scope</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UndefinedScopeException(int scope, string message) : base(message)
        {
            Scope = scope;
        }

        /// <summary>
        /// Creates a new instance with the given error message and the related exception
        /// </summary>
        /// <param name="scope">The current scope</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UndefinedScopeException(int scope, string message, Exception innerException) : base(message, innerException)
        {
            Scope = scope;
        }
    }
}