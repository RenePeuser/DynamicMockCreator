﻿using System;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class ArrayCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (!type.IsArray)
            {
                return default(T);
            }

            var elementType = type.GetElementType();
            var array = Array.CreateInstance(elementType, objectCreationStrategy.EnumerationCount);
            for (var i = 0; i < objectCreationStrategy.EnumerationCount; i++)
            {
                var arrayItem = elementType.Create(defaultData, objectCreationStrategy);
                array.SetValue(arrayItem, i);
            }
            return (T)(object)array;
        }
    }
}
