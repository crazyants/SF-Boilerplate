 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SF.Core.Abstraction.GenericServices
{
    /// <summary>
    /// Interface for handling feedback of success or errors from an operation
    /// </summary>
    public interface ISuccessOrErrors
    {
        /// <summary>
        /// Holds the list of errors. Empty list means no errors. Null means validation has not been done
        /// </summary>
        IReadOnlyList<ValidationResult> Errors { get; }

        /// <summary>
        /// This returns any warning messages
        /// </summary>
        IReadOnlyList<string> Warnings { get; }

        /// <summary>
        /// Returns true if not errors or not validated yet, else false. 
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Returns true if any errors. Note: different to IsValid in that it just checks for errors,
        /// i.e. different to IsValid in that no errors but unset Validity will return false.
        /// Useful for checking inside a method where the status is being manipulated.
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Returns true if not errors or not validated yet, else false. 
        /// </summary>
        bool HasWarnings { get; }

        /// <summary>
        /// This returns the success message with suffix is nay warning messages
        /// </summary>
        string SuccessMessage { get; }

        /// <summary>
        /// Adds a warning message. It places the test 'Warning: ' before the message
        /// </summary>
        /// <param name="warningformat"></param>
        /// <param name="args"></param>
        void AddWarning(string warningformat, params object[] args);

        /// <summary>
        /// Copies in validation errors found outside into the status
        /// </summary>
        ISuccessOrErrors SetErrors(IEnumerable<ValidationResult> errors);

        /// <summary>
        /// This sets the error list to a series of non property specific error messages
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        ISuccessOrErrors SetErrors(IEnumerable<string> errors);

        /// <summary>
        /// Allows a single error to be set.
        /// </summary>
        /// <param name="errorformat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ISuccessOrErrors AddSingleError(string errorformat, params object[] args);

        /// <summary>
        /// This adds an error for a specific, named parameter
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="errorformat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ISuccessOrErrors AddNamedParameterError(string parameterName, string errorformat, params object[] args);

        /// <summary>
        /// This combines any errors or warnings into the current status.
        /// Note: it does NOT copy any success message into the current status
        /// as it is the job of the outer status to set its own success message
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        ISuccessOrErrors Combine(object status);

        /// <summary>
        /// This sets a success message and sets the IsValid flag to true
        /// </summary>
        /// <param name="successformat"></param>
        /// <param name="args"></param>
        ISuccessOrErrors SetSuccessMessage(string successformat, params object[] args);

        /// <summary>
        /// This returns all the error messages, with parameter name prefix if appropriate, joined together into one long string
        /// </summary>
        /// <param name="joinWith">By default joined using \n, i.e. newline. Can provide different join string </param>
        /// <returns></returns>
        string GetAllErrors(string joinWith = "\n");

        /// <summary>
        /// This returns the errors as:
        /// If only one error then as a html p 
        /// If multiple errors then as an unordered list
        /// </summary>
        /// <returns>simple html data without any classes</returns>
        string ErrorsAsHtml();
    }
}
