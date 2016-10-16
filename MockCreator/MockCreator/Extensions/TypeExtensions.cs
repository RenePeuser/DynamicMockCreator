using System;
using System.Linq;
using System.Reflection;

namespace MockCreator.Extensions
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
            params object[] agruments)
        {
            return (T) InvokeGenericMethod(classType, methodName, argumentTypes, agruments);
        }

        public static object InvokeGenericMethod(this Type classType, string methodName, Type[] argumentTypes,
            params object[] agruments)
        {
            var expectedMethod = classType.GetMethods().First(m => m.Name == methodName);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, agruments);
            return result;
        }
    }
}