using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class EnumerableCreatorGeneric
    {
        internal static T Create<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrateg)
        {
            var expectedType = typeof(T);
            var genericType = expectedType.GetGenericTypeDefinition();

            return expectedType.IsInterface ?
                (T)GenericInterfaceCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType, defaultData, objectCreationStrateg) :
                (T)GenericCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType, defaultData, objectCreationStrateg);
        }

        private static readonly Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>> GenericCollectionTypes = new Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>>
        {
            { typeof(List<>), (type, defaultData, objectCreationStrategy) =>
            {
                var creation = Activator.CreateInstance(type);
                var items = EnumerationCreator.CreateEnumeration(type, defaultData, objectCreationStrategy);
                foreach (var item in items)
                {
                    ((IList)creation).Add(item);
                }
                return creation;
            } },

            { typeof(Collection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var creation = Activator.CreateInstance(type);
                var items = EnumerationCreator.CreateEnumeration(type, defaultData, objectCreationStrategy);
                foreach (var item in items)
                {
                    ((IList)creation).Add(item);
                }
                return creation;
            } },

            { typeof(ObservableCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var creation = Activator.CreateInstance(type);
                var items = EnumerationCreator.CreateEnumeration(type, defaultData, objectCreationStrategy);
                foreach (var item in items)
                {
                    ((IList)creation).Add(item);
                }
                return creation;
            } },

            { typeof(ConcurrentBag<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(BlockingCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(ConcurrentBag<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(ReadOnlyCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(Queue<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(KeyedByTypeCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var types = new object[] {1, 1.1, "a", 'c', (byte) 2, (float) 3, (ushort) 4, true};
                var maxItems = Math.Min(objectCreationStrategy.EnumerationCount, types.Length);
                var expectedTypes = types.Take(maxItems).ToArray();
                var creation = Activator.CreateInstance(type, expectedTypes);
                return creation;
            } },

            { typeof(HashSet<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(Stack<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(LinkedList<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(SynchronizedCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var creation = Activator.CreateInstance(type);
                var items = EnumerationCreator.CreateEnumeration(type, defaultData, objectCreationStrategy);
                foreach (var item in items)
                {
                    ((IList)creation).Add(item);
                }
                return creation;
            } },

            { typeof(SynchronizedReadOnlyCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var items = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, new object(), items);
                return creation;
            } },

            { typeof(SortedSet<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(ConcurrentQueue<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(ConcurrentStack<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(ReadOnlyObservableCollection<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(ObservableCollection<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var creation = Activator.CreateInstance(type, list);
                return creation;
            } },

            { typeof(ImmutableList<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var immutable = typeof(ImmutableList).InvokeGenericMethod(nameof(ImmutableList.CreateRange), type.GetGenericArguments(), list);
                return immutable;
            } },

            { typeof(ImmutableArray<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var immutable = typeof(ImmutableArray).InvokeGenericMethod(nameof(ImmutableArray.CreateRange), type.GetGenericArguments(), list);
                return immutable;
            } },

            { typeof(ImmutableHashSet<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var immutable = typeof(ImmutableHashSet).InvokeGenericMethod(nameof(ImmutableHashSet.CreateRange), type.GetGenericArguments(), list);
                return immutable;
            } },

            { typeof(ImmutableSortedSet<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var immutable = typeof(ImmutableSortedSet).InvokeGenericMethod(nameof(ImmutableSortedSet.CreateRange), type.GetGenericArguments(), list);
                return immutable;
            } },

            { typeof(ImmutableQueue<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var immutable = typeof(ImmutableQueue).InvokeGenericMethod(nameof(ImmutableQueue.CreateRange), type.GetGenericArguments(), list);
                return immutable;
            } },

             { typeof(ImmutableStack<>), (type, defaultData, objectCreationStrategy) =>
            {
                var list = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy);
                var immutable = typeof(ImmutableStack).InvokeGenericMethod(nameof(ImmutableStack.CreateRange), type.GetGenericArguments(), list);
                return immutable;
            } },

            { typeof(SortedDictionary<,>.KeyCollection), (type, defaultData, objectCreationStrategy) =>
            {
                var sortedDictionary = typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create(defaultData,objectCreationStrategy);
                var keyCollection = sortedDictionary.GetType().GetProperty(nameof(SortedDictionary<int, int>.Keys)).GetValue(sortedDictionary);
                return keyCollection;
            }},
            { typeof(SortedDictionary<,>.ValueCollection), (type, defaultData, objectCreationStrategy) =>
            {
                var sortedDictionary = typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create(defaultData,objectCreationStrategy);
                var keyCollection = sortedDictionary.GetType().GetProperty(nameof(SortedDictionary<int, int>.Values)).GetValue(sortedDictionary);
                return keyCollection;
            }},
            { typeof(Dictionary<,>.ValueCollection), (type, defaultData, objectCreationStrategy) =>
            {
                var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create(defaultData,objectCreationStrategy);
                var keyCollection = dictionary.GetType().GetProperty(nameof(Dictionary<int, int>.Values)).GetValue(dictionary);
                return keyCollection;
            }},
            { typeof(Dictionary<,>.KeyCollection), (type, defaultData, objectCreationStrategy) =>
            {
                var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create(defaultData,objectCreationStrategy);
                var keyCollection = dictionary.GetType().GetProperty(nameof(Dictionary<int, int>.Keys)).GetValue(dictionary);
                return keyCollection;
            }},
            { typeof(ReadOnlyDictionary<,>.KeyCollection), (type, defaultData, objectCreationStrategy) =>
            {
                var genericArguments = type.GetGenericArguments();
                var readOnlyDictionary = typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments).Create(defaultData,objectCreationStrategy);
                var keyCollection = readOnlyDictionary.GetType().GetProperty(nameof(ReadOnlyDictionary<int, int>.Keys)).GetValue(readOnlyDictionary);
                return keyCollection;
            }},
            { typeof(ReadOnlyDictionary<,>.ValueCollection), (type, defaultData, objectCreationStrategy) =>
            {
                var genericArguments = type.GetGenericArguments();
                var readOnlyDictionary = typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments).Create(defaultData,objectCreationStrategy);
                var keyCollection = readOnlyDictionary.GetType().GetProperty(nameof(ReadOnlyDictionary<int, int>.Values)).GetValue(readOnlyDictionary);
                return keyCollection;
            }},
        };

        private static readonly Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>> GenericInterfaceCollectionTypes = new Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>>
        {
            { typeof(IEnumerable<>), (type, defaultData, objectCreationStrategy) => typeof(Collection<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(ICollection<>), (type, defaultData, objectCreationStrategy) => typeof(Collection<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IList<>), (type, defaultData, objectCreationStrategy) => typeof(List<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IReadOnlyCollection<>), (type, defaultData, objectCreationStrategy) => typeof(ReadOnlyCollection<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IReadOnlyList<>), (type, defaultData, objectCreationStrategy) => typeof(ReadOnlyCollection<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(ISet<>), (type, defaultData, objectCreationStrategy) => typeof(HashSet<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IProducerConsumerCollection<>), (type, defaultData, objectCreationStrategy) => typeof(ConcurrentBag<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IImmutableList<>), (type, defaultData, objectCreationStrategy) => typeof(ImmutableList<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IImmutableQueue<>), (type, defaultData, objectCreationStrategy) => typeof(ImmutableQueue<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IImmutableSet<>), (type, defaultData, objectCreationStrategy) => typeof(ImmutableHashSet<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
            { typeof(IImmutableStack<>), (type, defaultData, objectCreationStrategy) => typeof(ImmutableStack<>).MakeGenericType(type.GetGenericArguments()).Create(defaultData,objectCreationStrategy)},
        };
    }
}