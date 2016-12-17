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

        [TestMethod]
        public void TestGenericCallsWithArguments()
        {
            var mock = Substitute.For<InterfaceWithUndefinedMethods>();
            //mock.CreateTypeWithArgs(6);
            var resultGeneirc = mock.CreateType<string>();
        }

        private void Setup<T>(InterfaceWithUndefinedMethods mock)
        {
            //mock.CreateTypeWithArgs(Arg.Any<T>()).Returns(callInfo =>
            //{
            //    var result = callInfo;
            //    return (T)(object)5;
            //});

            //mock.CreateTypeWithArgs<T>(Arg.Any<T>()).Returns(callInfo =>
            //{
            //    var result = callInfo;
            //    var returnValue = typeof(T).Create();
            //    return (T)returnValue;
            //});

            mock.CreateType<dynamic>().Returns();

            //var resultA = mock.CreateTypeWithArgs("a");

            //var item = ObjectCreatorExtensions.Create<T>();

            //var generiResult = mock.CreateTypeWithArgs(item);
            //var special = mock.CreateType<int>();

            //var methodInfoObj = mock.GetType().GetMethod("CreateType");
            //var genericMethod = methodInfoObj.MakeGenericMethod(typeof(string));
            //genericMethod.Invoke(mock, new object[] { });
        }
    }
}
