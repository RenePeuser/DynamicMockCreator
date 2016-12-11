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
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    internal static class EnumerableCreator
    {
        internal static object Create(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!type.IsInterfaceImplemented<IEnumerable>())
            {
                return null;
            }

            var result = type.IsGenericType
            ? EnumerableCreatorGeneric.Create(type)
            : EnumerableCreatorNonGeneric.Create(type);

            if (result == null)
            {
                Debug.WriteLine($"Expected IEnumerble type: '{type.Name}' is unknown to create.");
            }

            return result;
        }

        internal static T Create<T>()
        {
            var type = typeof(T);
            if (!type.IsInterfaceImplemented<IEnumerable>())
            {
                return default(T);
            }

            return type.IsGenericType
            ? (T)EnumerableCreatorGeneric.Create<T>()
            : (T)EnumerableCreatorNonGeneric.Create<T>();
        }
    }

    internal static class EnumerableCreatorNonGeneric
    {
        internal static object Create(Type type)
        {
            return type.IsInterface ?
                NonGenericInterfaceTypeCreator.GetValueOrDefault(type)?.Invoke() :
                NonGenericTypeCreator.GetValueOrDefault(type)?.Invoke();
        }

        internal static object Create<T>()
        {
            var type = typeof(T);
            return type.IsInterface ?
                NonGenericInterfaceTypeCreator.GetValueOrDefault(type)?.Invoke() :
                NonGenericTypeCreator.GetValueOrDefault(type)?.Invoke();
        }

        private static readonly Dictionary<Type, Func<object>> NonGenericTypeCreator = new Dictionary<Type, Func<object>>
        {
            {typeof(ArrayList), () => new ArrayList()},
            {typeof(BitArray), () => new BitArray(0)},
            {typeof(CollectionBase), () => Substitute.ForPartsOf<CollectionBase>()},
            {typeof(DictionaryBase), () => Substitute.ForPartsOf<DictionaryBase>()},
            {typeof(ReadOnlyCollectionBase), () => Substitute.ForPartsOf<ReadOnlyCollectionBase>()},
            {typeof(Hashtable), () => new Hashtable()},
            {typeof(Queue), () => new Queue()},
            {typeof(SortedList), () => new SortedList()},
            {typeof(Stack), () => new Stack()},
            {typeof(DictionaryEntry), () => new DictionaryEntry()},
            {typeof(HybridDictionary), () => new HybridDictionary()},
            {typeof(ListDictionary), () => new ListDictionary()},
            {typeof(NameValueCollection), () => new NameValueCollection()},
            {typeof(OrderedDictionary), () => new OrderedDictionary()},
            {typeof(StringCollection), () => new StringCollection()},
            {typeof(StringDictionary), () => new StringDictionary()},
            {typeof(StringEnumerator), () => new StringCollection().GetEnumerator()},
            {typeof(BitVector32), () => new BitVector32(0)},
            {typeof(BitVector32.Section), () => BitVector32.CreateSection(0)},
            {typeof(UriSchemeKeyedCollection), () => new UriSchemeKeyedCollection() },
            {typeof(NameObjectCollectionBase), () => Substitute.ForPartsOf<NameObjectCollectionBase>() },
        };

        private static readonly Dictionary<Type, Func<object>> NonGenericInterfaceTypeCreator = new Dictionary<Type, Func<object>>()
        {
            {typeof(IEnumerable), () => new Collection<object>()},
            {typeof(ICollection), () => new Collection<object>()},
            {typeof(IList), () => new List<object>()},
            {typeof(IDictionary), () => new Dictionary<object, object>()},
            {typeof(IOrderedDictionary), () => new OrderedDictionary()},
        };
    }

    internal static class EnumerableCreatorGeneric
    {
        private static readonly Func<Type, object[], object> ForPartsOfFunc = (genericType, arguments) => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] { genericType }, new object[] { arguments });

        internal static object Create(Type type)
        {
            var genericType = type.GetGenericTypeDefinition();

            return type.IsInterface ?
                GenericInterfaceCollectionTypes.GetValueOrDefault(genericType)?.Invoke(type) :
                GenericCollectionTypes.GetValueOrDefault(genericType)?.Invoke(type);
        }

        internal static object Create<T>()
        {
            var expectedType = typeof(T);
            var genericType = expectedType.GetGenericTypeDefinition();

            return expectedType.IsInterface ?
                GenericInterfaceCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType) :
                GenericCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType);
        }

        private static readonly Dictionary<Type, Func<Type, object>> GenericCollectionTypes = new Dictionary<Type, Func<Type, object>>()
            {
                { typeof(HashSet<>), Activator.CreateInstance},
                { typeof(HashSet<>.Enumerator), Activator.CreateInstance },
                { typeof(Stack<>), Activator.CreateInstance },
                { typeof(Stack<>.Enumerator), Activator.CreateInstance },
                { typeof(Collection<>), Activator.CreateInstance },
                { typeof(KeyedByTypeCollection<>), Activator.CreateInstance },
                { typeof(List<>), Activator.CreateInstance },
                { typeof(List<>.Enumerator), Activator.CreateInstance },
                { typeof(ObservableCollection<>), Activator.CreateInstance },
                { typeof(SortedList<,>), Activator.CreateInstance },
                { typeof(LinkedList<>.Enumerator), Activator.CreateInstance },
                { typeof(LinkedList<>), Activator.CreateInstance },
                { typeof(SynchronizedCollection<>), Activator.CreateInstance },
                { typeof(SynchronizedReadOnlyCollection<>), Activator.CreateInstance},
                { typeof(SortedSet<>), Activator.CreateInstance },
                { typeof(SortedSet<>.Enumerator), Activator.CreateInstance },
                { typeof(Queue<>), Activator.CreateInstance },
                { typeof(Queue<>.Enumerator),Activator.CreateInstance },
                { typeof(KeyedCollection<,>),  type => ForPartsOfFunc(type, new object[] {}) },
                { typeof(Dictionary<,>), Activator.CreateInstance },
                { typeof(SortedDictionary<,>), Activator.CreateInstance },
                { typeof(SortedDictionary<,>.Enumerator), Activator.CreateInstance },
                { typeof(SortedDictionary<,>.KeyCollection.Enumerator), Activator.CreateInstance },
                { typeof(SortedDictionary<,>.ValueCollection.Enumerator), Activator.CreateInstance },
                { typeof(BlockingCollection<>), Activator.CreateInstance },
                { typeof(ConcurrentBag<>), Activator.CreateInstance },
                { typeof(ConcurrentDictionary<,>), Activator.CreateInstance },
                { typeof(ConcurrentQueue<>), Activator.CreateInstance },
                { typeof(ConcurrentStack<>), Activator.CreateInstance },
                { typeof(Partitioner<>), Activator.CreateInstance },

                { typeof(LinkedListNode<>), type => Activator.CreateInstance(type, type.GetGenericArguments().First().Create()) },
                { typeof(Dictionary<,>.ValueCollection), type =>
                {
                    var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                    var keyCollection = dictionary.GetType().GetProperty(nameof(Dictionary<int, int>.Values)).GetValue(dictionary);
                    return keyCollection;
                }},
                { typeof(Dictionary<,>.Enumerator), type =>
                {
                    var dictionary = Activator.CreateInstance(type);
                    var getEnumerator = dictionary.GetType().GetMethod("GetEnumerator").Invoke(dictionary, new object[] {});
                    return getEnumerator;
                }},
                { typeof(Dictionary<, >.KeyCollection.Enumerator), type =>
                {
                    var result = Activator.CreateInstance(type, typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create());
                    var keyCollection = result.GetType().GetProperty(nameof(Dictionary<int, int>.Keys)).GetValue(result);
                    var getEnumerator = keyCollection.GetType().GetMethod("GetEnumerator").Invoke(keyCollection, new object[] {});
                    return getEnumerator;
                } },
                { typeof(Dictionary<,>.KeyCollection), type =>
                {
                    var dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create();
                    var keyCollection = dictionary.GetType().GetProperty(nameof(Dictionary<int, int>.Keys)).GetValue(dictionary);
                    return keyCollection;
                }},
                { typeof(Dictionary<,>.ValueCollection.Enumerator), type =>
                {
                    var result = Activator.CreateInstance(type, typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments).Create());
                    var keyCollection = result.GetType().GetProperty(nameof(Dictionary<int, int>.Values)).GetValue(result);
                    var getEnumerator = keyCollection.GetType().GetMethod("GetEnumerator").Invoke(keyCollection, new object[] {});
                    return getEnumerator;
                } },
                { typeof(SortedDictionary<,>.KeyCollection), type => Activator.CreateInstance(type, typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create())},
                { typeof(SortedDictionary<,>.ValueCollection), type => Activator.CreateInstance(type, typeof(SortedDictionary<,>).MakeGenericType(type.GenericTypeArguments).Create())},
                { typeof(ReadOnlyCollection<>), type =>
                {
                    var genericArguments = type.GetGenericArguments();
                    var parameter = typeof(List<>).MakeGenericType(genericArguments).Create();
                    var returnValue = Activator.CreateInstance(typeof(ReadOnlyCollection<>).MakeGenericType(genericArguments), parameter);
                    return returnValue;
                }},
                { typeof(ReadOnlyObservableCollection<>), type =>
                {
                    var genericArguments = type.GetGenericArguments();
                    var parameter = typeof(ObservableCollection<>).MakeGenericType(genericArguments).Create();
                    var returnValue = Activator.CreateInstance(typeof(ReadOnlyObservableCollection<>).MakeGenericType(genericArguments), parameter);
                    return returnValue;
                } },
                { typeof(ReadOnlyDictionary<,>), type =>
                {
                    var genericArguments = type.GetGenericArguments();
                    var parameter = typeof(Dictionary<,>).MakeGenericType(genericArguments).Create();
                    var returnValue = Activator.CreateInstance(typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments), parameter);
                    return returnValue;
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
                { typeof(KeyValuePair<,>), type => Activator.CreateInstance(type, type.GetGenericArguments().Select(arg => arg.Create()).ToArray()) },

                { typeof(ImmutableArray<>), type => typeof(ImmutableArray).InvokeGenericMethod(nameof(ImmutableArray.Create), type.GetGenericArguments())},
                { typeof(ImmutableDictionary<,>), type => typeof(ImmutableDictionary).InvokeGenericMethod(nameof(ImmutableArray.Create), type.GetGenericArguments())},
                { typeof(ImmutableHashSet<>), type => typeof(ImmutableHashSet).InvokeGenericMethod(nameof(ImmutableHashSet.Create), type.GetGenericArguments())},
                { typeof(ImmutableList<>), type => typeof(ImmutableList).InvokeGenericMethod(nameof(ImmutableList.Create), type.GetGenericArguments())},
                { typeof(ImmutableSortedDictionary<,>), type => typeof(ImmutableSortedDictionary).InvokeGenericMethod(nameof(ImmutableSortedDictionary.Create), type.GetGenericArguments())},
                { typeof(ImmutableSortedSet<>), type => typeof(ImmutableSortedSet).InvokeGenericMethod(nameof(ImmutableSortedSet.Create), type.GetGenericArguments())},
                { typeof(ImmutableQueue<>), type => typeof(ImmutableQueue).InvokeGenericMethod(nameof(ImmutableQueue.Create), type.GetGenericArguments())},
                { typeof(ImmutableStack<>), type => typeof(ImmutableStack).InvokeGenericMethod(nameof(ImmutableStack.Create), type.GetGenericArguments())},
            };

        private static readonly Dictionary<Type, Func<Type, object>> GenericInterfaceCollectionTypes = new Dictionary<Type, Func<Type, object>>()
            {
                { typeof(ICollection<>), type => Activator.CreateInstance(typeof(Collection<>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IEnumerable<>), type => Activator.CreateInstance(typeof(Collection<>).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IEnumerator<>), type => Activator.CreateInstance(typeof(List<>.Enumerator).MakeGenericType(type.GetGenericArguments()))},
                { typeof(IEqualityComparer<>), type => Activator.CreateInstance(typeof(EqualityComparer<>).MakeGenericType(type.GetGenericArguments())).GetType().GetMethod(nameof(EqualityComparer<int>.Default))},
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
}
