using System;
using System.Collections;
using System.Collections.Generic;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
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