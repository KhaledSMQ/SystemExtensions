using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemExtensions
{
    /// <summary>
    /// Extensions that apply to all classes and/or structs,
    /// but not meaning every extension works with all kinds of types.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the objects that composes a path to an object,
        /// in an hiearachycal structure, given a target object,
        /// and a delegate that knows how to get the next (or previous, or parent, you name it) object.
        /// </summary>
        /// <typeparam name="T">The type of objects that composes each path node.</typeparam>
        /// <param name="target">The target object, to get the path for.</param>
        /// <param name="nextFunc">Delegate that is able to get the next object in the path.</param>
        /// <returns>An enumeration that gets each item in the resulting path.</returns>
        /// <remarks>
        /// The path is lazily evaluated. If the structure of objects can change
        /// while enumerating path elements, then the enumeration should be materialized into a list or an array.
        /// </remarks>
        public static IEnumerable<T> GetPath<T>(this T target, Func<T, T> nextFunc)
            where T : class
        {
            var current = target;
            while (current != null)
            {
                yield return current;
                current = nextFunc(current);
            }
        }

        /// <summary>
        /// Gets the objects that composes a path to an object,
        /// in an hiearachycal structure, given a target object,
        /// a delegate that knows how to get the next (or previous, or parent, you name it) object,
        /// and another delegate that can tell whether a given object represents the end of the path.
        /// </summary>
        /// <typeparam name="T">The type of objects that composes each path node.</typeparam>
        /// <param name="target">The target object, to get the path for.</param>
        /// <param name="nextFunc">Delegate that is able to get the next object in the path.</param>
        /// <param name="pathEnd">Delegate used to recognize the end of the path.</param>
        /// <returns>An enumeration that gets each item in the resulting path.</returns>
        /// <remarks>
        /// The path is lazily evaluated. If the structure of objects can change
        /// while enumerating path elements, then the enumeration should be materialized into a list or an array.
        /// </remarks>
        public static IEnumerable<T> GetPath<T>(this T target, Func<T, T> nextFunc, Predicate<T> pathEnd)
        {
            var current = target;
            while (!pathEnd(current))
            {
                yield return current;
                current = nextFunc(current);
            }
        }

        /// <summary>
        /// Does something with an object, and returns the result.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <typeparam name="TResult">Type of the resulting value.</typeparam>
        /// <param name="target">The target object to apply the delegate to.</param>
        /// <param name="withFunc">The delegate to be called passing the target object.</param>
        /// <returns>Returns the result of the delegate.</returns>
        public static TResult With<T, TResult>(this T target, Func<T, TResult> withFunc)
        {
            return withFunc(target);
        }

        /// <summary>
        /// Does multiple things with an object, and returns multiple results.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <typeparam name="TResult">Type of the resulting value.</typeparam>
        /// <param name="target">The target object to apply the delegate to.</param>
        /// <param name="withFuncs">The delegates to be called passing the target object.</param>
        /// <returns>Returns the results of the delegates.</returns>
        public static IEnumerable<TResult> With<T, TResult>(this T target, params Func<T, TResult>[] withFuncs)
        {
            return withFuncs.Select(withFunc => withFunc(target));
        }

        /// <summary>
        /// Does something with an object.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="target">The target object to apply the delegate to.</param>
        /// <param name="withFunc">The delegate to be called passing the target object.</param>
        public static void With<T>(this T target, Action<T> withFunc)
        {
            withFunc(target);
        }

        /// <summary>
        /// Does multiple things with an object.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="target">The target object to apply the delegate to.</param>
        /// <param name="withFuncs">The delegates to be called passing the target object.</param>
        public static void With<T>(this T target, params Action<T>[] withFuncs)
        {
            foreach (var withFunc in withFuncs)
                withFunc(target);
        }
    }
}