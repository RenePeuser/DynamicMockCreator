using System;

namespace ObjectCreatorTest.DataClasses
{
    public class ClassWithPrimitiveTypes
    {
        public ClassWithPrimitiveTypes(
            sbyte sbyteValue,
            byte byteValue,
            short shortValue,
            ushort ushortValue,
            int intValue,
            uint uintValue,
            long longValue,
            ulong ulongValue,
            char charValue,
            float floatValue,
            double doubleValue,
            bool boolValue,
            decimal decimalValue,
            string stringValue,
            DateTime dateTimeValue)
        {
            SbyteValue = sbyteValue;
            ByteValue = byteValue;
            ShortValue = shortValue;
            UshortValue = ushortValue;
            IntValue = intValue;
            UintValue = uintValue;
            LongValue = longValue;
            UlongValue = ulongValue;
            CharValue = charValue;
            FloatValue = floatValue;
            DoubleValue = doubleValue;
            BoolValue = boolValue;
            DecimalValue = decimalValue;
            StringValue = stringValue;
            DateTimeValue = dateTimeValue;
        }

        public sbyte SbyteValue { get; }
        public byte ByteValue { get; }
        public short ShortValue { get; }
        public ushort UshortValue { get; }
        public int IntValue { get; }
        public uint UintValue { get; }
        public long LongValue { get; }
        public ulong UlongValue { get; }
        public char CharValue { get; }
        public float FloatValue { get; }
        public double DoubleValue { get; }
        public bool BoolValue { get; }
        public decimal DecimalValue { get; }
        public string StringValue { get; }
        public DateTime DateTimeValue { get; }
    }
}