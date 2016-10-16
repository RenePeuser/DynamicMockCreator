using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockCreator.DataClasses;
using MockCreator.Helper;
using NSubstitute;
using SubstituteExtensions = MockCreator.Extensions.SubstituteExtensions;

namespace MockCreator.Test
{
    [TestClass]
    public class TestReturnInitializationOfProperties
    {
        [TestMethod]
        public void TestString()
        {
            var defaultData = new DefaultData("YEAH !!!");
            var mock = SubstituteExtensions.For<ITestInterface>(defaultData);

            Assert.AreEqual(defaultData.GetDefaultValue(typeof(string)), mock.Name);
        }

        [TestMethod]
        public void TestBool()
        {
            var mock = SubstituteExtensions.For<ITestInterface>();

            Assert.IsTrue(mock.IsValid);
        }
    }

    [TestClass]
    public class TestReturnInitializationOfMethods
    {
        [TestMethod]
        public void TestString()
        {
            var defaultData = new DefaultData("YEAH !!!");
            var mock = SubstituteExtensions.For<ITestInterface>(defaultData);

            Assert.AreEqual(defaultData.GetDefaultValue(typeof(string)), mock.DoSomethingReturn());
            Assert.AreEqual(defaultData.GetDefaultValue(typeof(string)), mock.DoSomethingReturn(3));
            Assert.AreEqual(defaultData.GetDefaultValue(typeof(string)), mock.DoSomethingReturn(0, "b"));
            Assert.AreEqual(defaultData.GetDefaultValue(typeof(string)), mock.DoSomethingReturn(0, "a", Substitute.For<ICloneable>()));
        }

    }
}
