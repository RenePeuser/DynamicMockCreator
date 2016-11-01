using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Extensions;
using NSubstitute;
using ObjectCreator.Helper;

namespace ObjectCreator.Extensions
{
    public static class SubstituteExtensions
    {
        public static T For<T>(DefaultData defaultData = null)
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

            return For(type, new object[] { null });
        }

        public static object For(this Type type, params object[] args)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return typeof(SubstituteExtensions).InvokeGenericMethod(nameof(SubstituteExtensions.For), new[] { type }, args);
        }

        public static void InitPropertries<T>(this T source, DefaultData defaultData = null)
        {
            if (source.GetType().IsSystemType())
            {
                return;
            }

            var properties = source.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                var newValue = For(propertyType, defaultData);
                if (propertyInfo.PropertyType.IsNotSystemType() && propertyInfo.PropertyType.IsNotArray())
                {
                    newValue.InitPropertries();
                }
                propertyInfo.SetValue(source, newValue);
            }
        }

        private static T CreateFromArray<T>(Type type, DefaultData defaultData)
        {
            var elementType = type.GetElementType();
            var array = Array.CreateInstance(elementType, 5);
            for (int i = 0; i < 5; i++)
            {
                var arrayItem = For(elementType, defaultData);
                arrayItem.InitPropertries();
                array.SetValue(arrayItem, i);
            }
            return (T)(object)array;
        }

        private static T CreateFromAbstractClass<T>(this Type type, DefaultData defaultData)
        {
            var args = type.CreateCtorArguments(defaultData);
            return typeof(Substitute).InvokeGenericMethod<T>(nameof(Substitute.ForPartsOf), new[] { type },
                new object[] { args });
        }

        private static T CreateDynamicFrom<T>(this Type type, DefaultData defaultData)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                return CreateEnumeration<T>(type, defaultData);
            }

            object[] args = { };
            if (type.GetConstructors().Any())
            {
                args = type.CreateCtorArguments(defaultData);
            }

            var result = (T)Activator.CreateInstance(type, args);
            return result;
        }

        private static object[] CreateCtorArguments(this Type type, DefaultData defaultData)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments(defaultData);
        }

        private static object[] CreateArguments(this MethodBase methodBase, DefaultData defaultData)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(item => For(item.ParameterType, defaultData));
            return arguments.ToArray();
        }

        private static object[] CreateAnyArgs(this MethodBase methodBase)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments =
                parameterInfos.Select(
                    param => typeof(Arg).InvokeGenericMethod(nameof(Arg.Any), new[] { param.ParameterType }));
            return arguments.ToArray();
        }

        private static T CreateFromInterface<T>(this Type argumentType, DefaultData defaultData)
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

        private static T CreateEnumeration<T>(Type enumerationType, DefaultData defaultData)
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

        private static IEnumerable CreateEnumeration(Type enumerationType, DefaultData defaultData)
        {
            var genericArgument = enumerationType.GetGenericArguments().First();
            for (var i = 0; i < 5; i++)
            {
                var result = For(genericArgument, defaultData);
                result.InitPropertries();
                yield return result;
            }
        }

        private static void SetupProperties<T>(this T mock, DefaultData defaultData)
        {
            var properties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                var returnValue = For(propertyType, defaultData);
                var propertyValue = propertyInfo.GetValue(mock);
                var array = Array.CreateInstance(propertyType, 0);

                typeof(NSubstitute.SubstituteExtensions).InvokeGenericMethod(
                    nameof(NSubstitute.SubstituteExtensions.Returns), new[] { propertyType }, propertyValue, returnValue,
                    array);
            }
        }

        private static void SetupMethods<T>(this T mock, DefaultData defaultData)
        {
            var methods = typeof(T).GetMethods().Where(m => (m.ReturnType != typeof(void)) && !m.IsSpecialName);
            foreach (var methodInfo in methods)
            {
                var methodReturnType = methodInfo.ReturnType;
                var returnValue = For(methodReturnType, defaultData);
                var arguments = methodInfo.CreateAnyArgs();
                var methodReturnValue = methodInfo.Invoke(mock, arguments);
                var array = Array.CreateInstance(methodReturnType, 0);
                typeof(NSubstitute.SubstituteExtensions).InvokeGenericMethod(
                    nameof(NSubstitute.SubstituteExtensions.Returns), new[] { methodReturnType }, methodReturnValue,
                    returnValue, array);
            }
        }
    }
}