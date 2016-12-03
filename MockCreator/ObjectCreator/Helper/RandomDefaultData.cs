using System;
using System.Collections.Generic;
using Extensions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Helper
{
    public class RandomDefaultData : IDefaultData
    {
        private static readonly Random Random = new Random();

        private static readonly string[] StringArray = { "WPF", "DesignTime", "C#", "XAML", "Visual", "Studio", "Microsoft", "Function", "Action", "Lambda", "Class", "Struct", "Enum", "Mobile", "Phone", "XMl", "Cool" };

        private static readonly char[] CharArray = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };

        private static readonly Dictionary<Type, Func<object>> RandomDataDictionary = new Dictionary<Type, Func<object>>()
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
            { typeof(bool), () => NextBool() },
            { typeof(string), NextString },
            { typeof(DateTime), () => NextDateTime() }
        };

        public object GetDefaultValue(Type type)
        {
            var result = RandomDataDictionary.GetValueOrDefault(type);
            return result?.Invoke();
        }

        private static double NextDouble()
        {
            return Math.Round((Random.NextDouble() * 100), 2);
        }

        private static char NextChar()
        {
            var nextIndex = Random.Next(0, CharArray.Length - 1);
            return CharArray[nextIndex];
        }

        private static string NextString()
        {
            var nextIndex = Random.Next(0, StringArray.Length - 1);
            return StringArray[nextIndex];
        }

        private static bool NextBool()
        {
            var random = Random.Next(0, 1);
            return Convert.ToBoolean(random);
        }

        private static DateTime NextDateTime()
        {
            var day = Random.Next(1, 28);
            var month = Random.Next(1, 12);
            var year = Random.Next(2016, 2020);
            return new DateTime(year, month, day);
        }
    }
}