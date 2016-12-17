using System;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public static class DefaultValueHelper
    {
        private static readonly DefaultData DefaultData = new DefaultData(
            (sbyte)1,
            (byte)1,
            (short)1,
            (ushort)1,
            1,
            (uint)1,
            (long)1.1,
            (ulong)1.2,
            'c',
            (float)1.3,
            1.4,
            true,
            new decimal(1.5),
            "MyString",
            new DateTime(2016, 10, 15),
            new Uri("http://www.google.com/"));



        public static object GetDefaultValue(this Type type, IDefaultData customDefaultData)
        {
            var defaultValue = DefaultData.GetDefaultValue(type);
            if (customDefaultData == null)
            {
                return defaultValue;
            }

            var customValue = customDefaultData.GetDefaultValue(type);
            return customValue ?? defaultValue;
        }
    }
}