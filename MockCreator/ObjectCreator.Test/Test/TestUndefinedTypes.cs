using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.Core;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreatorTest.Interfaces;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestUndefinedTypes
    {
        [TestMethod]
        public void TestUndefinedGenericType()
        {
            Assert.IsNull(typeof(IEnumerable<>).Create());
        }

        [TestMethod]
        public void TestUndefinedGenericTypeWithDefaultValue()
        {
            var defaultData = new DefaultData(new TypeToValue(typeof(IEnumerable<>), new List<object>()));
            Assert.IsNotNull(typeof(IEnumerable<>).Create(defaultData));
        }

        [TestMethod]
        public void TestInterfaceWithUndefinedTypeMethods()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<InterfaceWithUndefinedMethods>());
        }

        [TestMethod]
        public void TestUndefinedMethodsOnInterface()
        {
            var result = ObjectCreatorExtensions.Create<InterfaceWithUndefinedMethods>();

            Assert.IsNull(result.CreateType<object>());
        }

        [TestMethod]
        public void TestGenericCallsWithArguments()
        {
            var mock = Substitute.For<InterfaceWithUndefinedMethods>();
            var resultGeneirc = mock.CreateType<string>();
        }
    }
}
