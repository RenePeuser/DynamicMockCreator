using System;
using System.Linq;
using System.Reflection;
using MockCreator.Helper;
using NSubstitute;

namespace MockCreator.Extensions
{
    public static class SubstituteExtensions
    {
        public static T For<T>(DefaultData defaultData = null)
        {
            var type = typeof(T);
            var defaultValue = type.GetDefaultValue(defaultData);
            if (defaultValue != null)
            {
                return (T) defaultValue;
            }

            if (type.IsInterface)
            {
                return CreateFromInterface<T>(type);
            }

            if (type.IsAbstract)
            {
                return CreateFromAbstractClass<T>(type, defaultData);
            }

            return CreateDynamicFrom<T>(type, defaultData);
        }

        private static object For(this Type type, params object[] args)
        {
            return typeof(SubstituteExtensions).InvokeGenericMethod(nameof(SubstituteExtensions.For), new[] {type}, args);
        }

        private static T CreateFromAbstractClass<T>(this Type type, DefaultData defaultData)
        {
            var args = type.CreateCtorArguments(defaultData);
            return typeof(Substitute).InvokeGenericMethod<T>(nameof(Substitute.ForPartsOf), new[] {type},
                new object[] {args});
        }

        private static T CreateDynamicFrom<T>(this Type type, DefaultData defaultData)
        {
            var args = type.CreateCtorArguments(defaultData);
            return (T) Activator.CreateInstance(type, args);
        }

        private static object[] CreateCtorArguments(this Type type, DefaultData defaultData)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments(defaultData);
        }

        private static object[] CreateArguments(this ConstructorInfo constructorInfo, DefaultData defaultData)
        {
            var parameterInfos = constructorInfo.GetParameters();
            var arguments = parameterInfos.Select(item => For(item.ParameterType, defaultData));
            return arguments.ToArray();
        }

        private static T CreateFromInterface<T>(this Type argumentType)
        {
            return typeof(Substitute).InvokeGenericMethod<T>(nameof(Substitute.For), new[] {argumentType},
                new object[] {new object[] {}});
        }
    }
}