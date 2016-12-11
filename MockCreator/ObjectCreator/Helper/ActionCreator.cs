﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ObjectCreator.Extensions;

namespace ObjectCreator.Helper
{
    internal static class ActionCreator
    {
        internal static T Create<T>(Type type)
        {
            if (!type.IsAction())
            {
                return default(T);
            }
            var parameterExpressions = CreateParameterExpressions(type);
            var returnBlockExpression = Expression.Block(Expression.Empty());
            var lambda = Expression.Lambda(returnBlockExpression, parameterExpressions);
            var compiledLambda = lambda.Compile();
            return (T)(object)compiledLambda;
        }

        private static IEnumerable<ParameterExpression> CreateParameterExpressions(Type funcType)
        {
            var genericArguments = funcType.GetGenericArguments();
            var inputParameterCount = genericArguments.Length;
            if (inputParameterCount <= 0)
            {
                yield break;
            }

            for (var i = 0; i < inputParameterCount; i++)
            {
                yield return Expression.Parameter(genericArguments[i]);
            }
        }
    }
}
