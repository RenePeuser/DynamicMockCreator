using System;
using System.Collections.Generic;

namespace MockCreator
{
    public static class DefaultValueHelper
    {
        private static readonly Dictionary<Type, object> DefaultValues = new Dictionary<Type, object>
        {
            {typeof(sbyte), (sbyte) 42},
            {typeof(byte), (byte) 43},
            {typeof(short), (short) 44},
            {typeof(ushort), (ushort) 45},
            {typeof(int), 46},
            {typeof(uint), (uint) 47},
            {typeof(long), (long) 48.1},
            {typeof(ulong), (ulong) 49.2},
            {typeof(char), 'c'},
            {typeof(float), (float) 50.3},
            {typeof(double), 51.4},
            {typeof(bool), true},
            {typeof(decimal), new decimal(52.5)},
            {typeof(string), "MyString"},
            {typeof(DateTime), new DateTime(2016, 10, 15)}
        };

        public static object GetDefaultValue(Type type)
        {
            if (DefaultValues.ContainsKey(type))
            {
                return DefaultValues[type];
            }
            return null;
        }
    }
}