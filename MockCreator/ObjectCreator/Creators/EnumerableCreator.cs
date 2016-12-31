using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using Extensions;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class EnumerableCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (type.IsIEnumerable())
            {
                return CreateEnumerable<T>(type, defaultData, objectCreationStrategy);
            }

            return default(T);
        }

        private static T CreateEnumerable<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var result = type.IsGenericType
                ? EnumerableCreatorGeneric.Create<T>()
                : EnumerableCreatorNonGeneric.Create<T>(defaultData, objectCreationStrategy);

            if (result == null)
            {
                Debug.WriteLine($"Expected IEnumerble type: '{type.Name}' is unknown to create.");
            }

            return result;
        }
    }

    internal static class EnumerableCreatorNonGeneric
    {
        internal static T Create<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var type = typeof(T);
            return type.IsInterface ?
                (T)NonGenericInterfaceTypeCreator.GetValueOrDefault(type)?.Invoke(defaultData, objectCreationStrategy) :
                (T)NonGenericTypeCreator.GetValueOrDefault(type)?.Invoke(defaultData, objectCreationStrategy);
        }

        private static readonly Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>> NonGenericTypeCreator = new Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>>
        {
            {typeof(ArrayList), (defaultData, objectCreationStrategy) => new ArrayList(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToList())},
            {typeof(BitArray), (defaultData, objectCreationStrategy) => new BitArray(0)},
            {typeof(Hashtable), (defaultData, objectCreationStrategy) => new Hashtable(EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary())},
            {typeof(Queue), (defaultData, objectCreationStrategy) => new Queue(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToList())},
            {typeof(SortedList), (defaultData, objectCreationStrategy) => new SortedList(EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary())},
            {typeof(Stack), (defaultData, objectCreationStrategy) => new Stack(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToList())},
            {typeof(HybridDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToHybridDictionary()},
            {typeof(ListDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToListDictionary()},
            {typeof(NameValueCollection), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary().ToNameValueCollection()},
            {typeof(OrderedDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToOrderedDictionary()},
            {typeof(StringCollection), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToStringCollection()},
            {typeof(StringDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToStringDictionary()},
            {typeof(UriSchemeKeyedCollection), (defaultData, objectCreationStrategy) => new UriSchemeKeyedCollection(EnumerationCreator.CreateEnumerationWithObjects<Uri>(defaultData, objectCreationStrategy).ToArray()) },
            {typeof(NameObjectCollectionBase), (defaultData, objectCreationStrategy) => Substitute.ForPartsOf<NameObjectCollectionBase>() },
            {typeof(CollectionBase), (defaultData, objectCreationStrategy) => Substitute.ForPartsOf<CollectionBase>()},
            {typeof(DictionaryBase), (defaultData, objectCreationStrategy) => Substitute.ForPartsOf<DictionaryBase>()},
            {typeof(ReadOnlyCollectionBase), (defaultData, objectCreationStrategy) => Substitute.ForPartsOf<ReadOnlyCollectionBase>()},
        };

        private static readonly Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>> NonGenericInterfaceTypeCreator = new Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>>
        {
            {typeof(IEnumerable), (defaultData, objectCreationStrategy) => new List<object>(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy))},
            {typeof(ICollection), (defaultData, objectCreationStrategy) => new List<object>(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy))},
            {typeof(IList), (defaultData, objectCreationStrategy) => new List<object>(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy))},
            {typeof(IDictionary), (defaultData, objectCreationStrategy) => new Dictionary<int,int>(EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary())},
            {typeof(IOrderedDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToOrderedDictionary()},
        };
    }

    internal static class EnumerableCreatorGeneric
    {
        private static readonly Func<Type, object[], object> ForPartsOfFunc = (genericType, arguments) => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] { genericType }, new object[] { arguments });

        internal static T Create<T>()
        {
            var expectedType = typeof(T);
            var genericType = expectedType.GetGenericTypeDefinition();

            return expectedType.IsInterface ?
                (T)GenericInterfaceCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType) :
                (T)GenericCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType);
        }

        private static readonly Dictionary<Type, Func<Type, object>> GenericCollectionTypes = new Dictionary<Type, Func<Type, object>>()
            {
                { typeof(List<>), Activator.CreateInstance },
                { typeof(Collection<>), Activator.CreateInstance },
                { typeof(ObservableCollection<>), Activator.CreateInstance },
                { typeof(Dictionary<,>), Activator.CreateInstance },
                { typeof(ConcurrentBag<>), Activator.CreateInstance },
                { typeof(ConcurrentDictionary<,>), Activator.CreateInstance },
                { typeof(BlockingCollection<>), Activator.CreateInstance },
                { typeof(ReadOnlyCollection<>), type =>
                    {
                        var genericArguments = type.GetGenericArguments();
                        var parameter = typeof(List<>).MakeGenericType(genericArguments).Create();
                        var returnValue = Activator.CreateInstance(typeof(ReadOnlyCollection<>).MakeGenericType(genericArguments), parameter);
                        return returnValue;
                    }},
                { typeof(Queue<>), Activator.CreateInstance },
                { typeof(KeyedByTypeCollection<>), Activator.CreateInstance },
                { typeof(HashSet<>), Activator.CreateInstance},
                { typeof(Stack<>), Activator.CreateInstance },
                { typeof(SortedList<,>), Activator.CreateInstance },
                { typeof(LinkedList<>), Activator.CreateInstance },
                { typeof(SynchronizedCollection<>), Activator.CreateInstance },
                { typeof(SynchronizedReadOnlyCollection<>), Activator.CreateInstance},
                { typeof(SortedSet<>), Activator.CreateInstance },
                { typeof(SortedDictionary<,>), Activator.CreateInstance },
                { typeof(ConcurrentQueue<>), Activator.CreateInstance },
                { typeof(ConcurrentStack<>), Activator.CreateInstance },
                { typeof(ReadOnlyObservableCollection<>), type =>
                    {
                        var genericArguments = type.GetGenericArguments();
                        var parameter = typeof(ObservableCollection<>).MakeGenericType(genericArguments).Create();
                        var returnValue = Activator.CreateInstance(typeof(ReadOnlyObservableCollection<>).MakeGenericType(genericArguments), parameter);
                        return returnValue;
                    }},
                { typeof(ReadOnlyDictionary<,>), type =>
                {
                    var genericArguments = type.GetGenericArguments();
                    var parameter = typeof(Dictionary<,>).MakeGenericType(genericArguments).Create();
                    var returnValue = Activator.CreateInstance(typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments), parameter);
                    return returnValue;
                }},
                { typeof(ImmutableList<>), type => typeof(ImmutableList).InvokeGenericMethod(nameof(ImmutableList.Create), type.GetGenericArguments())},
                { typeof(KeyedCollection<,>),  type => ForPartsOfFunc(type, new object[] {}) },
                { typeof(ImmutableArray<>), type => typeof(ImmutableArray).InvokeGenericMethod(nameof(ImmutableArray.Create), type.GetGenericArguments())},
                { typeof(ImmutableDictionary<,>), type => typeof(ImmutableDictionary).InvokeGenericMethod(nameof(ImmutableArray.Create), type.GetGenericArguments())},
                { typeof(ImmutableHashSet<>), type => typeof(ImmutableHashSet).InvokeGenericMethod(nameof(ImmutableHashSet.Create), type.GetGenericArguments())},
                { typeof(ImmutableSortedDictionary<,>), type => typeof(ImmutableSortedDictionary).InvokeGenericMethod(nameof(ImmutableSortedDictionary.Create), type.GetGenericArguments())},
                { typeof(ImmutableSortedSet<>), type => typeof(ImmutableSortedSet).InvokeGenericMethod(nameof(ImmutableSortedSet.Create), type.GetGenericArguments())},
                { typeof(ImmutableQueue<>), type => typeof(ImmutableQueue).InvokeGenericMethod(nameof(ImmutableQueue.Create), type.GetGenericArguments())},
                { typeof(ImmutableStack<>), type => typeof(ImmutableStack).InvokeGenericMethod(nameof(ImmutableStack.Create), type.GetGenericArguments())},
                { typeof(SortedDictionary<,>.KeyCollection), type =>
                    {
                        var sortedDictionary = typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                        var keyCollection = sortedDictionary.GetType().GetProperty(nameof(SortedDictionary<int, int>.Keys)).GetValue(sortedDictionary);
                        return keyCollection;
                    }},
                { typeof(SortedDictionary<,>.ValueCollection), type =>
                    {
                        var sortedDictionary = typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                        var keyCollection = sortedDictionary.GetType().GetProperty(nameof(SortedDictionary<int, int>.Values)).GetValue(sortedDictionary);
                        return keyCollection;
                    }},
                { typeof(Dictionary<,>.ValueCollection), type =>
                {
                    var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                    var keyCollection = dictionary.GetType().GetProperty(nameof(Dictionary<int, int>.Values)).GetValue(dictionary);
                    return keyCollection;
                }},
                { typeof(Dictionary<,>.KeyCollection), type =>
                {
                    var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                    var keyCollection = dictionary.GetType().GetProperty(nameof(Dictionary<int, int>.Keys)).GetValue(dictionary);
                    return keyCollection;
                }},
                { typeof(ReadOnlyDictionary<,>.KeyCollection), type =>
                {
                    var genericArguments = type.GetGenericArguments();
                    var readOnlyDictionary = typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments).Create();
                    var keyCollection = readOnlyDictionary.GetType().GetProperty(nameof(ReadOnlyDictionary<int, int>.Keys)).GetValue(readOnlyDictionary);
                    return keyCollection;
                }},
                { typeof(ReadOnlyDictionary<,>.ValueCollection), type =>
                {
                    var genericArguments = type.GetGenericArguments();
                    var readOnlyDictionary = typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments).Create();
                    var keyCollection = readOnlyDictionary.GetType().GetProperty(nameof(ReadOnlyDictionary<int, int>.Values)).GetValue(readOnlyDictionary);
                    return keyCollection;
                }},
            };

        private static readonly Dictionary<Type, Func<Type, object>> GenericInterfaceCollectionTypes = new Dictionary<Type, Func<Type, object>>()
            {
                { typeof(IEnumerable<>), type => Activator.CreateInstance(typeof(Collection<>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(ICollection<>), type => Activator.CreateInstance(typeof(Collection<>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IList<>), type => Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IDictionary<,>), type => Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IReadOnlyCollection<>), type => typeof(ReadOnlyCollection<>).MakeGenericType(type.GetGenericArguments()).Create() },
                { typeof(IReadOnlyDictionary<,>), type => typeof(ReadOnlyDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(IReadOnlyList<>), type => typeof(ReadOnlyCollection<>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(ISet<>), type => typeof(HashSet<>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(IProducerConsumerCollection<>), type => Activator.CreateInstance(typeof(ConcurrentBag<>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IImmutableDictionary<,>), type => typeof(ImmutableDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(IImmutableList<>), type => typeof(ImmutableList<>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(IImmutableQueue<>), type => typeof(ImmutableQueue<>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(IImmutableSet<>), type => typeof(ImmutableHashSet<>).MakeGenericType(type.GetGenericArguments()).Create()},
                { typeof(IImmutableStack<>), type => typeof(ImmutableStack<>).MakeGenericType(type.GetGenericArguments()).Create()},
            };
    }

    internal static class EnumerationCreator
    {
        internal static IEnumerable CreateEnumeration(Type enumerationType, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var enumerationItemType = enumerationType.GetGenericArguments()[0];
            var enumerationCount = objectCreationStrategy.EnumerationCount;

            for (var i = 0; i < enumerationCount; i++)
            {
                var result = enumerationItemType.Create(defaultData, objectCreationStrategy);
                yield return result;
            }
        }

        internal static IEnumerable<T> CreateEnumerationWithObjects<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var itemsCount = objectCreationStrategy.EnumerationCount;
            for (int i = 0; i < itemsCount; i++)
            {
                yield return ObjectCreatorExtensions.Create<T>(defaultData, objectCreationStrategy);
            }
        }

        internal static IEnumerable<KeyValuePair<TKey, TValue>> CreateDictionaryEntries<TKey, TValue>(
            IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var itemsCount = objectCreationStrategy.EnumerationCount;
            for (int i = 0; i < itemsCount; i++)
            {
                var key = ObjectCreatorExtensions.Create<TKey>(defaultData, objectCreationStrategy);
                var value = ObjectCreatorExtensions.Create<TValue>(defaultData, objectCreationStrategy);
                yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }
    }
}
