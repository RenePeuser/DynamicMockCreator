using System;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    internal static class AbstractClassCreator
    {
        private static readonly Func<Type, object[], object> ForPartsOfFunc = (genericType, arguments) => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] { genericType }, new object[] { arguments });

        internal static object Create(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!type.IsAbstract)
            {
                return null;
            }

            // ToDo: check if necessary !!
            var args = type.CreateCtorArguments(defaultData, objectCreatorMode);
            var result = ForPartsOfFunc(type, args);

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                case ObjectCreatorMode.WithProperties:
                    result.InitProperties(defaultData);
                    break;
            }

            return result;
        }

        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var args = type.CreateCtorArguments(defaultData, objectCreatorMode);
            var result = (T)ForPartsOfFunc(type, args);

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
