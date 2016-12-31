using System.Collections.Generic;

namespace ObjectCreator.Extensions
{
    public static class GenericTypeExtensions
    {
        public static bool Equal<T>(this T source, T target)
        {
            return EqualityComparer<T>.Default.Equals(source, target);
        }

        public static bool NotEquals<T>(this T source, T target)
        {
            return !source.Equal(target);
        }
    }
}
