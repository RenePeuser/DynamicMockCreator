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

            var returnValue = default(T);
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                returnValue = EnumerableCreator.Create<T>(type, defaultData, objectCreatorMode);
            }

            if (returnValue == null)
            {
                returnValue = (T)ForFunc(type);
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

        //internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        //{
        //    if (!type.IsInterface)
        //    {
        //        return default(T);
        //    }

        //    var mock = (T)ForFunc(type);

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
