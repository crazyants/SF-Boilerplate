/*******************************************************************************
* 命名空间: SF.Core.Extensions
*
* 功 能： N/A
* 类 名： ConversionExtensions
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/24 14:25:26 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Common;
using SF.Core.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SF.Core.Extensions
{
    public static class ConversionExtensions
    {
        #region Object

        public static T Convert<T>(this object value)
        {
            return (T)(Convert(value, typeof(T)) ?? default(T));
        }

        public static T Convert<T>(this object value, T defaultValue)
        {
            return (T)(Convert(value, typeof(T)) ?? defaultValue);
        }

        public static T Convert<T>(this object value, CultureInfo culture)
        {
            return (T)(Convert(value, typeof(T), culture) ?? default(T));
        }

        public static T Convert<T>(this object value, T defaultValue, CultureInfo culture)
        {
            return (T)(Convert(value, typeof(T), culture) ?? defaultValue);
        }

        public static object Convert(this object value, Type to)
        {
            return value.Convert(to, CultureInfo.InvariantCulture);
        }

        public static object Convert(this object value, Type to, CultureInfo culture)
        {
            Guard.NotNull(to, nameof(to));

            if (value == null || value == DBNull.Value || to.IsInstanceOfType(value))
            {
                return value == DBNull.Value ? null : value;
            }

            Type from = value.GetType();

            if (culture == null)
            {
                culture = CultureInfo.InvariantCulture;
            }

            // get a converter for 'to' (value -> to)
            var converter = TypeConverterFactory.GetConverter(to);
            if (converter != null && converter.CanConvertFrom(from))
            {
                return converter.ConvertFrom(culture, value);
            }

            // try the other way round with a 'from' converter (to <- from)
            converter = TypeConverterFactory.GetConverter(from);
            if (converter != null && converter.CanConvertTo(to))
            {
                return converter.ConvertTo(culture, null, value, to);
            }

            // use Convert.ChangeType if both types are IConvertible
            if (value is IConvertible && typeof(IConvertible).IsAssignableFrom(to))
            {
                return System.Convert.ChangeType(value, to, culture);
            }

            throw ErrorHelper.InvalidCast(from, to);
        }

        #endregion

        #region int

        public static char ToHex(this int value)
        {
            if (value <= 9)
            {
                return (char)(value + 48);
            }
            return (char)((value - 10) + 97);
        }

        #endregion

        #region  Numeric
        public static string ToFileSizeDisplay(this int i)
        {
            return ToFileSizeDisplay((long)i, 2);
        }

        public static string ToFileSizeDisplay(this int i, int decimals)
        {
            return ToFileSizeDisplay((long)i, decimals);
        }

        public static string ToFileSizeDisplay(this long i)
        {
            return ToFileSizeDisplay(i, 2);
        }

        public static string ToFileSizeDisplay(this long i, int decimals)
        {
            if (i < 1024 * 1024 * 1024) // 1 GB
            {
                string value = Math.Round((decimal)i / 1024m / 1024m, decimals).ToString("N" + decimals);
                if (decimals > 0 && value.EndsWith(new string('0', decimals)))
                    value = value.Substring(0, value.Length - decimals - 1);

                return String.Concat(value, " MB");
            }
            else
            {
                string value = Math.Round((decimal)i / 1024m / 1024m / 1024m, decimals).ToString("N" + decimals);
                if (decimals > 0 && value.EndsWith(new string('0', decimals)))
                    value = value.Substring(0, value.Length - decimals - 1);

                return String.Concat(value, " GB");
            }
        }

        public static string ToOrdinal(this int num)
        {
            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString("#,###0") + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString("#,###0") + "st";
                case 2:
                    return num.ToString("#,###0") + "nd";
                case 3:
                    return num.ToString("#,###0") + "rd";
                default:
                    return num.ToString("#,###0") + "th";
            }
        }
        #endregion

        #region String

        public static T SafeParse<T>(string value, T defaultValue)
        where T : struct
        {
            T result;

            if (!Enum.TryParse(value, out result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// Attempts to convert string to integer.  Returns 0 if unsuccessful.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static int AsInteger(this string str, int defaultValue = 0)
        {
            return str.AsIntegerOrNull(defaultValue) ?? 0;
        }

        /// <summary>
        /// Attempts to convert string to an integer.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static int? AsIntegerOrNull(this string str, int defaultValue = 0)
        {
            var value = defaultValue;
            if (int.TryParse(str, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to convert string to integer.  Returns 0 if unsuccessful.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static long AsLong(this string str, long defaultValue = 0)
        {
            return str.AsLongOrNull(defaultValue) ?? 0;
        }

        /// <summary>
        /// Attempts to convert string to an integer.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static long? AsLongOrNull(this string str, long defaultValue = 0)
        {
            var value = defaultValue;
            if (long.TryParse(str, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to convert string to Guid.  Returns Guid.Empty if unsuccessful.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static Guid AsGuid(this string str)
        {
            return str.AsGuidOrNull() ?? Guid.Empty;
        }

        /// <summary>
        /// Attempts to convert string to Guid.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static Guid? AsGuidOrNull(this string str)
        {
            Guid value;
            if (Guid.TryParse(str, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines whether the specified unique identifier is Guid.Empty.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid guid)
        {
            return guid.Equals(Guid.Empty);
        }

        /// <summary>
        /// Attempts to convert string to decimal.  Returns 0 if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static decimal AsDecimal(this string str)
        {
            return str.AsDecimalOrNull() ?? 0;
        }

        /// <summary>
        /// Attempts to convert string to decimal.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static decimal? AsDecimalOrNull(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                // strip off non numeric and characters (for example, currency symbols)
                str = Regex.Replace(str, @"[^0-9\.-]", "");
            }

            decimal value;
            if (decimal.TryParse(str, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to convert string to double.  Returns 0 if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static double AsDouble(this string str)
        {
            return str.AsDoubleOrNull() ?? 0;
        }

        /// <summary>
        /// Attempts to convert string to double.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static double? AsDoubleOrNull(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                // strip off non numeric and characters (for example, currency symbols)
                str = Regex.Replace(str, @"[^0-9\.-]", "");
            }

            double value;
            if (double.TryParse(str, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to convert string to TimeSpan.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static TimeSpan? AsTimeSpan(this string str)
        {
            TimeSpan value;
            if (TimeSpan.TryParse(str, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the value to Type, or if unsuccessful, returns the default value of Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T AsType<T>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return converter.IsValid(value)
                ? (T)converter.ConvertFrom(value)
                : default(T);
        }

        public static DateTime? AsDateTime(this string value, DateTime? defaultValue)
        {
            return value.AsDateTime(null, defaultValue);
        }

        public static DateTime? AsDateTime(this string value, string[] formats, DateTime? defaultValue)
        {
            return value.AsDateTime(formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces, defaultValue);
        }

        public static DateTime? AsDateTime(this string value, string[] formats, IFormatProvider provider, DateTimeStyles styles, DateTime? defaultValue)
        {
            DateTime result;

            if (formats.IsNullOrEmpty())
            {
                if (DateTime.TryParse(value, provider, styles, out result))
                {
                    return result;
                }
            }

            if (DateTime.TryParseExact(value, formats, provider, styles, out result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// Parse ISO-8601 UTC timestamp including milliseconds.
        /// </summary>
        /// <remarks>
        /// Dublicate can be found in HmacAuthentication class.
        /// </remarks>
        public static DateTime? AsDateTimeIso8601(this string value)
        {
            if (value.HasValue())
            {
                DateTime dt;
                if (DateTime.TryParseExact(value, "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dt))
                    return dt;

                if (DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dt))
                    return dt;
            }
            return null;
        }

        [DebuggerStepThrough]
        public static Version ToVersion(this string value, Version defaultVersion = null)
        {
            try
            {
                return new Version(value);
            }
            catch
            {
                return defaultVersion ?? new Version("1.0");
            }
        }

        #endregion

        #region Stream

        public static byte[] ToByteArray(this Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            if (stream is MemoryStream)
            {
                return ((MemoryStream)stream).ToArray();
            }
            else
            {
                using (var streamReader = new MemoryStream())
                {
                    stream.CopyTo(streamReader);
                    return streamReader.ToArray();
                }
            }
        }

        public static async Task<byte[]> ToByteArrayAsync(this Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            if (stream is MemoryStream)
            {
                return ((MemoryStream)stream).ToArray();
            }
            else
            {
                using (var streamReader = new MemoryStream())
                {
                    await stream.CopyToAsync(streamReader);
                    return streamReader.ToArray();
                }
            }
        }

        public static string AsString(this Stream stream)
        {
            return stream.AsString(Encoding.UTF8);
        }

        public static Task<string> AsStringAsync(this Stream stream)
        {
            return stream.AsStringAsync(Encoding.UTF8);
        }

        public static string AsString(this Stream stream, Encoding encoding)
        {
            Guard.NotNull(encoding, nameof(encoding));

            // convert stream to string
            string result;

            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        public static Task<string> AsStringAsync(this Stream stream, Encoding encoding)
        {
            Guard.NotNull(encoding, nameof(encoding));

            // convert stream to string
            Task<string> result;

            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                result = sr.ReadToEndAsync();
            }

            return result;
        }

        #endregion

   
    }
}
