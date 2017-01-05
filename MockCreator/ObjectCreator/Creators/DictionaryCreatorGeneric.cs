using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class DictionaryCreatorGeneric
    {
        private static readonly Func<Type, object[], object> ForPartsOfFunc = (genericType, arguments) => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] { genericType }, new object[] { arguments });

        internal static T Create<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var expectedType = typeof(T);
            var genericType = expectedType.GetGenericTypeDefinition();

            return expectedType.IsInterface ?
                (T)GenericInterfaceCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType, defaultData, objectCreationStrategy) :
                (T)GenericCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType, defaultData, objectCreationStrategy);
        }

        private static readonly Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>> GenericCollectionTypes = new Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>>
        {
            { typeof(Dictionary<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var dictionary = typeof(EnumerationCreator).InvokeExpectedMethod(nameof(EnumerationCreator.CreateDictionary), type.GetGenericArguments(), defaultData, objectCreationStrategy);
                return dictionary;
            }},

            { typeof(ConcurrentDictionary<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var keyValuePair = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
                var items = typeof(IEnumerable<>).MakeGenericType(keyValuePair).Create(defaultData, objectCreationStrategy);
                var result = Activator.CreateInstance(typeof(ConcurrentDictionary<,>).MakeGenericType(type.GetGenericArguments()), items);
                return result;
            }},

            { typeof(SortedList<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var genericArguments = type.GetGenericArguments();
                var parameter = typeof(Dictionary<,>).MakeGenericType(genericArguments).Create(defaultData, objectCreationStrategy);
                var returnValue = Activator.CreateInstance(typeof(SortedList<,>).MakeGenericType(genericArguments), parameter);
                return returnValue;
            }},

            { typeof(SortedDictionary<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var genericArguments = type.GetGenericArguments();
                var parameter = typeof(Dictionary<,>).MakeGenericType(genericArguments).Create(defaultData, objectCreationStrategy);
                var returnValue = Activator.CreateInstance(typeof(SortedDictionary<,>).MakeGenericType(genericArguments), parameter);
                return returnValue;
            }},

            { typeof(ReadOnlyDictionary<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var genericArguments = type.GetGenericArguments();
                var parameter = typeof(Dictionary<,>).MakeGenericType(genericArguments).Create(defaultData, objectCreationStrategy);
                var returnValue = Activator.CreateInstance(typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments), parameter);
                return returnValue;
            }},

            { typeof(KeyedCollection<,>),  (type, defaultData, objectCreationStrategy) => ForPartsOfFunc(type, new object[] {}) },

            { typeof(ImmutableDictionary<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var keyValuePair = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
                var items = typeof(IEnumerable<>).MakeGenericType(keyValuePair).Create(defaultData, objectCreationStrategy);
                var result = typeof(ImmutableDictionary).InvokeGenericMethod(nameof(ImmutableDictionary.CreateRange), type.GetGenericArguments(), items);
                return result;
            }},

            { typeof(ImmutableSortedDictionary<,>), (type, defaultData, objectCreationStrategy) =>
            {
                var keyValuePair = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
                var items = typeof(IEnumerable<>).MakeGenericType(keyValuePair).Create(defaultData, objectCreationStrategy);
                var result = typeof(ImmutableSortedDictionary).InvokeGenericMethod(nameof(ImmutableSortedDictionary.CreateRange), type.GetGenericArguments(), items);
                return result;
            }},
        };

        private static readonly Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>> GenericInterfaceCollectionTypes = new Dictionary<Type, Func<Type, IDefaultData, ObjectCreationStrategy, object>>
        {
            { typeof(IDictionary<,>), (type, defaultData, objectCreationStrategy) => typeof(Dictionary<,>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy)},
            { typeof(IReadOnlyDictionary<,>), (type, defaultData, objectCreationStrategy) => typeof(ReadOnlyDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy)},
            { typeof(IImmutableDictionary<,>), (type, defaultData, objectCreationStrategy) => typeof(ImmutableDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create(defaultData, objectCreationStrategy)},
        };
    }
}