using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SystemExtensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the first element in a sequence that does not satisfy a specified condition.
        /// </summary>
        /// <returns>
        /// The first element in the sequence that does not pass the test in the specified predicate function.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a complement condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.-or-The source sequence is empty.</exception>
        public static TSource FirstNon<TSource>(this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return source.First(t => !predicate(t));
        }

        /// <summary>
        /// Returns the first element in a sequence that does not satisfy a specified condition or a default value if no such element is found.
        /// </summary>
        /// <returns>
        /// The first element in the sequence that does not pass the test in the specified predicate function, or default(<paramref name="TSource"/>) if no such element is found.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a complement condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.-or-The source sequence is empty.</exception>
        public static TSource FirstOrDefaultNon<TSource>(this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return source.FirstOrDefault(t => !predicate(t));
        }

        /// <summary>
        /// Returns the only element in a sequence that does not satisfy a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <returns>
        /// The single element in the sequence that does not pass the test in the specified predicate function.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a complement condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.-or-The source sequence is empty.</exception>
        public static TSource SingleNon<TSource>(this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return source.Single(t => !predicate(t));
        }

        /// <summary>
        /// Returns the first element in a sequence that does not satisfy a specified condition or a default value if no such element is found, and throws an exception if more than one such element exists.
        /// </summary>
        /// <returns>
        /// The single element in the sequence that does not pass the test in the specified predicate function, or default(<paramref name="TSource"/>) if no such element is found.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a complement condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.-or-The source sequence is empty.</exception>
        public static TSource SingleOrDefaultNon<TSource>(this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return source.SingleOrDefault(t => !predicate(t));
        }

        /// <summary>
        /// Filters null values out, leaving only non-null values in the sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the sequence.</typeparam>
        /// <param name="source">Source enumeration to be filtered.</param>
        /// <returns>A sequence that does not contain nulls.</returns>
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            return source.Where(e => e != null);
        }
    }
}