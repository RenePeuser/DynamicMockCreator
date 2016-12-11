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

        internal static T Create<T>(Type argumentType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!argumentType.IsInterface)
            {
                return default(T);
            }

            var returnValue = default(T);
            if (argumentType.IsInterfaceImplemented<IEnumerable>())
            {
                returnValue = EnumerableCreator.Create<T>(argumentType, defaultData, objectCreatorMode);
            }

            if (returnValue == null)
            {
                returnValue = (T)ForFunc(argumentType);
                switch (objectCreatorMode)
                {
                    case ObjectCreatorMode.All:
                        returnValue.SetupProperties(defaultData, objectCreatorMode);
                        returnValue.SetupMethods(defaultData, objectCreatorMode);
                        break;
                    case ObjectCreatorMode.WithProperties:
                        returnValue.SetupProperties(defaultData, objectCreatorMode);
                        break;
                    case ObjectCreatorMode.WithMethods:
                        returnValue.SetupMethods(defaultData, objectCreatorMode);
                        break;
                }
            }

            return returnValue;
        }

        //internal static T Create<T>(Type argumentType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        //{
        //    if (!argumentType.IsInterface)
        //    {
        //        return default(T);
        //    }

        //    var mock = (T)ForFunc(argumentType);

        //    // Check solution for that case and all scenarios.
        //    if (typeof(T).IsSystemType())
        //    {
        //        return mock;
        //    }

        //    switch (objectCreatorMode)
        //    {
        //        case ObjectCreatorMode.All:
        //            mock.SetupProperties(defaultData, objectCreatorMode);
        //            mock.SetupMethods(defaultData, objectCreatorMode);
        //            break;
        //        case ObjectCreatorMode.WithProperties:
        //            mock.SetupProperties(defaultData, objectCreatorMode);
        //            break;
        //        case ObjectCreatorMode.WithMethods:
        //            mock.SetupMethods(defaultData, objectCreatorMode);
        //            break;
        //    }

        //    return mock;
        //}
    }
}
