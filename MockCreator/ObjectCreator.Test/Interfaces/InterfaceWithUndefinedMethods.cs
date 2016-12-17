namespace ObjectCreatorTest.Interfaces
{
    public interface InterfaceWithUndefinedMethods
    {
        T CreateType<T>();

        T CreateTypeWithArgs<T>(T item);
    }
}
