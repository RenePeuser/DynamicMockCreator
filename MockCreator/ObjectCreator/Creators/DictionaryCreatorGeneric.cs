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
                (T)GenericInterfaceCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType) :
                (T)GenericCollectionTypes.GetValueOrDefault(genericType)?.Invoke(expectedType);
        }

        private static readonly Dictionary<Type, Func<Type, object>> GenericCollectionTypes = new Dictionary<Type, Func<Type, object>>()
        {
            { typeof(Dictionary<,>), Activator.CreateInstance },
            { typeof(ConcurrentDictionary<,>), Activator.CreateInstance },
            { typeof(SortedList<,>), Activator.CreateInstance },
            { typeof(SortedDictionary<,>), Activator.CreateInstance },
            { typeof(ReadOnlyDictionary<,>), type =>
            {
                var genericArguments = type.GetGenericArguments();
                var parameter = typeof(Dictionary<,>).MakeGenericType(genericArguments).Create();
                var returnValue = Activator.CreateInstance(typeof(ReadOnlyDictionary<,>).MakeGenericType(genericArguments), parameter);
                return returnValue;
            }},
            { typeof(KeyedCollection<,>),  type => ForPartsOfFunc(type, new object[] {}) },
            { typeof(ImmutableDictionary<,>), type => typeof(ImmutableDictionary).InvokeGenericMethod(nameof(ImmutableArray.Create), type.GetGenericArguments())},
            { typeof(ImmutableSortedDictionary<,>), type => typeof(ImmutableSortedDictionary).InvokeGenericMethod(nameof(ImmutableSortedDictionary.Create), type.GetGenericArguments())},
        };

        private static readonly Dictionary<Type, Func<Type, object>> GenericInterfaceCollectionTypes = new Dictionary<Type, Func<Type, object>>()
        {
            { typeof(IDictionary<,>), type => Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(type.GetGenericArguments()))},
            { typeof(IReadOnlyDictionary<,>), type => typeof(ReadOnlyDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create()},
            { typeof(IImmutableDictionary<,>), type => typeof(ImmutableDictionary<,>).MakeGenericType(type.GetGenericArguments()).Create()},
        };
    }
}