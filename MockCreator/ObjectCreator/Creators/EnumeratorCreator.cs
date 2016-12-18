﻿using Extensions;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace ObjectCreator.Creators
{
    internal static class EnumeratorCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultData, ObjectCreatorMode objectCreatorMode)
        {
            if (!type.IsInterfaceImplemented<IEnumerator>())
            {
                return default(T);
            }

            var expectedType = typeof(T);
            return expectedType.IsGenericType ?
                EnumeratorCreatorGeneric.Create<T>(expectedType) :
                EnumeratorCreatorNonGeneric.Create<T>(expectedType);
        }
    }

    internal static class EnumeratorCreatorNonGeneric
    {
        internal static T Create<T>(Type expectedType)
        {
            var result = (T)NonGenericTypeCreator.GetValueOrDefault(expectedType)?.Invoke();
            if (result == null)
            {
                Debug.WriteLine($"Expected Enumerator type: '{expectedType.Name}' is unknown to create.");
            }
            return result;
        }

        private static readonly Dictionary<Type, Func<object>> NonGenericTypeCreator = new Dictionary<Type, Func<object>>
            {
                {typeof(IEnumerator), () => ObjectCreatorExtensions.Create<IEnumerable>().GetEnumerator()},
                {typeof(IDictionaryEnumerator), () => ObjectCreatorExtensions.Create<IDictionary>().GetEnumerator()},
            };
    }

    internal static class EnumeratorCreatorGeneric
    {
        internal static T Create<T>(Type expectedType)
        {
            if (expectedType.IsInterface)
            {
                var genericType = expectedType.GetGenericTypeDefinition();
                var result = EnumeratorCreators.GetValueOrDefault(genericType)?.Invoke(expectedType);
                if (result == null)
                {
                    Debug.WriteLine($"Expected IEnumerator type: '{expectedType.Name}' is unknown to create.");
                }
                return (T)result;
            }
            else
            {
                var declaringType = expectedType.DeclaringType;
                var genericTypes = expectedType.GetGenericArguments();
                var genericDeclaringType = declaringType.MakeGenericType(genericTypes);
                var createdType = genericDeclaringType.Create();
                var enumerator = createdType.GetType().GetMethod("GetEnumerator").Invoke(createdType, new object[] { });
                return (T)enumerator;
            }
        }

        private static readonly Dictionary<Type, Func<Type, object>> EnumeratorCreators = new Dictionary<Type, Func<Type, object>>()
            {
                { typeof(IEnumerator<>), type =>
                {
                    var queue = typeof(IEnumerable<>).MakeGenericType(type.GenericTypeArguments).Create();
                    var getEnumerator = queue.GetType().GetMethod(nameof(IEnumerable<int>.GetEnumerator)).Invoke(queue, new object[] {});
                    return getEnumerator;
                } },
            };
    }
}
