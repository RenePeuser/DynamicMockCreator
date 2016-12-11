using System;
using System.Collections;
using Extensions;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public static class InterfaceCreator
    {
        private static readonly Func<Type, object> ForFunc = genericType => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] { genericType }, new object[] { new object[] { } });

        internal static object Create(Type argumentType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!argumentType.IsInterface)
            {
                return null;
            }

            object returnValue = null;
            if (argumentType.IsInterfaceImplemented<IEnumerable>())
            {
                returnValue = EnumerableCreator.Create(argumentType, defaultData, objectCreatorMode);
            }

            if (returnValue == null)
            {
                returnValue = ForFunc(argumentType);
            }

            return returnValue;
        }

        internal static object CreateOld(Type argumentType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!argumentType.IsInterface)
            {
                return null;
            }

            var mock = ForFunc(argumentType);

            // Check solution for that case and all scenarios.
            if (argumentType.IsSystemType())
            {
                return mock;
            }

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                    mock.SetupProperties(defaultData, objectCreatorMode);
                    mock.SetupMethods(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithProperties:
                    mock.SetupProperties(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithMethods:
                    mock.SetupMethods(defaultData, objectCreatorMode);
                    break;
            }

            return mock;
        }

        internal static T Create<T>(Type argumentType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!argumentType.IsInterface)
            {
                return default(T);
            }

            var mock = (T)ForFunc(argumentType);

            // Check solution for that case and all scenarios.
            if (typeof(T).IsSystemType())
            {
                return mock;
            }

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                    mock.SetupProperties(defaultData, objectCreatorMode);
                    mock.SetupMethods(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithProperties:
                    mock.SetupProperties(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithMethods:
                    mock.SetupMethods(defaultData, objectCreatorMode);
                    break;
            }

            return mock;
        }
    }
}
