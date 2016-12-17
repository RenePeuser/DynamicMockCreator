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
            var returnValue = EnumerableCreator.Create<T>(type, defaultData, objectCreatorMode);
            if (returnValue != null)
            {
                return returnValue;
            }

            var args = new object[] { };
            if (type.GetConstructors(ExpectedBindingFlags).Any())
            {
                args = type.CreateCtorArguments(defaultData, objectCreatorMode);
            }

            T result = default(T);
            try
            {
                result = (T) Activator.CreateInstance(type, args);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Could not create expected type: '{type.FullName}'");
                return result;
            }

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                case ObjectCreatorMode.WithProperties:
                    result.InitProperties(defaultData);
                    break;
            }

            return result;
        }
    }
}
