using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.IsNotNull(typeof(IEnumerable<>).Create(new DefaultData(new TypeToValue(typeof(IEnumerable<>), new List<object>()))));
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
    }
}
