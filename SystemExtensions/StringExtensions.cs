using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SystemExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the specified starting from a string. The removal happens once:
        /// if the source contains multiple repeated beginnings, only one will be removed.
        /// </summary>
        /// <param name="source">String to remove the beginnings from.</param>
        /// <param name="start">The beginning to be removed from the source string.</param>
        /// <returns>A new string without the specified beginning.</returns>
        [NotNull]
        public static string RemoveStart([NotNull] this string source, [NotNull] string start)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.StartsWith(start) ? source.Substring(start.Length) : source;
        }

        /// <summary>
        /// Removes the specified ending from a string. The removal happens once:
        /// if the source contains multiple repeated endings, only one will be removed.
        /// </summary>
        /// <param name="source">String to remove the endings from.</param>
        /// <param name="end">The ending string to be removed from the source string.</param>
        /// <returns>A new string without the specified ending.</returns>
        [NotNull]
        public static string RemoveEnd([NotNull] this string source, [NotNull] string end)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.EndsWith(end) ? source.Substring(0, end.Length) : source;
        }

        /// <summary>
        /// Ensures that the string starts with the given start.
        /// </summary>
        /// <param name="source">String to remove the endings of.</param>
        /// <param name="start">The ending string to be ensured in the source string.</param>
        /// <returns>A string that corresponds to the source string, ending with the ending string.</returns>
        [NotNull]
        public static string EnsureStart([NotNull] this string source, [NotNull] string start)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return !source.StartsWith(start) ? start + source : source;
        }

        /// <summary>
        /// Ensures that the string starts with the given end.
        /// </summary>
        /// <param name="source">String to remove the endings of.</param>
        /// <param name="end">The ending string to be ensured in the source string.</param>
        /// <returns>A string that corresponds to the source string, ending with the ending string.</returns>
        [NotNull]
        public static string EnsureEnd([NotNull] this string source, [NotNull] string end)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return !source.EndsWith(end) ? source + end : source;
        }

        /// <summary>
        /// Determines whether a string ends with another substring.
        /// </summary>
        /// <param name="source">The string being tested.</param>
        /// <param name="value">The desired string ending.</param>
        /// <returns>True if `source` end with `value`; otherwise False.</returns>
        public static bool EndsWithInvariantIgnoreCase(this string source, string value)
        {
            if (source == null)
                return false;

            return source.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns a substring of this string, without throwing exceptions when `start` or `length` are out of bounds.
        /// Out of bounds characters are replaced by the padding character.
        /// </summary>
        /// <param name="source">String to get the substring from.</param>
        /// <param name="start">Starting index where the substring start.</param>
        /// <param name="length">Length of the substring to get.</param>
        /// <param name="paddingFunc">Padding <see cref="Func{Int32,Char}"/> that returns characters to fill out of bounds spaces.</param>
        /// <returns>A substring of this string.</returns>
        public static string LooseSubstring(this string source, int start, int length, Func<int, char> paddingFunc)
        {
            if (length < 0)
                throw new InvalidOperationException("`length` must be zero or a positive number.");

            var start1 = Math2.MinMax(start, 0, source.Length);
            var length1 = Math2.MinMax(start + length, 0, source.Length) - start1;
            var b = new StringBuilder(length);

            var end = start + length;
            var st0 = Math.Min(start1, end);
            for (int it = start; it < st0; it++)
                b.Append(paddingFunc(it));

            b.Append(source, start1, length1);

            for (int it = Math.Max(source.Length, start); it < end; it++)
                b.Append(paddingFunc(it));

            var result = b.ToString();
            return result;
        }

        /// <summary>
        /// Returns a substring of this string, without throwing exceptions when `start` or `length` are out of bounds.
        /// Out of bounds characters are replaced by the padding character.
        /// </summary>
        /// <param name="source">String to get the substring from.</param>
        /// <param name="start">Starting index where the substring start.</param>
        /// <param name="length">Length of the substring to get.</param>
        /// <param name="padding">Padding character to fill out of bounds spaces.</param>
        /// <returns>A substring of this string.</returns>
        public static string LooseSubstring(this string source, int start, int length, char padding)
        {
            return LooseSubstring(source, start, length, padding, padding);
        }

        /// <summary>
        /// Returns a substring of this string, without throwing exceptions when `start` or `length` are out of bounds.
        /// Out of bounds characters are replaced by the padding character.
        /// </summary>
        /// <param name="source">String to get the substring from.</param>
        /// <param name="start">Starting index where the substring start.</param>
        /// <param name="length">Length of the substring to get.</param>
        /// <param name="paddingStart">Padding character to fill out of bounds spaces with negative indexes.</param>
        /// <param name="paddingEnd">Padding character to fill out of bounds spaces with positive indexes.</param>
        /// <returns>A substring of this string.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string LooseSubstring(this string source, int start, int length, char paddingStart, char paddingEnd)
        {
            if (length < 0)
                throw new InvalidOperationException("`length` must be zero or a positive number.");

            var start1 = Math2.MinMax(start, 0, source.Length);
            var length1 = Math2.MinMax(start + length, 0, source.Length) - start1;

            var b = new StringBuilder(Math.Max(length, 1));
            b.Append(paddingStart, Math.Min(Math.Max(0, start1 - start), length));
            b.Append(source, start1, length1);
            b.Append(paddingEnd, length - b.Length);
            var result = b.ToString();
            return result;
        }

        /// <summary>
        /// Returns a substring of this string, without throwing exceptions when `start` or `length` are out of bounds.
        /// Out of bounds characters are trimmed.
        /// </summary>
        /// <param name="source">String to get the substring from.</param>
        /// <param name="start">Starting index where the substring start.</param>
        /// <param name="length">Length of the substring to get.</param>
        /// <returns>A substring of this string.</returns>
        public static string LooseSubstring(this string source, int start, int length)
        {
            if (length < 0)
                throw new InvalidOperationException("`length` must be zero or a positive number.");

            var start1 = Math2.MinMax(start, 0, source.Length);
            var length1 = Math2.MinMax(start + length, 0, source.Length) - start1;

            var result = source.Substring(start1, length1);
            return result;
        }

        /// <summary>
        /// Replaces fields in the text with the corresponding value in the object.
        /// Fields are denoted by "{PropertyPath}".
        /// The PropertyPath can contain a property name or a property path using '.' operator.
        /// </summary>
        /// <param name="source"> String to replace items from. </param>
        /// <param name="rootObj"> The object to get values from. </param>
        /// <param name="formatter"> Formatter delegate that converts objects to string. </param>
        /// <returns>The string resulting from the replacements.</returns>
        [NotNull]
        public static string ReflectionReplace(
            [NotNull] this string source,
            [NotNull] object rootObj,
            Func<object, string> formatter = null)
        {
            // TODO: method name is an issue? options: ReflectionReplace, FormatWithObject
            // supported: properties, indirections
            // unsupported: indexer, dynamic, methods, operators, extensions, statics
            if (source == null)
                throw new ArgumentNullException("source");

            if (rootObj == null)
                throw new ArgumentNullException("rootObj");

            formatter = formatter ?? (o => string.Format("{0}", o));

            var result = FieldReplace(source, key => formatter(rootObj.Evaluate(key, ObjectExtensions.DlrPropertyEvaluator)));
            return result;
        }

        /// <summary>
        /// Replaces regular expression matches of a source string, by using a match evaluator.
        /// The matches collection must contain non-overlapping matches,
        /// and also, the matches must be based on the source string.
        /// </summary>
        /// <param name="source">Source string where replacements will happen.</param>
        /// <param name="matches">Matches from the source string, indicating the positions to replace text.</param>
        /// <param name="evaluator">Evaluator that can tell what text replaces each match in the source text.</param>
        /// <returns>A new string with the positions indicated by the matches replaced by text given by the evaluator.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [NotNull]
        public static string ReplaceMatches(this string source, MatchCollection matches, MatchEvaluator evaluator)
        {
            var sb = new StringBuilder(source.Length);
            var prevPos = 0;
            foreach (var match in matches.Cast<Match>())
            {
                sb.Append(source, prevPos, match.Index - prevPos);
                prevPos = match.Index + match.Length;

                var replacement = evaluator(match);
                sb.Append(replacement);
            }

            sb.Append(source, prevPos, source.Length - prevPos);

            return sb.ToString();
        }

        /// <summary>
        /// Replaces fields in the text with the corresponding values in the dictionary.
        /// Fields are denoted by "{KeyString}".
        /// </summary>
        /// <typeparam name="TValue">Type of items of the dictionary.</typeparam>
        /// <param name="source">Source string where replacements will happen.</param>
        /// <param name="dictionary">Dictionary containing the values that will be used in the string.</param>
        /// <param name="formatter">Delegate that converts dictionary values to string when needed.</param>
        /// <param name="whenNotFound">Handles fields that have no corresponding key in the dictionary.</param>
        /// <returns>A string with the fields replaced by values in the dictionary.</returns>
        [NotNull]
        public static string DictionaryReplace<TValue>(
            [NotNull] this string source,
            [NotNull] IDictionary<string, TValue> dictionary,
            Func<TValue, string> formatter = null,
            Func<string, string> whenNotFound = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            Contract.EndContractBlock();

            formatter = formatter ?? (o => string.Format("{0}", o));

            var result = FieldReplace(
                source,
                key =>
                {
                    TValue value;
                    if (!dictionary.TryGetValue(key, out value))
                    {
                        const string error = "The given key is not present in the dictionary.";
                        if (whenNotFound == null)
                            throw new InvalidOperationException(
                                string.Format("{1} Key: {0}", key, error));

                        return whenNotFound(key);
                    }

                    return formatter(value);
                });

            return result;
        }

        /// <summary>
        /// Replaces fields denoted by "{field_name}" in the string,
        /// with strings returned from an evaluator delegate.
        /// The source string accepts "{{" and "}}" as escapes for single braces.
        /// </summary>
        /// <param name="source">Source string containing the fields.</param>
        /// <param name="evaluator">Evaluator delegate that returns replacements given the field names.</param>
        /// <returns>A new string with the replaced fields.</returns>
        public static string FieldReplace(string source, Func<string, string> evaluator)
        {
            // TODO: method name is an issue? options: FieldReplace, FormatFields
            var matches = Regex.Matches(source, @"\{\{|\}\}|\{([^\{\}]*?)\}|\{|\}");

            // validating source string
            var invalidMatches = matches.Cast<Match>().Where(m => "{}".Contains(m.Value)).ToList();
            if (invalidMatches.Any())
            {
                var x = string.Join(
                    "\n",
                    invalidMatches.Select(
                        (m, i) => string.Format(
                            "Error {0} at offset {1} near {2}",
                            i,
                            m.Index,
                            source.LooseSubstring(m.Index - 5, m.Length + 10))));

                throw new Exception(
                    string.Format(
                        "Invalid source string:\n{0}\n{1}",
                        source,
                        x));
            }

            // replacing the matches
            var result = source.ReplaceMatches(
                matches,
                match =>
                {
                    if (match.Length == 2)
                    {
                        if (match.Value == "{{")
                            return "{";

                        if (match.Value == "}}")
                            return "}";
                    }

                    return evaluator(match.Groups[1].Value);
                });

            return result;
        }

        /// <summary>
        /// Splits a string into enumerable parts.
        /// This is different from Split method, because it is lazily enumerated.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="separator">Separator string.</param>
        /// <param name="comparisonType">Optional comparison type to find separators inside the string to split.</param>
        /// <param name="options">Optional splitting options.</param>
        /// <returns>An enumeration that returns the next piece iteratively.</returns>
        public static IEnumerable<string> EnumerableSplit(
            [NotNull] this string str,
            [NotNull] string separator,
            StringComparison comparisonType = StringComparison.InvariantCulture,
            StringSplitOptions options = StringSplitOptions.None)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (separator == null)
                throw new ArgumentNullException("separator");

            return EnumerableSplitInternal(str, separator, comparisonType, options);
        }

        private static IEnumerable<string> EnumerableSplitInternal(
            [NotNull] string str,
            [NotNull] string value,
            StringComparison comparisonType,
            StringSplitOptions options)
        {
            var prevPos = 0;
            while (true)
            {
                var nextPos = str.IndexOf(value, prevPos, comparisonType);

                if (nextPos < 0)
                    yield break;

                if (options != StringSplitOptions.RemoveEmptyEntries || nextPos != prevPos)
                    yield return str.Substring(prevPos, nextPos - prevPos);

                prevPos = nextPos + value.Length;
            }
        }

        /// <summary>
        /// Splits a string into enumerable parts.
        /// This is different from Split method, because it is lazily enumerated.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="separator">Separator character.</param>
        /// <param name="options">Optional splitting options.</param>
        /// <returns>An enumeration that returns the next piece iteratively.</returns>
        public static IEnumerable<string> EnumerableSplit(
            [NotNull] this string str,
            char separator,
            StringSplitOptions options = StringSplitOptions.None)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return EnumerableSplitInternal(str, separator, options);
        }

        private static IEnumerable<string> EnumerableSplitInternal(
            [NotNull] string str,
            char separator,
            StringSplitOptions options)
        {
            var prevPos = 0;
            while (true)
            {
                var nextPos = str.IndexOf(separator, prevPos);

                if (nextPos < 0)
                    yield break;

                if (options != StringSplitOptions.RemoveEmptyEntries || nextPos != prevPos)
                    yield return str.Substring(prevPos, nextPos - prevPos);

                prevPos = nextPos + 1;
            }
        }
    }
}