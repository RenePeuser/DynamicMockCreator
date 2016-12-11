using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace ObjectCreator.Extensions
{
    public static class ObjectCreatorExtensions
    {
        private static readonly Func<Type[], IDefaultData, ObjectCreatorMode, object> CreateFunc = (types, data, creatorMode) => typeof(ObjectCreatorExtensions).InvokeExpectedMethod(nameof(ObjectCreatorExtensions.Create), types, data, creatorMode);

        public static T Create<T>(ObjectCreatorMode objectCreatorMode = ObjectCreatorMode.None)
        {
            // For performance comparison !! ;-) Autofixture is much slower than this creator.
            //var fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());
            //var result = fixture.Create<T>();
            //return result;
            return Create<T>(null, objectCreatorMode);
        }

        public static T Create<T>(IDefaultData defaultData, ObjectCreatorMode objectCreatorMode = ObjectCreatorMode.None)
        {
            var type = typeof(T);
            var defaultValue = type.GetDefaultValue(defaultData);
            if (defaultValue != null)
            {
                return (T)defaultValue;
            }

            if (type.IsInterface)
            {
                return InterfaceCreator.Create<T>(type, defaultData, objectCreatorMode);
            }

            if (type.IsAbstract)
            {
                return AbstractClassCreator.Create<T>(type, defaultData, objectCreatorMode);
            }

            if (type.IsArray)
            {
                return ArrayCreator.Create<T>(type, defaultData, objectCreatorMode);
            }

            if (type.IsAction())
            {
                return ActionCreator.Create<T>(type);
            }

            if (type.IsFunc())
            {
                return FuncCreator.Create<T>(type, defaultData);
            }

            if (type.IsTask())
            {
                return TaskCreator.Create<T>(type, defaultData);
            }

            return UnknownTypeCreator.CreateDynamicFrom<T>(type, defaultData, objectCreatorMode);
        }

        public static object Create(this Type type, ObjectCreatorMode objectCreatorMode = ObjectCreatorMode.None)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.Create(null, objectCreatorMode);
        }

        public static object Create(this Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode = ObjectCreatorMode.None)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
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

            return CreateFunc(new[] { type }, defaultData, objectCreatorMode);
        }

        internal static T CreateDynamicFrom<T>(this Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                var returnValue = EnumerableCreator.Create<T>(type, defaultData, objectCreatorMode);
                if (returnValue != null)
                {
                    return returnValue;
                }
            }

            var args = new object[] { };
            if (type.GetConstructors().Any())
            {
                args = type.CreateCtorArguments(defaultData, objectCreatorMode);
            }

            var result = (T)Activator.CreateInstance(type, args);

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                case ObjectCreatorMode.WithProperties:
                    result.InitProperties(defaultData);
                    break;
            }

            return result;
        }

        internal static object[] CreateCtorArguments(this Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments(defaultData, objectCreatorMode);
        }

        private static object[] CreateArguments(this MethodBase methodBase, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(item => item.ParameterType.Create(defaultData, objectCreatorMode));
            return arguments.ToArray();
        }

        private static IEnumerable CreateEnumeration(Type enumerationType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var genericArgument = enumerationType.GetGenericArguments().First();
            for (var i = 0; i < 5; i++)
            {
                var result = genericArgument.Create(defaultData, objectCreatorMode);
                yield return result;
            }
        }
    }
}