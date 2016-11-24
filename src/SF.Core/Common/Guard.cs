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
    public class Guard
    {
        private const string AgainstMessage = "Assertion evaluation failed with 'false'.";
        private const string ImplementsMessage = "Type '{0}' must implement type '{1}'.";
        private const string InheritsFromMessage = "Type '{0}' must inherit from type '{1}'.";
        private const string IsTypeOfMessage = "Type '{0}' must be of type '{1}'.";
        private const string IsEqualMessage = "Compared objects must be equal.";
        private const string IsPositiveMessage = "Argument '{0}' must be a positive value. Value: '{1}'.";
        private const string IsTrueMessage = "True expected for '{0}' but the condition was False.";
        private const string NotNegativeMessage = "Argument '{0}' cannot be a negative value. Value: '{1}'.";

        private Guard()
        {
        }
        #region 3.0

        [DebuggerStepThrough]
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
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

        #endregion
    }
}
