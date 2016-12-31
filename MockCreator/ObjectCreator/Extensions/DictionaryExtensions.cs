using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ObjectCreator.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue result;
            dictionary.TryGetValue(key, out result);
            return result;
        }
    }
}