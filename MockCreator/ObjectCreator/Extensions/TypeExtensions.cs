using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ObjectCreator.Extensions
{
    public static class TypeExtensions
    {
        private static readonly BindingFlags ExpectedBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                   BindingFlags.Static;

        public static ConstructorInfo GetConstructor(this Type type)
        {
            var ctor = type.GetConstructors(ExpectedBindingFlags).FirstOrDefault();
            return ctor;
        }

        public static T InvokeGenericMethod<T>(this Type classType, string methodName, Type[] argumentTypes,
            params object[] arguments)
        {
            return (T)InvokeGenericMethod(classType, methodName, argumentTypes, arguments);
        }

        public static object InvokeGenericMethod(this Type classType, string methodName, Type[] argumentTypes,
            params object[] arguments)
        {
            var expectedMethod = classType.GetMethods(ExpectedBindingFlags).First(m => m.Name == methodName);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, arguments);
            return result;
        }

        public static object InvokeExpectedMethod(this Type classType, string methodName, Type[] argumentTypes,
            params object[] arguments)
        {
            var argumentCount = arguments.Length;
            var expectedMethod = classType.GetMethods(ExpectedBindingFlags).First(item => item.Name == methodName && item.GetParameters().Length == argumentCount);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, arguments);
            return result;
        }

        public static bool IsSystemType(this Type type)
        {
            return type.FullName.StartsWith("System");
        }

        public static bool IsUndefined(this Type type)
        {
            if (type.FullName == null)
            {
                return true;
            }

            var genericArguments = type.GetGenericArguments();
            return genericArguments.Any(arg => arg.IsUndefined());
        }

        public static bool IsTask(this Type type)
        {
            if (type == typeof(Task))
            {
                return true;
            }

            return type == typeof(Task<>);
        }

        public static bool IsStatic(this Type type)
        {
            return type.IsClass && type.IsAbstract && type.IsSealed;
        }

        public static bool IsIEnumerable(this Type type)
        {
            return type.IsInterfaceImplemented<IEnumerable>() || type == typeof(IEnumerable);
        }

        public static bool IsIEnumerator(this Type type)
        {
            return type.IsInterfaceImplemented<IEnumerator>() || type == typeof(IEnumerator);
        }

        public static bool IsInterfaceImplemented<T>(this Type type) where T : class
        {
            var result = type.GetInterface<T>();
            return result != null;
        }

        public static Type GetInterface<T>(this Type type) where T : class
        {
            var genericType = typeof(T);
            if (!genericType.IsInterface)
            {
                throw new ArgumentException($"The generic type '{typeof(T).Name}' is not an interface");
            }

            var result = type.GetInterface(genericType.Name);
            return result;
        }

        public static Array ToArray(this Type type, int length)
        {
            return Array.CreateInstance(type, length);
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
            var properties = type.GetProperties();
            var inheritInterfaces = type.GetInterfaces();
            var inheritProperties = inheritInterfaces.SelectMany(item => item.GetProperties());
            var allProperties = properties.Concat(inheritProperties);
            return allProperties;
        }

        public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
        {
            var methods = type.GetMethods().Where(m => (m.ReturnType != typeof(void)) && !m.IsSpecialName);
            // var inheritInterfaces = type.GetInterfaces().Where(i => !i.FullName.StartsWith("System"));
            var inheritInterfaces = type.GetInterfaces();
            var inhertiMethods = inheritInterfaces.SelectMany(item => item.GetMethods().Where(m => (m.ReturnType != typeof(void)) && !m.IsSpecialName));
            var allMethods = methods.Concat(inhertiMethods);
            return allMethods;
        }
    }
}