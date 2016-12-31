using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Extensions;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class UnknownTypeCreator
    {
        private static readonly BindingFlags ExpectedBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                              BindingFlags.Static;

        internal static T CreateDynamicFrom<T>(Type type, IDefaultData defaultData,
            ObjectCreationStrategy objectCreationStrategy)
        {
            if (type.IsIEnumerable())
            {
                var enumerable = EnumerableCreator.Create<T>(type, defaultData, objectCreationStrategy);
                if (enumerable != null)
                {
                    return enumerable;
                }
            }

            if (type.IsIEnumerator())
            {
                var enumerator = EnumeratorCreator.Create<T>(type, defaultData, objectCreationStrategy);
                if (enumerator != null)
                {
                    return enumerator;
                }
            }

            var expectedObject = CreateDynamic<T>(type, defaultData, objectCreationStrategy);
            if (expectedObject == null)
            {
                return default(T);
            }

            return InitObject(defaultData, objectCreationStrategy, expectedObject);
        }

        private static T InitObject<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy, T expectedObject)
        {
            if (objectCreationStrategy.SetupProperties)
            {
                expectedObject.InitProperties(defaultData, objectCreationStrategy);
            }

            return expectedObject;
        }

        private static T CreateDynamic<T>(Type type, IDefaultData defaultData,
            ObjectCreationStrategy objectCreationStrategy)
        {
            var args = new object[] { };
            if (type.GetConstructors(ExpectedBindingFlags).Any())
            {
                args = type.CreateCtorArguments(defaultData, objectCreationStrategy);
            }

            T result = default(T);
            try
            {
                result = (T)Activator.CreateInstance(type, args);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Could not create expected type: '{type.FullName}'");
            }

            return result;
        }
    }
}
