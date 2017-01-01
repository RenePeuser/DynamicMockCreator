using System;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public static class DefaultValueHelper
    {
        private static readonly DefaultData DefaultData = new DefaultData(
            new TypeToValue(typeof(int), 1),
            new TypeToValue(typeof(double), 1.4),
            new TypeToValue(typeof(byte), (byte)1),
            new TypeToValue(typeof(sbyte), (sbyte)1),
            new TypeToValue(typeof(short), (short)1),
            new TypeToValue(typeof(ushort), (ushort)1),
            new TypeToValue(typeof(uint), (uint)1),
            new TypeToValue(typeof(long), (long)1.1),
            new TypeToValue(typeof(ulong), (ulong)1.2),
            new TypeToValue(typeof(char), 'c'),
            new TypeToValue(typeof(float), (float)1.3),
            new TypeToValue(typeof(bool), true),
            new TypeToValue(typeof(decimal), new decimal(1.5)),
            new TypeToValue(typeof(string), "MyString"),
            new TypeToValue(typeof(object), new object()),
            new TypeToValue(typeof(DateTime), new DateTime(2016, 10, 15)),
            new TypeToValue(typeof(Guid), Guid.NewGuid()),
            new TypeToValue(typeof(Uri), new Uri("http://www.google.com/")));

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