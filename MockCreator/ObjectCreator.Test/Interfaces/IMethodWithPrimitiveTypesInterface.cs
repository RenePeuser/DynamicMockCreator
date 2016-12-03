using System;

namespace ObjectCreatorTest.Interfaces
{
    public interface IMethodWithPrimitiveTypesInterface
    {
        sbyte MethodReturnsSByte();
        byte MethodReturnsByte();
        short MethodReturnsShort();
        ushort MethodReturnsUShort();
        int MethodReturnsInt();
        uint MethodReturnsUInt();
        long MethodReturnsLong();
        ulong MethodReturnsULong();
        char MethodReturnsChar();
        float MethodReturnsFloat();
        double MethodReturnsDouble();
        bool MethodReturnsBool();
        string MethodReturnsString();
        decimal MethodReturnsDecimal();
        DateTime MethodReturnsDateTime();
    }
}