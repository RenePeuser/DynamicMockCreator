using System;
using System.Linq;
using System.Reflection;
using NSubstitute;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class ObjectCreatorSetupExtensions
    {
        private static readonly Action<Type, object, object, Array> ReturnsFunc = (methodReturnType, methodReturnValue, returnValue, array) => typeof(SubstituteExtensions).InvokeGenericMethod(nameof(SubstituteExtensions.Returns), new[] { methodReturnType }, methodReturnValue, returnValue, array);
        private static readonly Func<Type, object> ArgAnyFunc = parameterType => typeof(Arg).InvokeGenericMethod(nameof(Arg.Any), new[] { parameterType });
        private static readonly Func<Type, object> ForFunc = genericType => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] { genericType }, new object[] { new object[] { } });

        public static void InitProperties<T>(this T source, IDefaultData defaultData)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.GetType().IsSystemType())
            {
                return;
            }

            var properties = source.GetType().GetProperties().Where(prop => prop.CanWrite);
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                var newValue = propertyType.Create(defaultData);
                propertyInfo.SetValue(source, newValue);
            }
        }

        public static void SetupProperties<T>(this T mock, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var allProperties = typeof(T).GetAllProperties();

            foreach (var propertyInfo in allProperties)
            {
                var propertyType = propertyInfo.PropertyType;
                var returnValue = propertyType.Create(defaultData, objectCreatorMode);
                var propertyValue = propertyInfo.GetValue(mock);
                var array = Array.CreateInstance(propertyType, 0);
                ReturnsFunc(propertyType, propertyValue, returnValue, array);
            }
        }

        public static void SetupMethods<T>(this T mock, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var allMethods = typeof(T).GetAllMethods();

            foreach (var methodInfo in allMethods)
            {
                var methodReturnType = methodInfo.ReturnType;
                object returnValue;
                if (methodReturnType.IsInterface)
                {
                    returnValue = ForFunc(methodReturnType);
                    returnValue.SetupProperties(defaultData, objectCreatorMode);
                }
                else
                {
                    returnValue = methodReturnType.Create(defaultData, objectCreatorMode);
                }

                var arguments = methodInfo.CreateAnyArgs();
                var methodReturnValue = methodInfo.Invoke(mock, arguments);
                var array = methodReturnType.ToArray(0);
                ReturnsFunc(methodReturnType, methodReturnValue, returnValue, array);
            }
        }

        /// <summary>
        /// Creates any arguments from <see cref="Arg.Any{T}"/>
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>The parameter array for a specific method.</returns>
        private static object[] CreateAnyArgs(this MethodBase methodBase)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(param => ArgAnyFunc(param.ParameterType));
            return arguments.ToArray();
        }
    }
}
