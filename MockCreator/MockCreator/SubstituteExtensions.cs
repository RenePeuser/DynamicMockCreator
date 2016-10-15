using System;
using System.Linq;
using System.Reflection;
using NSubstitute;

namespace MockCreator
{
    public static class SubstituteExtensions
    {
        public static object For<T>()
        {
            var type = typeof(T);
            var defaultValue = type.GetDefaultValue();
            if (defaultValue != null)
            {
                return defaultValue;
            }

            if (type.IsInterface)
            {
                return CreateFromInterface(type);
            }

            if (type.IsAbstract)
            {
                return CreateFromAbstractClass(type);
            }

            return CreateDynamicFrom(type);
        }

        private static object For(this Type type, params object[] args)
        {
            return typeof(SubstituteExtensions).InvokeGenericMethod(nameof(Substitute.For), new[] {type}, args);
        }

        private static object CreateFromAbstractClass(this Type type)
        {
            var args = type.CreateCtorArguments();
            return typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] {type},
                new object[] {args});
        }

        private static object CreateDynamicFrom(this Type type)
        {
            var args = type.CreateCtorArguments();
            return Activator.CreateInstance(type, args);
        }

        private static object[] CreateCtorArguments(this Type type)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments();
        }

        private static object[] CreateArguments(this ConstructorInfo constructorInfo)
        {
            var parameterInfos = constructorInfo.GetParameters();
            var arguments = parameterInfos.Select(item => For(item.ParameterType));
            return arguments.ToArray();
        }

        private static object CreateFromInterface(this Type argumentType)
        {
            return typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] {argumentType},
                new object[] {new object[] {}});
        }
    }
}