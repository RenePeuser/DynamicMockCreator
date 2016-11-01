using System;
using System.Linq;
using System.Reflection;
using NSubstitute;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class SubstituteSetupExtensions
    {
        public static void InitProperties<T>(this T source, IDefaultData defaultData)
        {
            if (source.GetType().IsSystemType())
            {
                return;
            }

            var properties = source.GetType().GetProperties().Where(prop => prop.CanWrite);
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                var newValue = propertyType.For(defaultData);
                propertyInfo.SetValue(source, newValue);
            }
        }

        public static void SetupProperties<T>(this T mock, IDefaultData defaultData)
        {
            var properties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                var returnValue = propertyType.For(defaultData);
                var propertyValue = propertyInfo.GetValue(mock);
                var array = Array.CreateInstance(propertyType, 0);

                typeof(NSubstitute.SubstituteExtensions).InvokeGenericMethod(
                    nameof(NSubstitute.SubstituteExtensions.Returns), new[] { propertyType }, propertyValue, returnValue,
                    array);
            }
        }

        public static void SetupMethods<T>(this T mock, IDefaultData defaultData)
        {
            var methods = typeof(T).GetMethods().Where(m => (m.ReturnType != typeof(void)) && !m.IsSpecialName);
            foreach (var methodInfo in methods)
            {
                var methodReturnType = methodInfo.ReturnType;
                var returnValue = methodReturnType.For(defaultData);
                var arguments = methodInfo.CreateAnyArgs();
                var methodReturnValue = methodInfo.Invoke(mock, arguments);
                var array = Array.CreateInstance(methodReturnType, 0);
                typeof(NSubstitute.SubstituteExtensions).InvokeGenericMethod(
                    nameof(NSubstitute.SubstituteExtensions.Returns), new[] { methodReturnType }, methodReturnValue,
                    returnValue, array);
            }
        }

        private static object[] CreateAnyArgs(this MethodBase methodBase)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments =
                parameterInfos.Select(
                    param => typeof(Arg).InvokeGenericMethod(nameof(Arg.Any), new[] { param.ParameterType }));
            return arguments.ToArray();
        }
    }
}
