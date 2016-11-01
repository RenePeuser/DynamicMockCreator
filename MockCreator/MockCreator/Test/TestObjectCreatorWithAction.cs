using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestObjectCreatorWithAction
    {
        private static readonly IReadOnlyList<Type> ActionDeclarations = new[]
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

        [TestMethod]
        public void TestAction()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Action>());
        }

        [TestMethod]
        public void TestActions()
        {
            for (int i = 1; i < ActionDeclarations.Count; i++)
            {
                var genericType = ActionDeclarations[i].MakeGenericType(CreateTypeArguments(i).ToArray());
                var func = genericType.Create();
                Assert.IsNotNull(func);
            }
        }

        private IEnumerable<Type> CreateTypeArguments(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return typeof(string);
            }
        }
    }
}
