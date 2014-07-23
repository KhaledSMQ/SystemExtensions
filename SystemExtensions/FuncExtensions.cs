using System;

namespace SystemExtensions
{
    public static class FuncExtensions
    {
        /// <summary>
        /// Converts a <see cref="Func&lt;T>"/> delegate without parameters to a <see cref="Lazy&lt;T>"/> object.
        /// </summary>
        /// <typeparam name="T">Type of the return.</typeparam>
        /// <param name="func"><see cref="Func&lt;T>"/> to convert to <see cref="Lazy&lt;T>"/>.</param>
        /// <returns>A lazy instance, that call the function once to get it's value, and stores the result in an internal cache for latter use.</returns>
        public static Lazy<T> ToLazy<T>(this Func<T> func)
        {
            return new Lazy<T>(func);
        }
    }
}
