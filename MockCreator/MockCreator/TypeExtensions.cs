using System;
using System.Linq;
using System.Reflection;

namespace MockCreator
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

        public static object InvokeGenericMethod(this Type classType, string methodName, Type[] argumentTypes,
            object[] agruments)
        {
            var expectedMethod = classType.GetMethods().First(m => m.Name == methodName);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, agruments);
            return result;
        }
    }
}
