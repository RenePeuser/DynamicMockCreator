using System;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    public static class UndefinedTypeCreator
    {
        public static object Create(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (objectCreationStrategy.UndefinedType == null)
            {
                return null;
            }

            if (type.IsGenericType)
            {
                return type.MakeGenericType(objectCreationStrategy.UndefinedType).Create(defaultData, objectCreationStrategy);
            }

            return objectCreationStrategy.UndefinedType.Create(defaultData, objectCreationStrategy);
        }
    }
}
