﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class FuncTypeExtensions
    {
        private static readonly IEnumerable<Type> FuncDeclarations = new[]
        {
            typeof(Func<>),
            typeof(Func<,>),
            typeof(Func<,,>),
            typeof(Func<,,,>),
            typeof(Func<,,,,>),
            typeof(Func<,,,,,>),
            typeof(Func<,,,,,,>),
            typeof(Func<,,,,,,,>),
            typeof(Func<,,,,,,,,>),
            typeof(Func<,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,,>)
        };

        public static bool IsFunc(this Type type)
        {
            var genericTypeDefintion = type.GetGenericTypeDefinition();
            return FuncDeclarations.Contains(genericTypeDefintion);
        }

        public static T CreateFromFunc<T>(this Type type, IDefaultData defaultValue)
        {
            var parameterExpressions = CreateParameterExpressions(type);
            var returnBlockExpression = CreateReturnBlockExpression(type, defaultValue);
            var lambda = Expression.Lambda(returnBlockExpression, parameterExpressions);
            var compiledLambda = lambda.Compile();
            return (T)(object)compiledLambda;
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

        private static BlockExpression CreateReturnBlockExpression(Type funcType, IDefaultData defaultValue)
        {
            var outputArgument = funcType.GetGenericArguments().Last();
            var returnValue = Expression.Constant(outputArgument.Create(defaultValue));
            var labelTarget = Expression.Label(typeof(string));
            var expressionBlock = Expression.Block(Expression.Label(labelTarget, returnValue));
            return expressionBlock;
        }
    }
}