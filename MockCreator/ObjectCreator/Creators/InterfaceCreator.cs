using System;
using System.Collections;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    public static class InterfaceCreator
    {
        private static readonly Func<Type, object> ForFunc = genericType => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] { genericType }, new object[] { new object[] { } });

        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (!type.IsInterface)
            {
                return default(T);
            }

            if (type.IsIEnumerable())
            {
                var result = EnumerableCreator.Create<T>(type, defaultData, objectCreationStrategy);
                if (result != null)
                {
                    return result;
                }
            }

            if (type.IsIEnumerator())
            {
                var result = EnumeratorCreator.Create<T>(type, defaultData, objectCreationStrategy);
                if (result != null)
                {
                    return result;
                }
            }

            return CreateProxy<T>(type, defaultData, objectCreationStrategy);
        }

        private static T CreateProxy<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var proxy = (T)ForFunc(type);

            if (objectCreationStrategy.SetupProperties)
            {
                proxy.SetupProperties(defaultData, objectCreationStrategy);
            }

            if (objectCreationStrategy.SetupMethods)
            {
                proxy.SetupMethods(defaultData, objectCreationStrategy);
            }

            return proxy;
        }
    }
}
