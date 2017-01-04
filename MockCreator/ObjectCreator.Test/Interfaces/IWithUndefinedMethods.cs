namespace ObjectCreatorTest.Interfaces
{
    public interface IWithUndefinedMethods
    {
        T CreateType<T>();

        T CreateTypeWithArgs<T>(T item);
    }

    public interface IUndefinedGenericInterface<out T>
    {
        T Property { get; }
    }
}
