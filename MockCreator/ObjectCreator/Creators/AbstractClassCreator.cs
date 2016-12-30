using System;
using NSubstitute;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class AbstractClassCreator
    {
        private static readonly Func<Type, object[], object> ForPartsOfFunc = (genericType, arguments) => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] { genericType }, new object[] { arguments });

        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var args = type.CreateCtorArguments(defaultData, objectCreationStrategy);
            var result = (T)ForPartsOfFunc(type, args);
            result.Init(defaultData, objectCreationStrategy);
            return result;
        }

        private static T Init<T>(this T source, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (objectCreationStrategy.SetupProperties)
            {
                source.InitProperties(defaultData, objectCreationStrategy);
            }

            return source;
        }
    }
}
