using System;
using System.Diagnostics;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class EnumerableCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (type.IsIDictionary())
            {
                return CreateDictionary<T>(type, defaultData, objectCreationStrategy);
            }

            if (type.IsIEnumerable())
            {
                return CreateEnumerable<T>(type, defaultData, objectCreationStrategy);
            }

            return default(T);
        }

        private static T CreateEnumerable<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var result = type.IsGenericType
                ? EnumerableCreatorGeneric.Create<T>(defaultData, objectCreationStrategy)
                : EnumerableCreatorNonGeneric.Create<T>(defaultData, objectCreationStrategy);

            if (result == null)
            {
                Debug.WriteLine($"Expected IEnumerble type: '{type.Name}' is unknown to create.");
            }

            return result;
        }

        private static T CreateDictionary<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var result = type.IsGenericType
                ? DictionaryCreatorGeneric.Create<T>(defaultData, objectCreationStrategy)
                : DictionaryCreatorNonGeneric.Create<T>(defaultData, objectCreationStrategy);

            if (result == null)
            {
                Debug.WriteLine($"Expected IEnumerble type: '{type.Name}' is unknown to create.");
            }

            return result;
        }
    }
}
