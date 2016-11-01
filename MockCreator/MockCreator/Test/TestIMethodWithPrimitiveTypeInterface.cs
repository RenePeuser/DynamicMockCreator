using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockCreatorTest.Interfaces;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using TestExtension;

namespace MockCreatorTest.Test
{
    [TestClass]
    public class TestIMethodWithPrimitiveTypeInterface
    {
        private static readonly DefaultData CustomData = new DefaultData(
            (sbyte) 2,
            (byte) 2,
            (short) 2,
            (ushort) 2,
            2,
            (uint) 2,
            (long) 2.2,
            (ulong) 2.2,
            '?',
            (float) 2.3,
            2.4,
            false,
            new decimal(2.5),
            "Next",
            new DateTime(),
            new object(),
            new[] {"B..C"},
            new Collection<int> {2, 2});

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
            var mock = SubstituteExtensions.For<IMethodWithPrimitiveTypesInterface>(CustomData);
            var errors = Analyze(mock, _dictionary);

            Assert.IsFalse(errors.Any(), ToErrorString(errors));
        }

        private static IEnumerable<string> Analyze<T>(T mock, Dictionary<Type, object> dictionary)
        {
            var methods = typeof(T).GetMethods().Where(m => (m.ReturnType != typeof(void)) && !m.IsSpecialName);

            foreach (var methodInfo in methods)
            {
                var expectedValue = dictionary[methodInfo.ReturnType];
                var currentValue = mock.GetType().GetMethod(methodInfo.Name).Invoke(mock, new object[] {});

                if (expectedValue.NotEqualityEquals(currentValue))
                {
                    yield return
                        $"Expected method:{methodInfo.Name} has not expected return value {expectedValue} current return value {currentValue}"
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