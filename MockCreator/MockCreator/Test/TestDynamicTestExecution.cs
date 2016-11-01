using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using TestExtension;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestDynamicTestExecution
    {
        private static readonly DefaultData CustomData = new DefaultData(
            (sbyte) 3,
            (byte) 3,
            (short) 3,
            (ushort) 3,
            3,
            (uint) 3,
            (long) 3.3,
            (ulong) 3.3,
            '?',
            (float) 3.3,
            3.4,
            false,
            new decimal(3.5),
            "What",
            new DateTime(),
            new object(),
            new[] {"T..X"},
            new Collection<int> {3, 3});

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
            var errors = Analyze(_dictionary);

            Assert.IsFalse(errors.Any(), ToErrorString(errors));
        }

        private static IEnumerable<string> Analyze(Dictionary<Type, object> dictionary)
        {
            foreach (var keyValuePair in dictionary)
            {
                var result = typeof(ObjectCreatorExtensions).InvokeExpectedMethod(nameof(ObjectCreatorExtensions.Create),
                    new[] {keyValuePair.Key}, CustomData);

                if (CustomData.GetDefaultValue(keyValuePair.Key).NotEqualityEquals(result))
                {
                    yield return $"Expected type:{keyValuePair.Key} has not expected {result}";
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