using System;
using System.Collections.Generic;
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
            if (type.IsInterface)
            {
                return CreateFromInterface(type);
            }

            var defaultValue = type.GetDefaultValue();
            if (defaultValue != null)
            {
                return defaultValue;
            }

            if (type.IsAbstract)
            {
                return CreateFromAbstractClass(type);
            }

            return CreateDynamicFrom(type);
        }

        private static object For(this Type type, params object[] args)
        {
            var result = InvokeMethodHelper.InvokeGenericMethod(
                typeof(SubstituteExtensions),
                nameof(Substitute.For),
                new[] { type }, args);

            return result;
        }

        private static object CreateFromAbstractClass(this Type type)
        {
            var ctor = type.GetConstructor();
            var args = ctor.CreateArguments().ToArray();

            var result = InvokeMethodHelper.InvokeGenericMethod(
                typeof(Substitute),
                nameof(Substitute.ForPartsOf),
                new[] { type }, new object[] { args });

            return result;
        }

        private static ConstructorInfo GetConstructor(this Type type)
        {
            var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Static |
                                            BindingFlags.NonPublic | BindingFlags.Instance)
                           .First(item => !item.GetParameters().Any(p => p.ParameterType.IsPointer));
            return ctor;
        }

        private static object CreateDynamicFrom(this Type type)
        {
            var ctor = type.GetConstructor();
            var args = ctor.CreateArguments().ToArray();
            return Activator.CreateInstance(type, args);
        }

        private static IEnumerable<object> CreateArguments(this ConstructorInfo constructorInfo)
        {
            var parameterInfos = constructorInfo.GetParameters();
            var arguments = parameterInfos.Select(item => For(item.ParameterType));
            return arguments;
        }

        private static object CreateFromInterface(this Type argumentType)
        {
            var result = InvokeMethodHelper.InvokeGenericMethod(
                typeof(Substitute),
                nameof(Substitute.For),
                new[] { argumentType }, new object[] { new object[] { } });

            return result;
        }
    }
}