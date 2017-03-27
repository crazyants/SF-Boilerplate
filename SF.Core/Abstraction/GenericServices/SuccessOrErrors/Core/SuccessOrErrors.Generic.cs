 

using System;
using System.Collections.Generic;

namespace SF.Core.Abstraction.GenericServices
{
	/// <summary>
	/// This class handles feedback of success or errors and, if successful returns a result
	/// </summary>
	/// <typeparam name="T">The Type of the Result to return if IsValid is true</typeparam>
    public class SuccessOrErrors<T> : SuccessOrErrors, ISuccessOrErrors<T>
    {
        /// <summary>
        /// Holds the value set using SetSuccessWithResult
        /// </summary>
        public T Result { get; private set; }


        //ctors
        public SuccessOrErrors() :base() {}

        public SuccessOrErrors(T result, string successformat, params object[] args) : base()
        {
            Result = result;
            base.SetSuccessMessage(successformat, args);
        }

        private SuccessOrErrors(ISuccessOrErrors nonResultStatus)
            : base(nonResultStatus) {}


        /// <summary>
        /// This sets a successful end by setting the Result and supplying a success message 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="successformat"></param>
        /// <param name="args"></param>
        public ISuccessOrErrors<T> SetSuccessWithResult(T result, string successformat, params object[] args)
        {
            Result = result;
            base.SetSuccessMessage(successformat, args);
            return this;
        }

        public new ISuccessOrErrors<T> SetErrors(IEnumerable<string> errors)
        {
            base.SetErrors(errors);
            return this;
        }

        public new ISuccessOrErrors<T> AddSingleError(string errorformat, params object[] args)
        {
            base.AddSingleError(errorformat, args);
            return this;
        }

        public new ISuccessOrErrors<T> AddNamedParameterError(string parameterName, string errorformat, params object[] args)
        {
            base.AddNamedParameterError(parameterName, errorformat, args);
            return this;
        }

	    public new ISuccessOrErrors<T> Combine(object status)
	    {
	        base.Combine(status);
	        return this;
	    }

	    /// <summary>
        /// This allows the current success message to be updated
        /// </summary>
        /// <param name="successformat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ISuccessOrErrors<T> UpdateSuccessMessage(string successformat, params object[] args)
        {
            base.SetSuccessMessage(successformat, args);
            return this;
        }

        /// <summary>
        /// Turns the non result status into a result status by copying any errors or warnings
        /// </summary>
        /// <param name="status">Can be a non-result status, or a result status of a different type</param>
        /// <returns></returns>
        public static ISuccessOrErrors<T> ConvertNonResultStatus(object status)
        {
            var castISuccessOrErrors = status as ISuccessOrErrors;
            if (castISuccessOrErrors == null)
                throw new ArgumentNullException("status", "The status parameter was not derived from a type thta supported ISuccessOrErrors.");

            return new SuccessOrErrors<T>(castISuccessOrErrors);
        }

        /// <summary>
        /// This is a quick way to create an ISuccessOrErrors(T) with a success message and result
        /// </summary>
        /// <param name="result"></param>
        /// <param name="formattedSuccessMessage"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ISuccessOrErrors<T> SuccessWithResult(T result, string formattedSuccessMessage, params object[] args)
        {
            return new SuccessOrErrors<T>().SetSuccessWithResult(result, string.Format(formattedSuccessMessage, args));
        }

    }


}
