using System;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class ArrayCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!type.IsArray)
            {
                return default(T);
            }

            var elementType = type.GetElementType();
            var array = Array.CreateInstance(elementType, 5);
            for (var i = 0; i < 5; i++)
            {
                var arrayItem = elementType.Create(defaultData, objectCreatorMode);
                array.SetValue(arrayItem, i);
            }
            return (T)(object)array;
        }
    }
}
