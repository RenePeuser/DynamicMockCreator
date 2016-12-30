using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreatorTest.Interfaces;
using TestExtension;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestIPropertyInterfaceMock
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

        private Dictionary<Type, object> _dictionary;

        [TestInitialize]
        public void Init()
        {
            var privateObject = CustomData.ToPrivateObject();
            _dictionary = privateObject.GetField<Dictionary<Type, object>>("_defaultData");
        }

        [TestMethod]
        public void TestFor()
        {
            var objectCreationStrategy = new ObjectCreationStrategy(true, false, false, 0);
            var mock = ObjectCreatorExtensions.Create<IPrimitivePropertyInterface>(CustomData, objectCreationStrategy);
            var errors = Analyze(mock, _dictionary);

            Assert.IsFalse(errors.Any(), ToErrorString(errors));
        }

        private static IEnumerable<string> Analyze<T>(T mock, Dictionary<Type, object> dictionary)
        {
            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                var expectedPropertyValue = dictionary[propertyInfo.PropertyType];
                var propertyValue = mock.GetType().GetProperty(propertyInfo.Name).GetValue(mock);

                if (expectedPropertyValue.NotEqualityEquals(propertyValue))
                {
                    yield return
                        $"Expected property:{propertyInfo.Name} has not expected value {expectedPropertyValue} current value {propertyValue}"
                        ;
                }
            }
        }

        private static string ToErrorString(IEnumerable<string> errors)
        {
            var stringBuilder = new StringBuilder();
            errors.ForEach(e => stringBuilder.AppendLine(e));
            return stringBuilder.ToString();
        }
    }
}