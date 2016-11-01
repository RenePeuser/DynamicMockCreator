using System;
using System.Linq;
using System.Reflection;

namespace ObjectCreator.Extensions
{
    public static class TypeExtensions
    {
        public static ConstructorInfo GetConstructor(this Type type)
        {
            var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Static |
                                            BindingFlags.NonPublic | BindingFlags.Instance)

                .First(item => !item.GetParameters().Any(p => p.ParameterType.IsPointer));
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
            var expectedMethod = classType.GetMethods().First(m => m.Name == methodName);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, arguments);
            return result;
        }

        public static object InvokeExpectedMethod(this Type classType, string methodName, Type[] argumentTypes,
            params object[] arguments)
        {
            var argumentCount = arguments.Length;
            var expectedMethod = classType.GetMethods().First(item => item.Name == methodName && item.GetParameters().Length == argumentCount);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, arguments);
            return result;
        }

        public static bool IsSystemType(this Type type)
        {
            return type.FullName.StartsWith("System");
        }

        public static bool IsNotSystemType(this Type type)
        {
            return !type.IsSystemType();
        }

        public static bool IsNotArray(this Type type)
        {
            return !type.IsArray;
        }
    }
}