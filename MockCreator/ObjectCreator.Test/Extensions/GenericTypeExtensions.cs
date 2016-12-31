using System;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectCreatorTest.Extensions
{
    public static class GenericTypeExtensions
    {
        public static PrivateObject ToPrivateObject<T>(this T source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new PrivateObject(source);
        }
    }
}
