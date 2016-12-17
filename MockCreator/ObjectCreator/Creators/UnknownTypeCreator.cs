using System;
using System.Collections;
using System.Linq;
using Extensions;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class UnknownTypeCreator
    {
        internal static T CreateDynamicFrom<T>(Type type, IDefaultData defaultData,
            ObjectCreatorMode objectCreatorMode)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                var returnValue = EnumerableCreator.Create<T>(type, defaultData, objectCreatorMode);
                if (returnValue != null)
                {
                    return returnValue;
                }
            }

            var args = new object[] { };
            if (type.GetConstructors().Any())
            {
                args = type.CreateCtorArguments(defaultData, objectCreatorMode);
            }

            var result = (T)Activator.CreateInstance(type, args);

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
