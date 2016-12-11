using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectCreator.Extensions
{
    public static class ActionExtensions
    {
        private static readonly IEnumerable<Type> ActionDeclarations = new[]
        {
            typeof(Action),
            typeof(Action<>),
            typeof(Action<,>),
            typeof(Action<,,>),
            typeof(Action<,,,>),
            typeof(Action<,,,,>),
            typeof(Action<,,,,,>),
            typeof(Action<,,,,,,>),
            typeof(Action<,,,,,,,>),
            typeof(Action<,,,,,,,,>),
            typeof(Action<,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,,>)
        };

        public static bool IsAction(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type == typeof(Action);
            }

            var genericTypeDefintion = type.GetGenericTypeDefinition();
            return ActionDeclarations.Contains(genericTypeDefintion);
        }
    }
}
