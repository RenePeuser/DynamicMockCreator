using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Ink;
using Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public class UniqueDefaultData : IDefaultData
    {
        private static long _currentValue = 0;
        private static readonly Dictionary<Type, Func<object>> UniqueDataDictionary = new Dictionary<Type, Func<object>>()
        {
            { typeof(sbyte), () => (sbyte)NextDouble() },
            { typeof(byte), () => (byte)NextDouble() },
            { typeof(short), () => (short)NextDouble() },
            { typeof(ushort), () => (ushort)NextDouble() },
            { typeof(int), () => (int)NextDouble() },
            { typeof(uint), () => (uint)NextDouble() },
            { typeof(long), () => (long)NextDouble() },
            { typeof(float), () => (float)NextDouble() },
            { typeof(decimal), () => (decimal)NextDouble() },
            { typeof(double), () => NextDouble() },
            { typeof(char), () => NextChar() },
            { typeof(string), NextString },
            { typeof(Uri),NextUri },
        };

        public object GetDefaultValue(Type type)
        {
            var result = UniqueDataDictionary.GetValueOrDefault(type);
            return result?.Invoke();
        }

        private static double NextDouble()
        {
            return Interlocked.Increment(ref _currentValue);
        }

        private static char NextChar()
        {
            return (char)Interlocked.Increment(ref _currentValue);
        }

        private static string NextString()
        {
            var nextValue = Interlocked.Increment(ref _currentValue);
            return "MyString" + nextValue;

        }

        private static Uri NextUri()
        {
            var nextValue = Interlocked.Increment(ref _currentValue);
            return new Uri($"http{nextValue}://www.google.com/");
        }
    }
}