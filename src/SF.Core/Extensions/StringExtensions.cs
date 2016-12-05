using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SF.Core.Extensions
{
    public static class StringExtensions
    {

        #region String Extensions
        [DebuggerStepThrough]
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// Determines whether the string is null, empty or all whitespace.
        /// </summary>
        [DebuggerStepThrough]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// Like null coalescing operator (??) but including empty strings
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string a, string b)
        {
            return string.IsNullOrEmpty(a) ? b : a;
        }
        /// <summary>
        /// Splits a string into a string array
        /// </summary>
        /// <param name="value">String value to split</param>
        /// <param name="separator">If <c>null</c> then value is searched for a common delimiter like pipe, semicolon or comma</param>
        /// <returns>String array</returns>
        [DebuggerStepThrough]
        public static string[] SplitSafe(this string value, string separator)
        {
            if (string.IsNullOrEmpty(value))
                return new string[0];

            // do not use separator.IsEmpty() here because whitespace like " " is a valid separator.
            // an empty separator "" returns array with value.
            if (separator == null)
            {
                separator = "|";

                if (value.IndexOf(separator) < 0)
                {
                    if (value.IndexOf(';') > -1)
                    {
                        separator = ";";
                    }
                    else if (value.IndexOf(',') > -1)
                    {
                        separator = ",";
                    }
                    else if (value.IndexOf(Environment.NewLine) > -1)
                    {
                        separator = Environment.NewLine;
                    }
                }
            }

            return value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// If <paramref name="a"/> is empty, returns null
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string EmptyToNull(this string a)
        {
            return string.IsNullOrEmpty(a) ? null : a;
        }

        /// <summary>
        /// Equalses the or null empty.
        /// </summary>
        /// <param name="str1">The STR1.</param>
        /// <param name="str2">The STR2.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns></returns>
        public static bool EqualsOrNullEmpty(this string str1, string str2, StringComparison comparisonType)
        {
            return String.Compare(str1 ?? "", str2 ?? "", comparisonType) == 0;
        }

        /// <summary>
        /// Equals invariant
        /// </summary>
        /// <param name="str1">The STR1.</param>
        /// <param name="str2">The STR2.</param>
        /// <returns></returns>
        public static bool EqualsInvariant(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        public static string Truncate(this string value, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + suffix;
        }

        public static string EscapeSearchTerm(this string term)
        {
            char[] specialCharcters = { '+', '-', '!', '(', ')', '{', '}', '[', ']', '^', '"', '~', '*', '?', ':', '\\' };
            var retVal = "";
            //'&&', '||',
            foreach (var ch in term)
            {
                if (specialCharcters.Any(x => x == ch))
                {
                    retVal += "\\";
                }
                retVal += ch;
            }
            retVal = retVal.Replace("&&", @"\&&");
            retVal = retVal.Replace("||", @"\||");
            retVal = retVal.Trim();

            return retVal;
        }

        /// <summary>
        /// Escapes the selector. Query requires special characters to be escaped in query
        /// http://api.jquery.com/category/selectors/
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static string EscapeSelector(this string attribute)
        {
            return Regex.Replace(attribute, string.Format("([{0}])", "/[!\"#$%&'()*+,./:;<=>?@^`{|}~\\]"), @"\\$1");
        }

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
            str = str.Substring(0, str.Length <= 240 ? str.Length : 240).Trim(); // cut and trim it   
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }

        /// <summary>
        /// Only english characters,
        /// Numbers are allowed,  
        /// Dashes are allowed, 
        /// Spaces are replaced by dashes, 
        /// Nothing else is allowed,
        /// Possibly you could replace ;amp by "-and-"
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string MakeFileNameWebSafe(this string fileName)
        {
            string str = fileName.RemoveAccent().ToLower();
            str = str.Replace("&", "-and-");
            str = Regex.Replace(str, @"[^A-Za-z0-9_\-. ]", ""); // invalid chars           
            str = Regex.Replace(str, @"\s+", "-").Trim(); // convert multiple spaces into one dash
            str = str.Substring(0, str.Length <= 240 ? str.Length : 240).Trim(); // cut and trim it   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int ComputeLevenshteinDistance(this string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFromInvariantString(s);
                }
            }
            catch { }
            return result;
        }

        public static string[] LeftJoin(this IEnumerable<string> left, IEnumerable<string> right, string delimiter)
        {
            if (right == null)
            {
                right = Enumerable.Empty<string>();
            }

            return left.Join(right.DefaultIfEmpty(String.Empty), x => true, y => true, (x, y) => String.Join(delimiter, new[] { x, y }.Where(z => !String.IsNullOrEmpty(z)))).ToArray();
        }

        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentException("input");
            }
            return input.First().ToString().ToUpper() + input.Substring(1);
        }


        public static string CamelFriendly(this string camel)
        {
            if (string.IsNullOrWhiteSpace(camel))
                return "";

            var sb = new StringBuilder(camel);

            for (int i = camel.Length - 1; i > 0; i--)
            {
                var current = sb[i];
                if ('A' <= current && current <= 'Z')
                {
                    sb.Insert(i, ' ');
                }
            }

            return sb.ToString();
        }

        public static string Ellipsize(this string text, int characterCount)
        {
            return text.Ellipsize(characterCount, "&#160;&#8230;");
        }

        public static string Ellipsize(this string text, int characterCount, string ellipsis, bool wordBoundary = false)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            if (characterCount < 0 || text.Length <= characterCount)
                return text;

            // search beginning of word
            var backup = characterCount;
            while (characterCount > 0 && text[characterCount - 1].IsLetter())
            {
                characterCount--;
            }

            // search previous word
            while (characterCount > 0 && text[characterCount - 1].IsSpace())
            {
                characterCount--;
            }

            // if it was the last word, recover it, unless boundary is requested
            if (characterCount == 0 && !wordBoundary)
            {
                characterCount = backup;
            }

            var trimmed = text.Substring(0, characterCount);
            return trimmed + ellipsis;
        }

        public static string HtmlClassify(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            var friendlier = text.CamelFriendly();

            var result = new char[friendlier.Length];

            var cursor = 0;
            var previousIsNotLetter = false;
            for (var i = 0; i < friendlier.Length; i++)
            {
                char current = friendlier[i];
                if (IsLetter(current) || (char.IsDigit(current) && cursor > 0))
                {
                    if (previousIsNotLetter && i != 0 && cursor > 0)
                    {
                        result[cursor++] = '-';
                    }

                    result[cursor++] = char.ToLowerInvariant(current);
                    previousIsNotLetter = false;
                }
                else
                {
                    previousIsNotLetter = true;
                }
            }

            return new string(result, 0, cursor);
        }

        public static LocalizedString OrDefault(this string text, LocalizedString defaultValue)
        {
            return string.IsNullOrEmpty(text)
                ? defaultValue
                : new LocalizedString(null, text);
        }

        public static string RemoveTags(this string html, bool htmlDecode = false)
        {
            if (String.IsNullOrEmpty(html))
            {
                return String.Empty;
            }

            var result = new char[html.Length];

            var cursor = 0;
            var inside = false;
            for (var i = 0; i < html.Length; i++)
            {
                char current = html[i];

                switch (current)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (!inside)
                {
                    result[cursor++] = current;
                }
            }

            var stringResult = new string(result, 0, cursor);

            if (htmlDecode)
            {
                stringResult = WebUtility.HtmlDecode(stringResult);
            }

            return stringResult;
        }

        // not accounting for only \r (e.g. Apple OS 9 carriage return only new lines)
        public static string ReplaceNewLinesWith(this string text, string replacement)
        {
            return String.IsNullOrWhiteSpace(text)
                       ? String.Empty
                       : text
                             .Replace("\r\n", "\r\r")
                             .Replace("\n", String.Format(replacement, "\r\n"))
                             .Replace("\r\r", String.Format(replacement, "\r\n"));
        }

        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static byte[] ToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length).
                Where(x => 0 == x % 2).
                Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).
                ToArray();
        }

        private static readonly char[] validSegmentChars = "/?#[]@\"^{}|`<>\t\r\n\f ".ToCharArray();
        public static bool IsValidUrlSegment(this string segment)
        {
            // valid isegment from rfc3987 - http://tools.ietf.org/html/rfc3987#page-8
            // the relevant bits:
            // isegment    = *ipchar
            // ipchar      = iunreserved / pct-encoded / sub-delims / ":" / "@"
            // iunreserved = ALPHA / DIGIT / "-" / "." / "_" / "~" / ucschar
            // pct-encoded = "%" HEXDIG HEXDIG
            // sub-delims  = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            // ucschar     = %xA0-D7FF / %xF900-FDCF / %xFDF0-FFEF / %x10000-1FFFD / %x20000-2FFFD / %x30000-3FFFD / %x40000-4FFFD / %x50000-5FFFD / %x60000-6FFFD / %x70000-7FFFD / %x80000-8FFFD / %x90000-9FFFD / %xA0000-AFFFD / %xB0000-BFFFD / %xC0000-CFFFD / %xD0000-DFFFD / %xE1000-EFFFD
            //
            // rough blacklist regex == m/^[^/?#[]@"^{}|\s`<>]+$/ (leaving off % to keep the regex simple)

            return !segment.Any(validSegmentChars);
        }

        /// <summary>
        /// Generates a valid technical name.
        /// </summary>
        /// <remarks>
        /// Uses a white list set of chars.
        /// </remarks>
        public static string ToSafeName(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            name = RemoveDiacritics(name);
            name = name.Strip(c =>
                !c.IsLetter()
                && !char.IsDigit(c)
                );

            name = name.Trim();

            // don't allow non A-Z chars as first letter, as they are not allowed in prefixes
            while (name.Length > 0 && !IsLetter(name[0]))
            {
                name = name.Substring(1);
            }

            if (name.Length > 128)
                name = name.Substring(0, 128);

            return name;
        }

        /// <summary>
        /// Whether the char is a letter between A and Z or not
        /// </summary>
        public static bool IsLetter(this char c)
        {
            return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
        }

        public static bool IsSpace(this char c)
        {
            return (c == '\r' || c == '\n' || c == '\t' || c == '\f' || c == ' ');
        }

        public static string RemoveDiacritics(this string name)
        {
            string stFormD = name.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char t in stFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string Strip(this string subject, params char[] stripped)
        {
            if (stripped == null || stripped.Length == 0 || string.IsNullOrEmpty(subject))
            {
                return subject;
            }

            var result = new char[subject.Length];

            var cursor = 0;
            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (Array.IndexOf(stripped, current) < 0)
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        public static string Strip(this string subject, Func<char, bool> predicate)
        {
            var result = new char[subject.Length];

            var cursor = 0;
            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (!predicate(current))
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        public static bool Any(this string subject, params char[] chars)
        {
            if (string.IsNullOrEmpty(subject) || chars == null || chars.Length == 0)
            {
                return false;
            }

            Array.Sort(chars);

            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (Array.BinarySearch(chars, current) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool All(this string subject, params char[] chars)
        {
            if (string.IsNullOrEmpty(subject))
            {
                return true;
            }

            if (chars == null || chars.Length == 0)
            {
                return false;
            }

            Array.Sort(chars);

            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (Array.BinarySearch(chars, current) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static string Translate(this string subject, char[] from, char[] to)
        {
            if (string.IsNullOrEmpty(subject))
            {
                return subject;
            }

            if (from == null || to == null)
            {
                throw new ArgumentNullException();
            }

            if (from.Length != to.Length)
            {
                throw new ArgumentNullException(nameof(from), "Parameters must have the same length");
            }

            var map = new Dictionary<char, char>(from.Length);
            for (var i = 0; i < from.Length; i++)
            {
                map[from[i]] = to[i];
            }

            var result = new char[subject.Length];

            for (var i = 0; i < subject.Length; i++)
            {
                var current = subject[i];
                if (map.ContainsKey(current))
                {
                    result[i] = map[current];
                }
                else
                {
                    result[i] = current;
                }
            }

            return new string(result);
        }

        public static string ReplaceAll(this string original, IDictionary<string, string> replacements)
        {
            var pattern = $"{string.Join("|", replacements.Keys)}";
            return Regex.Replace(original, pattern, match => replacements[match.Value]);
        }

        public static string TrimEnd(this string rough, string trim = "")
        {
            if (rough == null)
                return null;

            return rough.EndsWith(trim)
                       ? rough.Substring(0, rough.Length - trim.Length)
                       : rough;
        }



        /// <summary>
        /// Removes special characters from the string so that only Alpha, Numeric, '.' and '_' remain;
        /// </summary>
        /// <param name="str">The identifier.</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// removes any invalid FileName chars in a filename
        /// from http://stackoverflow.com/a/14836763/1755417
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string MakeValidFileName(this string name)
        {
            string invalidChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            string replace = Regex.Replace(name, invalidReStr, "_").Replace(";", "").Replace(",", "");
            return replace;
        }

        /// <summary>
        /// Splits a Camel or Pascal cased identifier into seperate words.
        /// </summary>
        /// <param name="str">The identifier.</param>
        /// <returns></returns>
        public static string SplitCase(this string str)
        {
            if (str == null)
                return null;

            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by any combination of whitespace, comma, semi-colon, or pipe characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="whitespace">if set to <c>true</c> whitespace will be treated as a delimiter</param>
        /// <returns></returns>
        public static string[] SplitDelimitedValues(this string str, bool whitespace = true)
        {
            if (str == null)
                return new string[0];

            string regex = whitespace ? @"[\s\|,;]+" : @"[\|,;]+";

            char[] delimiter = new char[] { ',' };
            return Regex.Replace(str, regex, ",").Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Replaces every instance of oldValue (regardless of case) with the newValue.
        /// (from http://www.codeproject.com/Articles/10890/Fastest-C-Case-Insenstive-String-Replace)
        /// </summary>
        /// <param name="str">The source string.</param>
        /// <param name="oldValue">The value to replace.</param>
        /// <param name="newValue">The value to insert.</param>
        /// <returns></returns>
        public static string ReplaceCaseInsensitive(this string str, string oldValue, string newValue)
        {
            if (str == null)
                return null;

            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = str.ToUpper();
            string upperPattern = oldValue.ToUpper();
            int inc = (str.Length / oldValue.Length) *
                      (newValue.Length - oldValue.Length);
            char[] chars = new char[str.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                                              position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = str[i];
                for (int i = 0; i < newValue.Length; ++i)
                    chars[count++] = newValue[i];
                position0 = position1 + oldValue.Length;
            }
            if (position0 == 0) return str;
            for (int i = position0; i < str.Length; ++i)
                chars[count++] = str[i];
            return new string(chars, 0, count);
        }

        /// <summary>
        /// Replaces every instance of oldValue with newValue.  Will continue to replace
        /// values after each replace until the oldValue does not exist.
        /// </summary>
        /// <param name="str">The source string.</param>
        /// <param name="oldValue">The value to replace.</param>
        /// <param name="newValue">The value to insert.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceWhileExists(this string str, string oldValue, string newValue)
        {
            string newstr = str;

            if (oldValue != newValue)
            {
                while (newstr.Contains(oldValue))
                {
                    newstr = newstr.Replace(oldValue, newValue);
                }
            }

            return newstr;
        }

        /// <summary>
        /// Adds escape character for quotes in a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EscapeQuotes(this string str)
        {
            if (str == null)
                return null;

            return str.Replace("'", "\\'").Replace("\"", "\\\"");
        }

        /// <summary>
        /// Adds Quotes around the specified string and escapes any quotes that are already in the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="QuoteChar">The quote character.</param>
        /// <returns></returns>
        public static string Quoted(this string str, string QuoteChar = "'")
        {
            var result = QuoteChar + str.EscapeQuotes() + QuoteChar;
            return result;
        }

        /// <summary>
        /// Returns the specified number of characters, starting at the left side of the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="length">The desired length.</param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            if (str == null)
            {
                return null;
            }
            else if (str.Length <= length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, length);
            }
        }

        /// <summary>
        /// Truncates a string after a max length and adds ellipsis.  Truncation will occur at first space prior to maxLength.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
                return null;

            if (str.Length <= maxLength)
                return str;

            maxLength -= 3;
            var truncatedString = str.Substring(0, maxLength);
            var lastSpace = truncatedString.LastIndexOf(' ');
            if (lastSpace > 0)
                truncatedString = truncatedString.Substring(0, lastSpace);

            return truncatedString + "...";
        }

        /// <summary>
        /// Removes any non-numeric characters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AsNumeric(this string str)
        {
            return Regex.Replace(str, @"[^0-9]", "");
        }

        /// <summary>
        /// Replaces the last occurrence of a given string with a new value
        /// </summary>
        /// <param name="Source">The string.</param>
        /// <param name="Find">The search parameter.</param>
        /// <param name="Replace">The replacement parameter.</param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        /// <summary>
        /// The true strings for AsBoolean and AsBooleanOrNull.
        /// </summary>
        private static string[] trueStrings = new string[] { "true", "yes", "t", "y", "1" };

        /// <summary>
        /// Returns True for 'True', 'Yes', 'T', 'Y', '1' (case-insensitive).
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="resultIfNullOrEmpty">if set to <c>true</c> [result if null or empty].</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static bool AsBoolean(this string str, bool resultIfNullOrEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return resultIfNullOrEmpty;
            }

            return trueStrings.Contains(str.ToLower());
        }

        /// <summary>
        /// Returns True for 'True', 'Yes', 'T', 'Y', '1' (case-insensitive), null for emptystring/null.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static bool? AsBooleanOrNull(this string str)
        {
            string[] trueStrings = new string[] { "true", "yes", "t", "y", "1" };

            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            return trueStrings.Contains(str.ToLower());
        }

        /// <summary>
        /// Attempts to convert string to an dictionary using the |/comma and ^ delimiter Key/Value syntax.  Returns an empty dictionary if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>                     
        public static System.Collections.Generic.Dictionary<string, string> AsDictionary(this string str)
        {
            var dictionary = new System.Collections.Generic.Dictionary<string, string>();
            string[] nameValues = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            // If we haven't found any pipes, check for commas
            if (nameValues.Count() == 1 && nameValues[0] == str)
            {
                nameValues = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            foreach (string nameValue in nameValues)
            {
                string[] nameAndValue = nameValue.Split(new char[] { '^' }, 2);
                if (nameAndValue.Count() == 2)
                {
                    dictionary[nameAndValue[0]] = nameAndValue[1];
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Attempts to convert string to an dictionary using the |/comma and ^ delimiter Key/Value syntax.  Returns null if unsuccessful.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough()]
        public static System.Collections.Generic.Dictionary<string, string> AsDictionaryOrNull(this string str)
        {
            var dictionary = AsDictionary(str);
            if (dictionary.Count() > 0)
            {
                return dictionary;
            }
            return null;
        }

        /// <summary>
        /// Masks the specified value if greater than 4 characters (such as a credit card number).
        /// For example, the return string becomes "************6789".
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Masked(this string value)
        {
            if (value.Length > 4)
            {
                return string.Concat(new string('*', 12), value.Substring(value.Length - 4));
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Ensures the trailing backslash. Handy when combining folder paths.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EnsureTrailingBackslash(this string value)
        {
            return value.TrimEnd(new char[] { '\\', '/' }) + "\\";
        }

        /// <summary>
        /// Ensures the trailing forward slash. Handy when combining url paths.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EnsureTrailingForwardslash(this string value)
        {
            return value.TrimEnd(new char[] { '\\', '/' }) + "/";
        }

        /// <summary>
        /// Ensures the leading forward slash. Handy when combining url paths.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveLeadingForwardslash(this string value)
        {
            return value.TrimStart(new char[] { '/' });
        }

        /// <summary>
        /// Evaluates string, and if null or empty, returns nullValue instead.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="nullValue">The null value.</param>
        /// <returns></returns>
        public static string IfEmpty(this string value, string nullValue)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : nullValue;
        }

        /// <summary>
        /// Replaces special Microsoft Word chars with standard chars
        /// For example, smart quotes will be replaced with apostrophes
        /// from http://www.andornot.com/blog/post/Replace-MS-Word-special-characters-in-javascript-and-C.aspx
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string ReplaceWordChars(this string text)
        {
            var s = text;
            // smart single quotes and apostrophe
            s = Regex.Replace(s, "[\u2018\u2019\u201A]", "'");
            // smart double quotes
            s = Regex.Replace(s, "[\u201C\u201D\u201E]", "\"");
            // ellipsis
            s = Regex.Replace(s, "\u2026", "...");
            // dashes
            s = Regex.Replace(s, "[\u2013\u2014]", "-");
            // circumflex
            s = Regex.Replace(s, "\u02C6", "^");
            // open angle bracket
            s = Regex.Replace(s, "\u2039", "<");
            // close angle bracket
            s = Regex.Replace(s, "\u203A", ">");
            // spaces
            s = Regex.Replace(s, "[\u02DC\u00A0]", " ");

            return s;
        }

        /// <summary>
        /// Returns a list of KeyValuePairs from a serialized list of Rock KeyValuePairs (e.g. 'Item1^Value1|Item2^Value2')
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> ToKeyValuePairList(this string input)
        {
            List<KeyValuePair<string, object>> keyPairs = new List<KeyValuePair<string, object>>();

            if (!string.IsNullOrWhiteSpace(input))
            {
                var items = input.Split('|');

                foreach (var item in items)
                {
                    var parts = item.Split('^');
                    if (parts.Length == 2)
                    {
                        keyPairs.Add(new KeyValuePair<string, object>(parts[0], parts[1]));
                    }
                }
            }

            return keyPairs;
        }

        /// <summary>
        /// Removes the spaces.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string RemoveSpaces(this string input)
        {
            return input.Replace(" ", "");
        }

        /// <summary>
		/// Determines whether this instance and another specified System.String object have the same value.
		/// </summary>
		/// <param name="value">The string to check equality.</param>
		/// <param name="comparing">The comparing with string.</param>
		/// <returns>
		/// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
		/// </returns>
		[DebuggerStepThrough]
        public static bool IsCaseSensitiveEqual(this string value, string comparing)
        {
            return string.CompareOrdinal(value, comparing) == 0;
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="value">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsCaseInsensitiveEqual(this string value, string comparing)
        {
            return string.Compare(value, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
		/// Formats a string to an invariant culture
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="objects">The objects.</param>
		/// <returns></returns>
		[DebuggerStepThrough]
        public static string FormatInvariant(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.InvariantCulture, format, objects);
        }

        /// <summary>
        /// Formats a string to the current culture.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string FormatCurrent(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.CurrentCulture, format, objects);
        }
        /// <summary>Splits a string into two strings</summary>
        /// <returns>true: success, false: failure</returns>
        [DebuggerStepThrough]
        public static bool SplitToPair(this string value, out string strLeft, out string strRight, string delimiter)
        {
            int idx = -1;
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(delimiter) || (idx = value.IndexOf(delimiter)) == -1)
            {
                strLeft = value;
                strRight = "";
                return false;
            }

            strLeft = value.Substring(0, idx);
            strRight = value.Substring(idx + delimiter.Length);

            return true;
        }


        /// <summary>Debug.WriteLine</summary>
        [DebuggerStepThrough]
        public static void Dump(this string value, bool appendMarks = false)
        {
            Debug.WriteLine(value);
            Debug.WriteLineIf(appendMarks, "------------------------------------------------");
        }
        #endregion String Extensions
    }
}
