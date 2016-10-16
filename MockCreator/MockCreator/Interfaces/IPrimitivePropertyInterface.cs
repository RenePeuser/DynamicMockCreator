using System;

namespace MockCreator.Interfaces
{
    public interface IPrimitivePropertyInterface
    {
        sbyte PropertySByte { get; }
        byte PropertyByte { get; }
        short PropertyShort { get; }
        ushort PropertyUShort { get; }
        int PropertyInt { get; }
        uint PropertyUInt { get; }
        long PropertyLong { get; }
        ulong PropertyULong { get; }
        char PropertyChar { get; }
        float PropertyFloat { get; }
        double PropertyDouble { get; }
        bool PropertyBool { get; }
        string PropertyString { get; }
        decimal PropertyDecimal { get; }
        DateTime PropertyDateTime { get; }
    }
}