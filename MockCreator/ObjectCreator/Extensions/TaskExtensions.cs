using System;
using System.Linq;
using System.Threading.Tasks;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    internal static class TaskCreator
    {
        internal static object CreateFromTask(Type type, IDefaultData defaultValue)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsTask())
            {
                return null;
            }

            var genericTypeParam = type.GetGenericArguments().FirstOrDefault();
            if (genericTypeParam != null)
            {
                var returnValue = FuncCreator.Create(genericTypeParam, defaultValue);
                var result = typeof(TaskCreator).InvokeGenericMethod(nameof(FromResult), new[] { genericTypeParam }, returnValue);
                return result;
            }

            return Task.FromResult(0);
        }

        public static T CreateFromTask<T>(this Type type, IDefaultData defaultValue)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var genericTypeParam = type.GetGenericArguments().FirstOrDefault();
            if (genericTypeParam != null)
            {
                var returnValue = FuncCreator.Create(genericTypeParam, defaultValue);
                var result = typeof(TaskCreator).InvokeGenericMethod(nameof(FromResult), new[] { genericTypeParam }, returnValue);
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
