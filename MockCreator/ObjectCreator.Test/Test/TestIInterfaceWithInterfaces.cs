using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreatorTest.Extensions;
using ObjectCreatorTest.Interfaces;
namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestIInterfaceWithInterfaces
    {
        private static readonly DefaultData CustomData = new DefaultData(
            (sbyte)2,
            (byte)2,
            (short)2,
            (ushort)2,
            2,
            (uint)2,
            (long)2.2,
            (ulong)2.2,
            '?',
            (float)2.3,
            2.4,
            false,
            new decimal(2.5),
            "Next",
            new DateTime(),
            new object(),
            new[] { "B..C" },
            new Collection<int> { 2, 2 });

        private static Dictionary<Type, object> _dictionary;
        private static IInterfaceWithInterfaces _mock;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var privateObject = CustomData.ToPrivateObject();
            _dictionary = privateObject.GetField<Dictionary<Type, object>>("_defaultData");
            var objectCreationStrategy = new ObjectCreationStrategy(true, false, 0, null);
            _mock = ObjectCreatorExtensions.Create<IInterfaceWithInterfaces>(CustomData, objectCreationStrategy);
        }

        [TestMethod]
        public void TestIfInterfaceOnlyIsNotNull()
        {
            Assert.IsNotNull(_mock.Cloneable);
        }

        [TestMethod]
        public void TestItEnumerableIsNotNull()
        {
            Assert.IsNotNull(_mock.Strings);
        }

        [TestMethod]
        public void TestIfSpecialInterfaceIsNotNull()
        {
            Assert.IsNotNull(_mock.PrimitivePropertyInterface);
        }

        [TestMethod]
        public void TestDeepSetup()
        {
            var properties = typeof(IPrimitivePropertyInterface).GetProperties();
            var expectedMock = _mock.PrimitivePropertyInterface;
            var typeOfMock = expectedMock.GetType();

            foreach (var propertyInfo in properties)
            {
                var propertyInfoPropertyType = propertyInfo.PropertyType;
                var expectedValue = _dictionary[propertyInfoPropertyType];
                var currentValue = typeOfMock.GetProperty(propertyInfo.Name).GetValue(expectedMock);
                Assert.AreEqual(expectedValue, currentValue);
            }
        }
    }
}