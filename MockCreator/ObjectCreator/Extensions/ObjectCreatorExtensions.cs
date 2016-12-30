using Extensions;
using ObjectCreator.Creators;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;


namespace ObjectCreator.Extensions
{
    public static class ObjectCreatorExtensions
    {
        private static readonly ObjectCreationStrategy DefaultCreationStrategy = new ObjectCreationStrategy();
        private static readonly Func<Type[], IDefaultData, ObjectCreationStrategy, object> CreateFunc = (types, data, creatorMode) => typeof(ObjectCreatorExtensions).InvokeExpectedMethod(nameof(ObjectCreatorExtensions.Create), types, data, creatorMode);

        public static T Create<T>()
        {
            return Create<T>(null, DefaultCreationStrategy);
        }

        public static T Create<T>(IDefaultData defaultData)
        {
            return Create<T>(defaultData, DefaultCreationStrategy);
        }

        public static T Create<T>(ObjectCreationStrategy objectCreationStrategy)
        {
            return Create<T>(null, objectCreationStrategy);
        }

        public static T Create<T>(IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var type = typeof(T);
            var defaultValue = type.GetDefaultValue(defaultData);
            if (defaultValue != null)
            {
                return (T)defaultValue;
            }

            if (type.IsInterface)
            {
                return InterfaceCreator.Create<T>(type, defaultData, objectCreationStrategy);
            }

            if (type.IsAbstract)
            {
                return AbstractClassCreator.Create<T>(type, defaultData, objectCreationStrategy);
            }

            if (type.IsArray)
            {
                return ArrayCreator.Create<T>(type, defaultData, objectCreationStrategy);
            }

            if (type.IsAction())
            {
                return ActionCreator.Create<T>(type);
            }

            if (type.IsFunc())
            {
                return FuncCreator.Create<T>(type, defaultData, objectCreationStrategy);
            }

            if (type.IsTask())
            {
                return TaskCreator.Create<T>(type, defaultData, objectCreationStrategy);
            }

            return UnknownTypeCreator.CreateDynamicFrom<T>(type, defaultData, objectCreationStrategy);
        }

        public static object Create(this Type type)
        {
            return Create(type, DefaultCreationStrategy);
        }

        public static object Create(this Type type, IDefaultData defaultData)
        {
            return type.Create(defaultData, DefaultCreationStrategy);
        }

        public static object Create(this Type type, ObjectCreationStrategy objectCreationStrategy)
        {
            return type.Create(null, objectCreationStrategy);
        }

        public static object Create(this Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsStatic())
            {
                throw new Exception("Static class could not be constructed");
            }

            var result = type.GetDefaultValue(defaultData);
            if (result != null)
            {
                return result;
            }

            if (type.IsUndefined())
            {
                return null;
            }

            return CreateFunc(new[] { type }, defaultData, objectCreationStrategy);
        }

        internal static T CreateDynamicFrom<T>(this Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                var returnValue = EnumerableCreator.Create<T>(type, defaultData, objectCreationStrategy);
                if (returnValue != null)
                {
                    return returnValue;
                }
            }

            var args = new object[] { };
            if (type.GetConstructors().Any())
            {
                args = type.CreateCtorArguments(defaultData, objectCreationStrategy);
            }

            var result = (T)Activator.CreateInstance(type, args);
            result.Setup(defaultData, objectCreationStrategy);
            return result;
        }

        private static T Setup<T>(this T source, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            if (objectCreationStrategy.SetupProperties)
            {
                source.InitProperties(defaultData, objectCreationStrategy);
            }

            return source;
        }

        internal static object[] CreateCtorArguments(this Type type, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments(defaultData, objectCreationStrategy);
        }

        private static object[] CreateArguments(this MethodBase methodBase, IDefaultData defaultData, ObjectCreationStrategy objectCreationStrategy)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(item => item.ParameterType.Create(defaultData, objectCreationStrategy));
            return arguments.ToArray();
        }
    }
}