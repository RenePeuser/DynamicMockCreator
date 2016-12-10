using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extensions;
using NSubstitute;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class ObjectCreatorExtensions
    {
        private static readonly Func<Type[], IDefaultData, ObjectCreatorMode, object> CreateFunc = (types, data, creatorMode) => typeof(ObjectCreatorExtensions).InvokeExpectedMethod(nameof(ObjectCreatorExtensions.Create), types, data, creatorMode);
        private static readonly Func<Type, IEnumerable, object> ToListOfTypeFunc = (genericTypeArg, enumeration) => typeof(EnumerableExtensions).InvokeGenericMethod(nameof(EnumerableExtensions.ToListOfType), new[] { genericTypeArg }, enumeration);
        private static readonly Func<Type, object[], object> ForPartsOfFunc = (genericType, arguments) => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.ForPartsOf), new[] { genericType }, new object[] { arguments });
        private static readonly Func<Type, object> ForFunc = genericType => typeof(Substitute).InvokeGenericMethod(nameof(Substitute.For), new[] { genericType }, new object[] { new object[] { } });
        private static readonly Func<Type, Type, IDefaultData, ObjectCreatorMode, object> CreateDictionaryFunc = (keyType, valueType, defaultData, creatorMode) => typeof(ObjectCreatorExtensions).InvokeExpectedMethod(nameof(ObjectCreatorExtensions.CreateFromDictionary), new[] { keyType, valueType }, defaultData, creatorMode);

        public static T Create<T>(ObjectCreatorMode objectCreatorMode = ObjectCreatorMode.None)
        {
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
                return CreateFromInterface<T>(type, defaultData, objectCreatorMode);
            }

            if (type.IsAbstract)
            {
                return CreateFromAbstractClass<T>(type, defaultData, objectCreatorMode);
            }

            if (type.IsArray)
            {
                return CreateFromArray<T>(type, defaultData, objectCreatorMode);
            }

            if (type.IsAction())
            {
                return type.CreateFromAction<T>();
            }

            if (type.IsFunc())
            {
                return type.CreateFromFunc<T>(defaultData);
            }

            if (type.IsTask())
            {
                return type.CreateFromTask<T>(defaultData);
            }

            return CreateDynamicFrom<T>(type, defaultData, objectCreatorMode);
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

        private static Dictionary<TKey, TValue> CreateFromDictionary<TKey, TValue>(IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            for (int i = 0; i < 5; i++)
            {
                var key = Create<TKey>(defaultData, objectCreatorMode);
                if (dictionary.ContainsKey(key))
                {
                    break;
                }

                var value = Create<TValue>(defaultData, objectCreatorMode);
                dictionary.Add(key, value);
            }
            return dictionary;
        }

        private static T CreateFromArray<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var elementType = type.GetElementType();
            var array = Array.CreateInstance(elementType, 5);
            for (var i = 0; i < 5; i++)
            {
                var arrayItem = elementType.Create(defaultData, objectCreatorMode);
                array.SetValue(arrayItem, i);
            }
            return (T)(object)array;
        }

        private static T CreateFromAbstractClass<T>(this Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            var args = type.CreateCtorArguments(defaultData);
            var result = (T)ForPartsOfFunc(type, args);

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                case ObjectCreatorMode.WithProperties:
                    result.InitProperties(defaultData);
                    break;
            }

            return result;
        }

        private static T CreateDynamicFrom<T>(this Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (type.IsInterfaceImplemented<IEnumerable>())
            {
                return CreateEnumeration<T>(type, defaultData, objectCreatorMode);
            }

            var args = new object[] { };
            if (type.GetConstructors().Any())
            {
                args = type.CreateCtorArguments(defaultData);
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

        private static object[] CreateCtorArguments(this Type type, IDefaultData defaultData)
        {
            var ctor = type.GetConstructor();
            return ctor.CreateArguments(defaultData);
        }

        private static object[] CreateArguments(this MethodBase methodBase, IDefaultData defaultData)
        {
            var parameterInfos = methodBase.GetParameters();
            var arguments = parameterInfos.Select(item => item.ParameterType.Create(defaultData));
            return arguments.ToArray();
        }

        private static T CreateFromInterface<T>(this Type argumentType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            // This is special we dont need a proxy for interfaces of IEnumerable.
            if (typeof(T).IsInterfaceImplemented<IEnumerable>())
            {
                return CreateEnumeration<T>(argumentType, defaultData, objectCreatorMode);
            }

            var mock = (T)ForFunc(argumentType);

            // Check solution for that case and all scenarios.
            if (typeof(T).IsSystemType())
            {
                return mock;
            }

            switch (objectCreatorMode)
            {
                case ObjectCreatorMode.All:
                    mock.SetupProperties(defaultData, objectCreatorMode);
                    mock.SetupMethods(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithProperties:
                    mock.SetupProperties(defaultData, objectCreatorMode);
                    break;
                case ObjectCreatorMode.WithMethods:
                    mock.SetupMethods(defaultData, objectCreatorMode);
                    break;
            }

            return mock;
        }

        private static T CreateEnumeration<T>(Type enumerationType, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            return EnumerableCreator.Create<T>();

            //var genericArguments = enumerationType.GetGenericArguments();
            //if (!genericArguments.Any())
            //{
            //    return (T)CreateEnumeration(typeof(object), defaultData, objectCreatorMode);
            //}

            //if (typeof(T).IsInterfaceImplemented<IDictionary>())
            //{
            //    return (T)CreateDictionaryFunc(genericArguments[0], genericArguments[1], defaultData, objectCreatorMode);
            //}

            //var enumeration = CreateEnumeration(enumerationType, defaultData, objectCreatorMode);
            //var result = ToListOfTypeFunc(genericArguments[0], enumeration);

            //if (enumerationType.IsInterfaceImplemented<ICollection>())
            //{
            //    return (T)Activator.CreateInstance(enumerationType, result);
            //}

            //return (T)result;
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