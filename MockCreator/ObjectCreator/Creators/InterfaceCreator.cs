using System;
using System.Collections;
using Extensions;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    public static class InterfaceCreator
    {
        private static readonly Func<Type, object> ForFunc = genericType => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] { genericType }, new object[] { new object[] { } });

        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!type.IsInterface)
            {
                return default(T);
            }

            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                var result = EnumerableCreator.Create<T>(type, defaultData, objectCreatorMode);
                if (result != null)
                {
                    return result;
                }
            }

            if (type.IsInterfaceImplemented<IEnumerator>())
            {
                var result = EnumeratorCreator.Create<T>(type, defaultData, objectCreatorMode);
                if (result != null)
                {
                    return result;
                }
            }

            return CreateProxy<T>(type, defaultData, objectCreatorMode);
        }

        private static T CreateProxy<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var proxy = (T)ForFunc(type);
            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                    proxy.SetupProperties(defaultData, objectCreatorMode);
                    proxy.SetupMethods(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithProperties:
                    proxy.SetupProperties(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithMethods:
                    proxy.SetupMethods(defaultData, objectCreatorMode);
                    break;
            }
            return proxy;
        }
    }
}
