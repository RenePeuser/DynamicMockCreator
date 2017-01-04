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
            var defaultData = new DefaultData(new TypeToValue(typeof(IEnumerable<>), new List<object>()));
            Assert.IsNotNull(typeof(IEnumerable<>).Create(defaultData));
        }

        [TestMethod]
        public void TestInterfaceWithUndefinedTypeMethods()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IWithUndefinedMethods>());
        }

        [TestMethod]
        public void TestUndefinedMethodsOnInterface()
        {
            var result = ObjectCreatorExtensions.Create<IWithUndefinedMethods>();

            Assert.IsNull(result.CreateType<object>());
        }
    }

    [TestClass]
    public class TestUndefinedTypeInitialization
    {
        private static readonly ObjectCreationStrategy ObjectCreationStrategy = new ObjectCreationStrategy(true, true, 0, typeof(string));

        [TestMethod]
        public void TestUndefinedGenericType()
        {
            Assert.IsNotNull(typeof(IEnumerable<>).Create(ObjectCreationStrategy));
        }

        [TestMethod]
        public void TestExpectedGenericType()
        {
            Assert.IsTrue(typeof(IEnumerable<>).Create(ObjectCreationStrategy) is IEnumerable<string>);
        }

        [TestMethod]
        public void TestInterfaceWithUndefinedTypeMethods()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IWithUndefinedMethods>());
        }

        [TestMethod]
        public void TestSetupMethodOnUndefinedType()
        {
            var result = ObjectCreatorExtensions.Create<IWithUndefinedMethods>(ObjectCreationStrategy);
            Assert.IsFalse(string.IsNullOrEmpty(result.CreateType<string>()));
        }

        [TestMethod]
        public void TestSetupUndefinedGenericInterface()
        {
            var result = (IUndefinedGenericInterface<string>)typeof(IUndefinedGenericInterface<>).Create(ObjectCreationStrategy);
            Assert.IsFalse(string.IsNullOrEmpty(result.Property));
        }

    }
}
