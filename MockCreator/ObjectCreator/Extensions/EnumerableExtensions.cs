using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ObjectCreator.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> FilterNullObjects<T>(this IEnumerable<T> source) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var result = source.Where(item => item != null);
            return result;
        }

        public static TSource FirstOfType<TSource>(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var result = source.FirstOrDefaultOfType<TSource>();
            if (result == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Enumeration does not contains any item of the specific type: {0}", typeof(TSource).Name));
            }

            return result;
        }

        public static TSource FirstOrDefaultOfType<TSource>(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var result = source.OfType<TSource>().FirstOrDefault();
            return result;
        }

        public static TSource FirstOfType<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = source.FirstOrDefaultOfType(predicate);
            if (result == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Enumeration does not contains any item of the specific type: {0}", typeof(TSource).Name));
            }

            return result;
        }

        public static TSource FirstOrDefaultOfType<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = source.OfType<TSource>().FirstOrDefault(predicate);
            return result;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            source.ToList().ForEach(action);
        }

        public static void ForEachOfType<TSource>(this IEnumerable source, Action<TSource> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            source.OfType<TSource>().ToList().ForEach(action);
        }

        public static bool NotSequenceEqualsNullable<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            // no argument checking is needed because SequenceEqualsNullable can handle null values.
            return !first.SequenceEqualsNullable(second);
        }

        public static bool SequenceEqualsNullable<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if (first == null || second == null)
            {
                return false;
            }

            return first.SequenceEqual(second);
        }

        public static bool SequenceEqualsOfType<TSource>(this IEnumerable first, IEnumerable second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }

            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            return first.OfType<TSource>().SequenceEqual(second.OfType<TSource>());
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var result = source.ToList().AsReadOnly();
            return result;
        }

        public static List<TSource> ToListOfType<TSource>(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var result = source.OfType<TSource>().ToList();
            return result;
        }

        public static List<TSource> ToListOfTypeOrEmpty<TSource>(this IEnumerable source)
        {
            if (source == null)
            {
                return new List<TSource>();
            }

            var result = source.OfType<TSource>().ToList();
            return result;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumeration)
        {
            if (enumeration == null)
            {
                throw new ArgumentNullException("enumeration");
            }

            return new ObservableCollection<T>(enumeration);
        }

        public static StringCollection ToStringCollection<T>(this IEnumerable<T> source)
        {
            var stringCollection = new StringCollection();
            source.ForEach(item => stringCollection.Add(item.ToString()));
            return stringCollection;
        }

        public static IEnumerable<TSource> WhereOfType<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = source.OfType<TSource>().Where(predicate);
            return result;
        }

        public static bool IsAnyItemNull<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var result = source.Any(item => item == null);
            return result;
        }

        public static bool IsNotAnyItemNull<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return !source.IsAnyItemNull();
        }

        public static void Dispose<T>(this IEnumerable<T> source) where T : IDisposable
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            source.ToList().ForEach(item => item.Dispose());
        }

        public static bool ContainsAll<T>(this IEnumerable<T> source, params T[] expectedItems)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedItems == null)
            {
                throw new ArgumentNullException("expectedItems");
            }

            var sourceContainsAllExpectedItems = expectedItems.All(source.Contains);
            return sourceContainsAllExpectedItems;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, params T[] expectedItems)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedItems == null)
            {
                throw new ArgumentNullException("expectedItems");
            }

            var sourceContainsAllExpectedItems = expectedItems.Any(source.Contains);
            return sourceContainsAllExpectedItems;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return !source.Any();
        }

        public static bool ContainsNoItemWith<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return !source.Any(predicate);
        }

        public static bool HasAny<T>(this IEnumerable<T> source, params object[] expectedValues)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return HasAny(source.ToArray(), expectedValues);
        }

        public static IEnumerable<T> Distinct<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var dictionary = new Dictionary<TProperty, T>();
            foreach (T item in source)
            {
                var value = selector(item);
                if (!dictionary.ContainsKey(value))
                {
                    dictionary.Add(value, item);
                }
            }

            return dictionary.Values;
        }
    }
}