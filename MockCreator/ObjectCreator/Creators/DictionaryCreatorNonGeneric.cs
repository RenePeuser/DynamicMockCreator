using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class DictionaryCreatorNonGeneric
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
            {typeof(Hashtable), (defaultData, objectCreationStrategy) => new Hashtable(EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary())},
            {typeof(SortedList), (defaultData, objectCreationStrategy) => new SortedList(EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary())},
            {typeof(HybridDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToHybridDictionary()},
            {typeof(ListDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToListDictionary()},
            {typeof(NameValueCollection), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary().ToNameValueCollection()},
            {typeof(OrderedDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToOrderedDictionary()},
            {typeof(StringDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToStringDictionary()},
        };

        private static readonly Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>> NonGenericInterfaceTypeCreator = new Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>>
        {
            {typeof(IDictionary), (defaultData, objectCreationStrategy) => new Dictionary<int,int>(EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary())},
            {typeof(IOrderedDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToOrderedDictionary()},
        };
    }
}