using System.Collections.Generic;

namespace SystemExtensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets a value from the dictionary given a key,
        /// or a default value when the key is not found.
        /// </summary>
        /// <typeparam name="TKey">Type of the keys of the dictionary.</typeparam>
        /// <typeparam name="TValue">Type of the values in the dictionary.</typeparam>
        /// <param name="dic">Dictionary to get the value or the default from.</param>
        /// <param name="key">Key identifying the value to get.</param>
        /// <param name="defaultValue">Default value to return when the key is not found in the dictionary.</param>
        /// <returns>A value representing the value associated with a found key, or the default value when the key is not found.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            var result = dic.TryGetValue(key, out value) ? value : defaultValue;
            return result;
        }
    }
}
