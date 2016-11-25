/*******************************************************************************
* 命名空间: SF.Core.Common
*
* 功 能： N/A
* 类 名： Guard
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/24 11:12:49 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Errors;
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Common
{
    public static class Guard
    {
        private const string AgainstMessage = "Assertion evaluation failed with 'false'.";
        private const string ImplementsMessage = "Type '{0}' must implement type '{1}'.";
        private const string InheritsFromMessage = "Type '{0}' must inherit from type '{1}'.";
        private const string IsTypeOfMessage = "Type '{0}' must be of type '{1}'.";
        private const string IsEqualMessage = "Compared objects must be equal.";
        private const string IsPositiveMessage = "Argument '{0}' must be a positive value. Value: '{1}'.";
        private const string IsTrueMessage = "True expected for '{0}' but the condition was False.";
        private const string NotNegativeMessage = "Argument '{0}' cannot be a negative value. Value: '{1}'.";

        static Guard()
        {
        }

        [DebuggerStepThrough]
        public static void NotEmpty(string arg, string argName)
        {
            if (arg.IsEmpty())
                throw ErrorHelper.Argument(argName, "String parameter '{0}' cannot be null or all whitespace.", argName);
        }

        [DebuggerStepThrough]
        public static void NotEmpty<T>(ICollection<T> arg, string argName)
        {
            if (arg == null || !arg.Any())
                throw ErrorHelper.Argument(argName, "Collection cannot be null and must have at least one item.");
        }

        [DebuggerStepThrough]
        public static void NotEmpty(Guid arg, string argName)
        {
            if (arg == Guid.Empty)
                throw ErrorHelper.Argument(argName, "Argument '{0}' cannot be an empty guid.", argName);
        }

        [DebuggerStepThrough]
        public static void InRange<T>(T arg, T min, T max, string argName) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(min) < 0 || arg.CompareTo(max) > 0)
                throw ErrorHelper.ArgumentOutOfRange(argName, "The argument '{0}' must be between '{1}' and '{2}'.", argName, min, max);
        }

        [DebuggerStepThrough]
        public static void NotOutOfLength(string arg, int maxLength, string argName)
        {
            if (arg.Trim().Length > maxLength)
            {
                throw ErrorHelper.Argument(argName, "Argument '{0}' cannot be more than {1} characters long.", argName, maxLength);
            }
        }

        [DebuggerStepThrough]
        public static void NotNegative<T>(T arg, string argName, string message = NotNegativeMessage) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 0)
                throw ErrorHelper.ArgumentOutOfRange(argName, message.FormatInvariant(argName, arg));
        }

        [DebuggerStepThrough]
        public static void NotZero<T>(T arg, string argName) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) == 0)
                throw ErrorHelper.ArgumentOutOfRange(argName, "Argument '{0}' must be greater or less than zero. Value: '{1}'.", argName, arg);
        }

        [DebuggerStepThrough]
        public static void Against<TException>(bool assertion, string message = AgainstMessage) where TException : Exception
        {
            if (assertion)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        public static void Against<TException>(Func<bool> assertion, string message = AgainstMessage) where TException : Exception
        {
            //Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion())
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        [DebuggerStepThrough]
        public static void IsPositive<T>(T arg, string argName, string message = IsPositiveMessage) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 1)
                throw ErrorHelper.ArgumentOutOfRange(argName, message.FormatInvariant(argName));
        }

        [DebuggerStepThrough]
        public static void IsTrue(bool arg, string argName, string message = IsTrueMessage)
        {
            if (!arg)
                throw ErrorHelper.Argument(argName, message.FormatInvariant(argName));
        }


        [DebuggerStepThrough]
        public static void IsEnumType(Type enumType, object arg, string argName)
        {
            NotNull(arg, argName);
            if (!Enum.IsDefined(enumType, arg))
            {
                throw ErrorHelper.ArgumentOutOfRange(argName, "The value of the argument '{0}' provided for the enumeration '{1}' is invalid.", argName, enumType.FullName);
            }
        }


        [DebuggerStepThrough]
        public static void PagingArgsValid(int indexArg, int sizeArg, string indexArgName, string sizeArgName)
        {
            NotNegative<int>(indexArg, indexArgName, "PageIndex cannot be below 0");
            if (indexArg > 0)
            {
                // if pageIndex is specified (> 0), PageSize CANNOT be 0 
                IsPositive<int>(sizeArg, sizeArgName, "PageSize cannot be below 1 if a PageIndex greater 0 was provided.");
            }
            else
            {
                // pageIndex 0 actually means: take all!
                NotNegative(sizeArg, sizeArgName);
            }
        }

        /// <summary>
        /// Checks if the argument is null.
        /// </summary>
        public static void CheckArgumentNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks if the parameter is null.
        /// </summary>
        public static void CheckMandatoryOption(this string s, string name)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException(name);
        }

        /// <summary>
        ///     Ensures that the given expression is true
        /// </summary>
        /// <exception cref="System.Exception">Exception thrown if false condition</exception>
        /// <param name="condition">Condition to test/ensure</param>
        /// <param name="message">Message for the exception</param>
        /// <exception cref="System.Exception">Thrown when <paramref name="condition" /> is false</exception>
        public static void That(bool condition, string message = "")
        {
            That<Exception>(condition, message);
        }

        /// <summary>
        ///     Ensures that the given expression is true
        /// </summary>
        /// <typeparam name="TException">Type of exception to throw</typeparam>
        /// <param name="condition">Condition to test/ensure</param>
        /// <param name="message">Message for the exception</param>
        /// <exception cref="TException">Thrown when <paramref name="condition" /> is false</exception>
        /// <remarks><see cref="TException" /> must have a constructor that takes a single string</remarks>
        public static void That<TException>(bool condition, string message = "") where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        /// <summary>
        ///     Ensures given condition is false
        /// </summary>
        /// <typeparam name="TException">Type of exception to throw</typeparam>
        /// <param name="condition">Condition to test</param>
        /// <param name="message">Message for the exception</param>
        /// <exception cref="TException">Thrown when <paramref name="condition" /> is true</exception>
        /// <remarks><see cref="TException" /> must have a constructor that takes a single string</remarks>
        public static void Not<TException>(bool condition, string message = "") where TException : Exception
        {
            That<TException>(!condition, message);
        }

        /// <summary>
        ///     Ensures given condition is false
        /// </summary>
        /// <param name="condition">Condition to test</param>
        /// <param name="message">Message for the exception</param>
        /// <exception cref="System.Exception">Thrown when <paramref name="condition" /> is true</exception>
        public static void Not(bool condition, string message = "")
        {
            Not<Exception>(condition, message);
        }

        /// <summary>
        ///     Ensures given object is not null
        /// </summary>
        /// <param name="value">Value of the object to test for null reference</param>
        /// <param name="message">Message for the Null Reference Exception</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="value" /> is null</exception>
        public static void NotNull(object value, string message = "")
        {
            That<NullReferenceException>(value != null, message);
        }

        /// <summary>
        ///     Ensures given string is not null or empty
        /// </summary>
        /// <param name="value">String value to compare</param>
        /// <param name="message">Message of the exception if value is null or empty</param>
        /// <exception cref="System.Exception">string value is null or empty</exception>
        public static void NotNullOrEmpty(string value, string message = "String cannot be null or empty")
        {
            That(!string.IsNullOrEmpty(value), message);
        }

        /// <summary>
        ///     Ensures given objects are equal
        /// </summary>
        /// <typeparam name="T">Type of objects to compare for equality</typeparam>
        /// <param name="left">First Value to Compare</param>
        /// <param name="right">Second Value to Compare</param>
        /// <param name="message">Message of the exception when values equal</param>
        /// <exception cref="System.Exception">
        ///     Exception is thrown when <paramref cref="left" /> not equal to
        ///     <paramref cref="right" />
        /// </exception>
        /// <remarks>Null values will cause an exception to be thrown</remarks>
        public static void Equal<T>(T left, T right, string message = "Values must be equal")
        {
            That(left != null && right != null && left.Equals(right), message);
        }

        /// <summary>
        ///     Ensures given objects are not equal
        /// </summary>
        /// <typeparam name="T">Type of objects to compare for equality</typeparam>
        /// <param name="left">First Value to Compare</param>
        /// <param name="right">Second Value to Compare</param>
        /// <param name="message">Message of the exception when values equal</param>
        /// <exception cref="System.Exception">Thrown when <paramref cref="left" /> equal to <paramref cref="right" /></exception>
        /// <remarks>Null values will cause an exception to be thrown</remarks>
        public static void NotEqual<T>(T left, T right, string message = "Values must not be equal")
        {
            That(left != null && right != null && !left.Equals(right), message);
        }

        /// <summary>
        ///     Ensures given collection contains a value that satisfied a predicate
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="collection">Collection to test</param>
        /// <param name="predicate">Predicate where one value in the collection must satisfy</param>
        /// <param name="message">Message of the exception if value not found</param>
        /// <exception cref="System.Exception">
        ///     Thrown if collection is null, empty or doesn't contain a value that satisfies <paramref cref="predicate" />
        /// </exception>
        public static void Contains<T>(IEnumerable<T> collection, Func<T, bool> predicate, string message = "")
        {
            That(collection != null && collection.Any(predicate), message);
        }

        /// <summary>
        ///     Ensures ALL items in the given collection satisfy a predicate
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="collection">Collection to test</param>
        /// <param name="predicate">Predicate that ALL values in the collection must satisfy</param>
        /// <param name="message">Message of the exception if not all values are valid</param>
        /// <exception cref="System.Exception">
        ///     Thrown if collection is null, empty or not all values satisfies <paramref cref="predicate" />
        /// </exception>
        public static void Items<T>(IEnumerable<T> collection, Func<T, bool> predicate, string message = "")
        {
            That(collection != null && !collection.Any(x => !predicate(x)), message);
        }

        /// <summary>
        ///     Argument-specific ensure methods
        /// </summary>
        public static class Argument
        {
            /// <summary>
            ///     Ensures given condition is true
            /// </summary>
            /// <param name="condition">Condition to test</param>
            /// <param name="message">Message of the exception if condition fails</param>
            /// <exception cref="System.ArgumentException">
            ///     Thrown if <paramref cref="condition" /> is false
            /// </exception>
            public static void Is(bool condition, string message = "")
            {
                That<ArgumentException>(condition, message);
            }

            /// <summary>
            ///     Ensures given condition is false
            /// </summary>
            /// <param name="condition">Condition to test</param>
            /// <param name="message">Message of the exception if condition is true</param>
            /// <exception cref="System.ArgumentException">
            ///     Thrown if <paramref cref="condition" /> is true
            /// </exception>
            public static void IsNot(bool condition, string message = "")
            {
                Is(!condition, message);
            }

            /// <summary>
            ///     Ensures given value is not null
            /// </summary>
            /// <param name="value">Value to test for null</param>
            /// <param name="paramName">Name of the parameter in the method</param>
            /// <exception cref="System.ArgumentNullException">
            ///     Thrown if <paramref cref="value" /> is null
            /// </exception>
            public static void NotNull(object value, string paramName = "")
            {
                That<ArgumentNullException>(value != null, paramName);
            }

            /// <summary>
            ///     Ensures the given string value is not null or empty
            /// </summary>
            /// <param name="value">Value to test for null or empty</param>
            /// <param name="paramName">Name of the parameter in the method</param>
            /// <exception cref="System.ArgumentException">
            ///     Thrown if <paramref cref="value" /> is null or empty string
            /// </exception>
            public static void NotNullOrEmpty(string value, string paramName = "")
            {
                if (value == null)
                {
                    throw new ArgumentNullException(paramName, "String value cannot be null");
                }

                if (string.Empty.Equals(value))
                {
                    throw new ArgumentException("String value cannot be empty", paramName);
                }
            }
        }
    }
}
