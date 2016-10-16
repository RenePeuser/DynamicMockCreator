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
                return CreateFromInterface<T>(type, defaultData);
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
                    param => typeof(Arg).InvokeGenericMethod(nameof(Arg.Any), new[] {param.ParameterType}));
            return arguments.ToArray();
        }

        private static T CreateFromInterface<T>(this Type argumentType, DefaultData defaultData)
        {
            var mock = typeof(Substitute).InvokeGenericMethod<T>(nameof(Substitute.For), new[] {argumentType},
                new object[] {new object[] {}});

            // return mock;

            // This is really crazy, but worked right now not in all Situation
            // Has to de differnce between Interface for .NET and own Implementations.
            // Some tests are not running. But base functionality is working.
            
            // Check solution for that case and all scenarios.
            if (typeof(T).FullName.StartsWith("System"))
            {
                return mock;
            }

            mock.SetupProperties(defaultData);
            mock.SetupMethods(defaultData);

            return mock;
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
                    nameof(NSubstitute.SubstituteExtensions.Returns), new[] {propertyType}, propertyValue, returnValue,
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
                var methodResturnValue = methodInfo.Invoke(mock, arguments);
                var array = Array.CreateInstance(methodReturnType, 0);
                typeof(NSubstitute.SubstituteExtensions).InvokeGenericMethod(
                    nameof(NSubstitute.SubstituteExtensions.Returns), new[] {methodReturnType}, methodResturnValue,
                    returnValue, array);
            }
        }
    }
}