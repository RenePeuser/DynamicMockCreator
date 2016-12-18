using System;
using System.Collections;
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
            ObjectCreatorMode objectCreatorMode)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                var enumerable = EnumerableCreator.Create<T>(type, defaultData, objectCreatorMode);
                if (enumerable != null)
                {
                    return enumerable;
                }
            }

            if (type.IsInterfaceImplemented<IEnumerator>())
            {
                var enumerator = EnumeratorCreator.Create<T>(type, defaultData, objectCreatorMode);
                if (enumerator != null)
                {
                    return enumerator;
                }
            }

            var expectedObject = CreateDynamic<T>(type, defaultData, objectCreatorMode);
            if (expectedObject == null)
            {
                return expectedObject;
            }

            return InitObject(defaultData, objectCreatorMode, expectedObject);
        }

        private static T InitObject<T>(IDefaultData defaultData, ObjectCreatorMode objectCreatorMode, T expectedObject)
        {
            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                case ObjectCreatorMode.WithProperties:
                    expectedObject.InitProperties(defaultData);
                    break;
            }
            return expectedObject;
        }

        private static T CreateDynamic<T>(Type type, IDefaultData defaultData,
            ObjectCreatorMode objectCreatorMode)
        {
            var args = new object[] { };
            if (type.GetConstructors(ExpectedBindingFlags).Any())
            {
                args = type.CreateCtorArguments(defaultData, objectCreatorMode);
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
