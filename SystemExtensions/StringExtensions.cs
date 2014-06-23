using System;

namespace SystemExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether a string ends with another substring.
        /// </summary>
        /// <param name="str">The string being tested.</param>
        /// <param name="value">The desired string ending.</param>
        /// <returns>True if `str` end with `value`; otherwise False.</returns>
        public static bool EndsWithInvariantIgnoreCase(this string str, string value)
        {
            if (str == null)
                return false;

            return str.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}