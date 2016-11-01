using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestObjectCreatorWithFunc
    {
        private static readonly IReadOnlyList<Type> FuncDeclarations = new[]
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

        [TestMethod]
        public void TestFunc()
        {
            for (int i = 1; i <= FuncDeclarations.Count; i++)
            {
                var genericType = FuncDeclarations[i - 1].MakeGenericType(CreateTypeArguments(i).ToArray());
                var func = genericType.Create();
                Assert.IsNotNull(func);
            }
        }

        private IEnumerable<Type> CreateTypeArguments(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return typeof(string);
            }
        }

    }
}
