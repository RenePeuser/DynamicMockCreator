using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockCreator.Extensions;
using MockCreator.Helper;
using TestExtension;

namespace MockCreator.Test
{
    [TestClass]
    public class TestDynamicTestExecution
    {
        private static readonly DefaultData CustomData = new DefaultData(
            (sbyte)1,
            (byte)1,
            (short)1,
            (ushort)1,
            1,
            (uint)1,
            (long)1.1,
            (ulong)1.2,
            'c',
            (float)1.3,
            1.4,
            true,
            new decimal(1.5),
            "MyString",
            new DateTime(2016, 10, 15),
            new object(),
            new[] { "A..Z" },
            new Collection<int> { 1, 2 });

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
                var result = typeof(SubstituteExtensions).InvokeGenericMethod(nameof(SubstituteExtensions.For),
                    new[] { keyValuePair.Key }, CustomData);

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
