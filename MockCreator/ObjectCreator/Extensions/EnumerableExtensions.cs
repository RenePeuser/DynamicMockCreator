using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ObjectCreator.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            source.ToList().ForEach(action);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumeration)
        {
            if (enumeration == null)
            {
                throw new ArgumentNullException(nameof(enumeration));
            }

            return new ObservableCollection<T>(enumeration);
        }

        public static StringCollection ToStringCollection<T>(this IEnumerable<T> source)
        {
            var stringCollection = new StringCollection();
            source.ForEach(item => stringCollection.Add(item.ToString()));
            return stringCollection;
        }
    }
}