using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class EnumerableCreatorNonGeneric
    {
        internal static T Create<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var type = typeof(T);
            if (type.IsInterface)
            {
                return (T)NonGenericInterfaceTypeCreator.GetValueOrDefault(type)?.Invoke(defaultData, objectCreationStrategy);
            }

            return (T)NonGenericTypeCreator.GetValueOrDefault(type)?.Invoke(defaultData, objectCreationStrategy);
        }

        private static readonly Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>> NonGenericTypeCreator = new Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>>
        {
            {typeof(ArrayList), (defaultData, objectCreationStrategy) => new ArrayList(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToList())},
            {typeof(BitArray), (defaultData, objectCreationStrategy) => new BitArray(0)},
            {typeof(Queue), (defaultData, objectCreationStrategy) => new Queue(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToList())},
            {typeof(Stack), (defaultData, objectCreationStrategy) => new Stack(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToList())},
            {typeof(StringCollection), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy).ToStringCollection()},
            {typeof(StringDictionary), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToStringDictionary()},
            {typeof(UriSchemeKeyedCollection), (defaultData, objectCreationStrategy) => new UriSchemeKeyedCollection(EnumerationCreator.CreateEnumerationWithObjects<Uri>(defaultData, objectCreationStrategy).ToArray()) },
            {typeof(NameValueCollection), (defaultData, objectCreationStrategy) => EnumerationCreator.CreateDictionaryEntries<int,int>(defaultData, objectCreationStrategy).ToDictionary().ToNameValueCollection()},
        };

        private static readonly Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>> NonGenericInterfaceTypeCreator = new Dictionary<Type, Func<IDefaultData, ObjectCreationStrategy, object>>
        {
            {typeof(IEnumerable), (defaultData, objectCreationStrategy) => new List<object>(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy))},
            {typeof(ICollection), (defaultData, objectCreationStrategy) => new List<object>(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy))},
            {typeof(IList), (defaultData, objectCreationStrategy) => new List<object>(EnumerationCreator.CreateEnumerationWithObjects<object>(defaultData, objectCreationStrategy))},
        };
    }
}