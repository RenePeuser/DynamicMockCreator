namespace ObjectCreatorTest.Interfaces
{
    public interface IWithUndefinedMethods
    {
        T CreateType<T>();

        T CreateTypeWithArgs<T>(T item);
    }
}
