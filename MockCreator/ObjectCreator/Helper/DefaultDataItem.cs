namespace ObjectCreator.Helper
{
    public class DefaultDataItem<TType, TValue>
    {
        public DefaultDataItem(TType type, TValue value)
        {
            Type = type;
            Value = value;
        }

        public TType Type { get; }

        public TValue Value { get; }
    }
}
