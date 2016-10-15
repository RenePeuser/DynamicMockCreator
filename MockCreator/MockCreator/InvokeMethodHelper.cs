using System;
using System.Linq;

namespace MockCreator
{
    public static class InvokeMethodHelper
    {
        public static object InvokeGenericMethod(Type classType, string methodName, Type[] argumentTypes,
            object[] agruments)
        {
            var expectedMethod = classType.GetMethods().First(m => m.Name == methodName);
            var genericMethod = expectedMethod.MakeGenericMethod(argumentTypes);
            var result = genericMethod.Invoke(null, agruments);
            return result;
        }
    }
}