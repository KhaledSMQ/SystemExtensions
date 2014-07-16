using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.CSharp.RuntimeBinder;

namespace SystemExtensions
{
    /// <summary>
    /// Extensions that apply to all classes and/or structs.
    /// Does not mean every extension works with all kinds of objects, but most do.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the objects that composes a path to an object,
        /// in an hierarchical structure, given a target object,
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
        /// in an hierarchical structure, given a target object,
        /// a delegate that knows how to get the next (or previous, or parent, you name it) object,
        /// and another delegate that can tell whether a given object represents a path node.
        /// </summary>
        /// <typeparam name="T">The type of objects that composes each path node.</typeparam>
        /// <param name="target">The target object, to get the path for.</param>
        /// <param name="nextFunc">Delegate that is able to get the next object in the path.</param>
        /// <param name="pathInclude">Delegate used to recognize the included path elements.</param>
        /// <returns>An enumeration that gets each item in the resulting path.</returns>
        /// <remarks>
        /// The path is lazily evaluated. If the structure of objects can change
        /// while enumerating path elements, then the enumeration should be materialized into a list or an array.
        /// </remarks>
        public static IEnumerable<T> GetPath<T>(this T target, Func<T, T> nextFunc, Predicate<T> pathInclude)
            where T : class
        {
            var current = target;
            while (pathInclude(current))
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
        /// Does multiple things with an object, and returns multiple results.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <typeparam name="TResult">Type of the resulting value.</typeparam>
        /// <param name="target">The target object to apply the delegate to.</param>
        /// <param name="withFuncs">The delegates to be called passing the target object.</param>
        /// <returns>Returns the results of the delegates.</returns>
        public static IEnumerable<TResult> With<T, TResult>(this T target, IEnumerable<Func<T, TResult>> withFuncs)
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

        /// <summary>
        /// Does multiple things with an object.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="target">The target object to apply the delegate to.</param>
        /// <param name="withFuncs">The delegates to be called passing the target object.</param>
        public static void With<T>(this T target, IEnumerable<Action<T>> withFuncs)
        {
            foreach (var withFunc in withFuncs)
                withFunc(target);
        }

        /// <summary>
        /// Creates an enumerable with a single object, if allowed by the predicate; otherwise an empty set.
        /// </summary>
        /// <typeparam name="T">Type of the enumerable result.</typeparam>
        /// <param name="target">Target object, that will compose this single or empty set.</param>
        /// <param name="predicate">Predicate filtering the set.</param>
        /// <returns>A single item enumeration, or and empty enumeration.</returns>
        public static IEnumerable<T> UnitSet<T>(this T target, [CanBeNull] Func<T, bool> predicate = null)
        {
            if (predicate == null || predicate(target))
                yield return target;
        }

        /// <summary>
        /// Evaluates an object with the given string representing a property to get the value from.
        /// </summary>
        /// <param name="source">Object to evaluate.</param>
        /// <param name="expression">String expression representing a property of the object.</param>
        /// <param name="propertyEvaluator">Delegate that gets the value of a property from an object.</param>
        /// <returns>Object evaluated from the source object, according to the given expression.</returns>
        internal static object Evaluate(this object source, string expression, Func<object, string, object> propertyEvaluator)
        {
            // TODO: this method does not fit here
            // Evaluating an expression is a DSL (domain specific language).
            var propNames = new Queue<string>(expression.Split('.'));
            object currentObj = source;
            while (propNames.Count > 0 && currentObj != null)
            {
                var propertyName = propNames.Dequeue().Trim();
                currentObj = DlrPropertyEvaluator(currentObj, propertyName);
            }

            var result2 = currentObj;
            return result2;
        }

        internal static object DlrPropertyEvaluator([NotNull]this object source, string propertyName)
        {
            var site = CallSite<Func<CallSite, object, object>>.Create(
                Binder.GetMember(
                    CSharpBinderFlags.None,
                    propertyName,
                    source.GetType(),
                    new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));

            var result = site.Target(site, source);
            return result;
        }

        internal static object ReflectionPropertyEvaluator([NotNull]this object source, string propertyName)
        {
            var propInfo = source.GetType().GetProperty(propertyName);

            if (propInfo == null)
                throw new InvalidOperationException("Could not evaluate the object with the given expression.");

            var result = propInfo.GetValue(source, null);
            return result;
        }
    }
}