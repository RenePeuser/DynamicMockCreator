using System;
using System.Collections;
using System.Linq;
using Extensions;
using ObjectCreator.Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    internal static class UnknownTypeCreator
    {
        internal static object CreateDynamicFrom(Type type, IDefaultData defaultData,
            ObjectCreatorMode objectCreatorMode)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                var returnValue = EnumerableCreator.Create(type, defaultData, objectCreatorMode);
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

            var result = Activator.CreateInstance(type, args);

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
