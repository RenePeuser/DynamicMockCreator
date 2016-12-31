using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace ObjectCreator.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var dictionary = keyValuePairs.ToDictionary(item => item.Key, item => item.Value);
            return dictionary;
        }

        public static OrderedDictionary ToOrderedDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var orderedDictionary = new OrderedDictionary();
            keyValuePairs.ForEach(item => orderedDictionary.Add(item.Key, item.Value));
            return orderedDictionary;
        }

        public static ListDictionary ToListDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var listDictionary = new ListDictionary();
            keyValuePairs.ForEach(item => listDictionary.Add(item.Key, item.Value));
            return listDictionary;
        }

        public static HybridDictionary ToHybridDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var nameValueCollection = new HybridDictionary();
            keyValuePairs.ForEach(item => nameValueCollection.Add(item.Key.ToString(), item.Value.ToString()));
            return nameValueCollection;
        }

        public static StringDictionary ToStringDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var stringDictionary = new StringDictionary();
            keyValuePairs.ForEach(item => stringDictionary.Add(item.Key.ToString(), item.Value.ToString()));
            return stringDictionary;
        }

        public static NameValueCollection ToNameValueCollection<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var nameValueCollection = new NameValueCollection();
            keyValuePairs.ForEach(item => nameValueCollection.Add(item.Key.ToString(), item.Value.ToString()));
            return nameValueCollection;
        }

        public static string ToString<T>(this KeyValuePair<string, Func<T, object>> compiledExpression, T argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("argument");
            }

            var result = string.Format(CultureInfo.InvariantCulture, "{0}[{1}] ", compiledExpression.Key, compiledExpression.Value.Invoke(argument));
            return result;
        }
    }
}