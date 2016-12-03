using System;
using System.Linq;
using System.Threading.Tasks;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class TcTaskTypeExtensions
    {
        public static T CreateFromTask<T>(this Type type, IDefaultData defaultValue)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var genericTypeParam = type.GetGenericArguments().FirstOrDefault();
            if (genericTypeParam != null)
            {
                var returnValue = genericTypeParam.Create(defaultValue);
                var result = typeof(TcTaskTypeExtensions).InvokeGenericMethod(nameof(FromResult), new[] { genericTypeParam }, returnValue);
                return (T)result;
            }

            return (T)(object)Task.FromResult(0);
        }

        private static Task<T> FromResult<T>(T value)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(value);
            return tcs.Task;
        }
    }
}
