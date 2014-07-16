using System;
using System.Reflection;

namespace SystemExtensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the underlying type of a MemberInfo.
        /// </summary>
        /// <param name="member">The <see cref="MemberInfo"/> to get the type of.</param>
        /// <returns>The type of the member.</returns>
        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException(
                        "Input MemberInfo must be of type EventInfo, FieldInfo, MethodInfo, or PropertyInfo.");
            }
        }
    }
}