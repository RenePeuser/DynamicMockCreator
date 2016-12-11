using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectCreator.Extensions
{
    public static class FuncExtensions
    {
        private static readonly IEnumerable<Type> FuncDeclarations = new[]
        {
            typeof(Func<>),
            typeof(Func<,>),
            typeof(Func<,,>),
            typeof(Func<,,,>),
            typeof(Func<,,,,>),
            typeof(Func<,,,,,>),
            typeof(Func<,,,,,,>),
            typeof(Func<,,,,,,,>),
            typeof(Func<,,,,,,,,>),
            typeof(Func<,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,,>)
        };

        public static bool IsFunc(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            var genericTypeDefintion = type.GetGenericTypeDefinition();
            return FuncDeclarations.Contains(genericTypeDefintion);
        }
    }
}
