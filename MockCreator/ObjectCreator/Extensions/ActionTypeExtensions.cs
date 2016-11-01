using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ObjectCreator.Interfaces;

namespace ObjectCreator.Extensions
{
    public static class ActionTypeExtensions
    {
        private static readonly IEnumerable<Type> ActionDeclarations = new[]
        {
            typeof(Action),
            typeof(Action<>),
            typeof(Action<,>),
            typeof(Action<,,>),
            typeof(Action<,,,>),
            typeof(Action<,,,,>),
            typeof(Action<,,,,,>),
            typeof(Action<,,,,,,>),
            typeof(Action<,,,,,,,>),
            typeof(Action<,,,,,,,,>),
            typeof(Action<,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,,>)
        };

        public static bool IsAction(this Type type)
        {
            if (ActionDeclarations.Contains(type))
            {
                return true;
            }

            if (!type.GetGenericArguments().Any())
            {
                return false;
            }

            var genericTypeDefintion = type.GetGenericTypeDefinition();
            return ActionDeclarations.Contains(genericTypeDefintion);
        }

        public static T CreateFromAction<T>(this Type type, IDefaultData defaultValue)
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

        private static BlockExpression CreateReturnBlockExpression(Type funcType, IDefaultData defaultValue)
        {
            var expressionBlock = Expression.Block(Expression.Empty());
            return expressionBlock;
        }
    }
}
