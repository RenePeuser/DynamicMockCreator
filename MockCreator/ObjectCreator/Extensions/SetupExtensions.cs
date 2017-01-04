using NSubstitute;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace ObjectCreator.Extensions
{
    internal static class SetupExtensions
    {
        private static readonly Action<Type, object, object, Array> ReturnsFunc = (methodReturnType, source, returnValue, array) => typeof(SubstituteExtensions).InvokeGenericMethod(nameof(SubstituteExtensions.Returns), new[] { methodReturnType }, source, returnValue, array);
        private static readonly Func<Type, object> ArgAnyFunc = parameterType => typeof(Arg).InvokeGenericMethod(nameof(Arg.Any), new[] { parameterType });
        private static readonly Func<Type, object> ForFunc = genericType => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] { genericType }, new object[] { new object[] { } });

        internal static T InitProperties<T>(this T source, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (typeof(T).IsSystemType())
            {
                return source;
            }

            var properties = source.GetType().GetProperties().Where(prop => prop.CanWrite);
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.GetIndexParameters().Any())
                {
                    continue;
                }

                var propertyType = propertyInfo.PropertyType;
                var propertyValue = propertyInfo.GetValue(source);
                if (!propertyType.IsDefaultValue(propertyValue))
                {
                    continue;
                }

                var newValue = propertyType.Create(defaultData, objectCreationStrategy);
                propertyInfo.SetValue(source, newValue);
            }

            return source;
        }

        internal static T SetupProperties<T>(this T mock, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var allProperties = typeof(T).GetAllProperties();

            foreach (var propertyInfo in allProperties)
            {
                var propertyType = propertyInfo.PropertyType;
                var returnValue = propertyType.Create(defaultData, objectCreationStrategy);
                var indexParameter = propertyInfo.GetIndexParameters().Select(p => p.ParameterType.Create()).ToArray();
                var propertyValue = propertyInfo.GetValue(mock, indexParameter);
                var array = Array.CreateInstance(propertyType, 0);
                ReturnsFunc(propertyType, propertyValue, returnValue, array);
            }

            return mock;
        }

        internal static T SetupMethods<T>(this T mock, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var allMethods = typeof(T).GetAllMethods();

            foreach (var methodInfo in allMethods)
            {
                if (methodInfo.ReturnType == typeof(void))
                {
                    continue;
                }

                if (methodInfo.ReturnType.IsUndefined())
                {
                    if (objectCreationStrategy.UndefinedType == null)
                    {
                        continue;
                    }

                    var newMethodInfo = methodInfo.MakeGenericMethod(objectCreationStrategy.UndefinedType);
                    SetupMethod(mock, defaultData, objectCreationStrategy, newMethodInfo);
                }
                else
                {
                    SetupMethod(mock, defaultData, objectCreationStrategy, methodInfo);
                }
            }

            return mock;
        }

        private static void SetupMethod<T>(this T mock, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy, MethodInfo methodInfo)
        {
            object returnValue;
            var methodReturnType = methodInfo.ReturnType;
            if (methodReturnType.IsInterface)
            {
                returnValue = ForFunc(methodReturnType);
                returnValue.SetupProperties(defaultData, objectCreationStrategy);
            }
            else
            {
                returnValue = methodReturnType.Create(defaultData, objectCreationStrategy);
            }

            var arguments = methodInfo.CreateAnyArgs();
            var methodReturnValue = methodInfo.Invoke(mock, arguments);
            var array = methodReturnType.ToArray(0);
            ReturnsFunc(methodReturnType, methodReturnValue, returnValue, array);
        }

        private static object[] CreateAnyArgs(this MethodBase methodBase)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(param => ArgAnyFunc(param.ParameterType));
            return arguments.ToArray();
        }
    }
}
