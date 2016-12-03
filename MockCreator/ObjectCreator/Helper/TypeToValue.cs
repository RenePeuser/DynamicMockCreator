using System;

namespace ObjectCreator.Helper
{
    public class TypeToValue
    {
        public TypeToValue(Type type, object value)
        {
            Type = type;
            Value = value;
        }

        public Type Type { get; }

        public object Value { get; }
    }
}
