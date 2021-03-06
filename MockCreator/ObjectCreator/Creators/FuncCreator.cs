﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Creators
{
    internal static class FuncCreator
    {
        internal static T Create<T>(Type type, IDefaultData defaultValue, ObjectCreationStrategy objectCreationStrategy)
        {
            return (T)Create(type, defaultValue, objectCreationStrategy);
        }

        internal static object Create(Type type, IDefaultData defaultValue, ObjectCreationStrategy objectCreationStrategy)
        {
            if (!type.IsFunc())
            {
                return null;
            }

            var parameterExpressions = CreateParameterExpressions(type);
            var returnBlockExpression = CreateReturnBlockExpression(type, defaultValue, objectCreationStrategy);
            var lambda = Expression.Lambda(returnBlockExpression, parameterExpressions);
            var compiledLambda = lambda.Compile();
            return compiledLambda;
        }

        private static IEnumerable<ParameterExpression> CreateParameterExpressions(Type funcType)
        {
            var genericArguments = funcType.GetGenericArguments();
            var inputParameterCount = genericArguments.Length - 1;
            if (inputParameterCount <= 0)
            {
                yield break;
            }

            for (var i = 0; i < inputParameterCount; i++)
            {
                yield return Expression.Parameter(genericArguments[i]);
            }
        }

        private static BlockExpression CreateReturnBlockExpression(Type funcType, IDefaultData defaultValue, ObjectCreationStrategy objectCreationStrategy)
        {
            var outputArgument = funcType.GetGenericArguments().Last();
            var returnValue = Expression.Constant(outputArgument.Create(defaultValue, objectCreationStrategy));
            var labelTarget = Expression.Label(outputArgument);
            var expressionBlock = Expression.Block(Expression.Label(labelTarget, returnValue));
            return expressionBlock;
        }
    }
}
