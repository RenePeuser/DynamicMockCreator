namespace ObjectCreator.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object source) where T : class
        {
            var result = source as T;
            return result;
        }

        public static T Cast<T>(this object source)
        {
            return (T)source;
        }
    }
}