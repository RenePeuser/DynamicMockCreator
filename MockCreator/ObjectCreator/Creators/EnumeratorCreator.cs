using Extensions;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCreator.Creators
{
    internal static class EnumeratorCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            // return default(T);
            var expectedType = typeof(T);
            return expectedType.IsGenericType ?
                EnumeratorCreatorGeneric.Create<T>(expectedType) :
                EnumeratorCreatorNonGeneric.Create<T>(expectedType);
        }

        private static class EnumeratorCreatorNonGeneric
        {
            internal static T Create<T>(Type expectedType)
            {
                var result = (T)NonGenericTypeCreator.GetValueOrDefault(expectedType)?.Invoke();
                if (result == null)
                {
                    Debug.WriteLine($"Expected Enumerator type: '{expectedType.Name}' is unknown to create.");
                }

                return result;
            }

            private static readonly Dictionary<Type, Func<object>> NonGenericTypeCreator = new Dictionary<Type, Func<object>>
            {
                {typeof(StringEnumerator), () => new StringCollection().GetEnumerator()},
                {typeof(IDictionaryEnumerator), () => ObjectCreatorExtensions.Create<IDictionary>().GetEnumerator()},
            };
        }

        private static class EnumeratorCreatorGeneric
        {
            internal static T Create<T>(Type expectedType)
            {
                var genericType = expectedType.GetGenericTypeDefinition();
                var result = (T)EnumeratorCreators.GetValueOrDefault(genericType)?.Invoke(expectedType);
                if (result == null)
                {
                    Debug.WriteLine($"Expected Enumerator type: '{expectedType.Name}' is unknown to create.");
                }

                return result;
            }

            private static readonly Dictionary<Type, Func<Type, object>> EnumeratorCreators = new Dictionary<Type, Func<Type, object>>()
            {
                { typeof(IEnumerator<>), type =>
                {
                    var queue = typeof(IEnumerable<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(IEnumerable<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },


                { typeof(List<>.Enumerator), type =>
                {
                    var queue = typeof(List<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(List<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },

                { typeof(SortedSet<>.Enumerator), type =>
                {
                    var queue = typeof(SortedSet<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(SortedSet<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },

                { typeof(LinkedList<>.Enumerator), type =>
                {
                    var queue = typeof(LinkedList<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(LinkedList<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },

                { typeof(Stack<>.Enumerator), type =>
                {
                    var queue = typeof(Stack<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(Stack<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },

                { typeof(HashSet<>.Enumerator), type =>
                {
                    var queue = typeof(HashSet<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(HashSet<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },

                { typeof(Queue<>.Enumerator), type =>
                {
                    var queue = typeof(Queue<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(Queue<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },

                { typeof(SortedDictionary<, >.KeyCollection.Enumerator), type =>
                {
                    var keyCollection = typeof(SortedDictionary<,>.KeyCollection).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = keyCollection.GetType().GetMethod(nameof(KeyedCollection<int,int>.GetEnumerator)).Invoke(keyCollection, new object[] {});
                    return getEnumerator;
                } },

                { typeof(SortedDictionary<, >.ValueCollection.Enumerator), type =>
                {
                    var valueCollection = typeof(SortedDictionary<,>.ValueCollection).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = valueCollection.GetType().GetMethod(nameof(KeyedCollection<int,int>.GetEnumerator)).Invoke(valueCollection, new object[] {});
                    return getEnumerator;
                } },

                { typeof(SortedDictionary<,>.Enumerator), type =>
                {
                    var dictionary = typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = dictionary.GetType().GetMethod(nameof(SortedDictionary<int,int>.GetEnumerator)).Invoke(dictionary, new object[] {});
                    return getEnumerator;
                }},

                { typeof(Dictionary<,>.Enumerator), type =>
                {
                    var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = dictionary.GetType().GetMethod(nameof(Dictionary<int,int>.GetEnumerator)).Invoke(dictionary, new object[] {});
                    return getEnumerator;
                }},
                { typeof(Dictionary<, >.KeyCollection.Enumerator), type =>
                {
                    var keyCollection = typeof(Dictionary<string, int>.KeyCollection).Create();
                    var getEnumerator = keyCollection.GetType().GetMethod(nameof(KeyedCollection<int,int>.GetEnumerator)).Invoke(keyCollection, new object[] {});
                    return getEnumerator;
                } },
                { typeof(Dictionary<,>.ValueCollection.Enumerator), type =>
                {
                    var keyCollection = typeof(Dictionary<string, int>.ValueCollection).Create();
                    var getEnumerator = keyCollection.GetType().GetMethod(nameof(Dictionary<int,int>.ValueCollection.GetEnumerator)).Invoke(keyCollection, new object[] {});
                    return getEnumerator;
                } },
                { typeof(ImmutableList<>.Enumerator), type =>
                {
                    var immutableList = typeof(ImmutableList<>).MakeGenericType(type.GetGenericArguments()).Create();
                    var getEnumerator = immutableList.GetType().GetMethod(nameof(ImmutableList<int>.GetEnumerator)).Invoke(immutableList, new object[] {});
                    return getEnumerator;
                } },
                { typeof(ImmutableSortedDictionary<,>.Enumerator), type =>
                {
                    var imutableSortedDictionary = typeof(ImmutableSortedDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create();
                    var getEnumerator = imutableSortedDictionary.GetType().GetMethod(nameof(ImmutableSortedDictionary<int,int>.GetEnumerator)).Invoke(imutableSortedDictionary, new object[] {});
                    return getEnumerator;
                } },
                { typeof(ImmutableSortedSet<>.Enumerator), type =>
                {
                    var immutableSortedSet = typeof(ImmutableSortedSet<>).MakeGenericType(type.GetGenericArguments()).Create();
                    var getEnumerator = immutableSortedSet.GetType().GetMethod(nameof(ImmutableSortedSet<int>.GetEnumerator)).Invoke(immutableSortedSet, new object[] {});
                    return getEnumerator;
                } },
            };
        }
    }
}
