using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Extensions;
using NSubstitute;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class SubstituteExtensions
    {
        public static T For<T>()
        {
            return For<T>(null);
        }

        public static T For<T>(IDefaultData defaultData)
        {
            var type = typeof(T);
            var defaultValue = type.GetDefaultValue(defaultData);
            if (defaultValue != null)
            {
                return (T)defaultValue;
            }

            if (type.IsInterface)
            {
                return CreateFromInterface<T>(type, defaultData);
            }

            if (type.IsAbstract)
            {
                return CreateFromAbstractClass<T>(type, defaultData);
            }

            if (type.IsArray)
            {
                return CreateFromArray<T>(type, defaultData);
            }

            return CreateDynamicFrom<T>(type, defaultData);
        }

        public static object For(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return For(type, null);
        }

        public static object For(this Type type, IDefaultData defaultData)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return typeof(SubstituteExtensions).InvokeExpectedMethod(nameof(SubstituteExtensions.For), new[] { type }, defaultData);
        }

        private static T CreateFromArray<T>(Type type, IDefaultData defaultData)
        {
            var elementType = type.GetElementType();
            var array = Array.CreateInstance(elementType, 5);
            for (var i = 0; i < 5; i++)
            {
                var arrayItem = For(elementType, defaultData);
                array.SetValue(arrayItem, i);
            }
            return (T)(object)array;
        }

        private static T CreateFromAbstractClass<T>(this Type type, IDefaultData defaultData)
        {
            var args = type.CreateCtorArguments(defaultData);
            return typeof(Substitute).InvokeGenericMethod<T>(nameof(Substitute.ForPartsOf), new[] { type },
                new object[] { args });
        }

        private static T CreateDynamicFrom<T>(this Type type, IDefaultData defaultData)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                return CreateEnumeration<T>(type, defaultData);
            }

            var args = new object[] { };
            if (type.GetConstructors().Any())
            {
                args = type.CreateCtorArguments(defaultData);
            }

            var result = (T)Activator.CreateInstance(type, args);
            result.InitProperties(defaultData);
            return result;
        }

        private static object[] CreateCtorArguments(this Type type, IDefaultData defaultData)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments(defaultData);
        }

        private static object[] CreateArguments(this MethodBase methodBase, IDefaultData defaultData)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(item => For(item.ParameterType, defaultData));
            return arguments.ToArray();
        }

        private static T CreateFromInterface<T>(this Type argumentType, IDefaultData defaultData)
        {
            // This is special we dont need a proxy for interfaces of IEnumerable.
            if (typeof(T).IsInterfaceImplemented<IEnumerable>())
            {
                return CreateEnumeration<T>(argumentType, defaultData);
            }

            var mock = typeof(Substitute).InvokeGenericMethod<T>(nameof(Substitute.For), new[] { argumentType },
                new object[] { new object[] { } });

            // Check solution for that case and all scenarios.
            if (typeof(T).IsSystemType())
            {
                return mock;
            }

            mock.SetupProperties(defaultData);
            mock.SetupMethods(defaultData);
            return mock;
        }

        private static T CreateEnumeration<T>(Type enumerationType, IDefaultData defaultData)
        {
            var genericArguments = enumerationType.GetGenericArguments();
            if (!genericArguments.Any())
            {
                return (T)CreateEnumeration(typeof(object), defaultData);
            }

            var enumeration = CreateEnumeration(enumerationType, defaultData);
            var result = typeof(EnumerableExtensions).InvokeGenericMethod(nameof(EnumerableExtensions.ToListOfType),
                new[] { genericArguments.First() }, enumeration);

            if (enumerationType.IsInterfaceImplemented<ICollection>())
            {
                return (T)Activator.CreateInstance(enumerationType, result);
            }

            return (T)result;
        }

        private static IEnumerable CreateEnumeration(Type enumerationType, IDefaultData defaultData)
        {
            var genericArgument = enumerationType.GetGenericArguments().First();
            for (var i = 0; i < 5; i++)
            {
                var result = For(genericArgument, defaultData);
                yield return result;
            }
        }
    }
}