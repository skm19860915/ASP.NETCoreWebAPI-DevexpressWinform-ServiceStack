using System;
using System.Collections.Generic;

namespace Xperters.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static V GetOr<K, V>(this Dictionary<K, V> dict, K key, V defaultValue)
        {
            if (dict == null)
                throw new ArgumentNullException("Dicitonary provided was null.");
            if (!dict.ContainsKey(key))
                return defaultValue;
            return dict[key];
        }
    }
}
