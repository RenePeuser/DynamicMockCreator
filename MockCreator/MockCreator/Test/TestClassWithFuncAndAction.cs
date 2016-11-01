using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreatorTest.DataClasses;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestClassWithFuncAndAction
    {
        [TestMethod]
        public void TestClassWithActionAndFunc()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithActionAndFunc>());
        }

        [TestMethod]
        public void TestActionWasSet()
        {
            var result = ObjectCreatorExtensions.Create<ClassWithActionAndFunc>();
            Assert.IsNotNull(result.Action);
        }

        [TestMethod]
        public void TestFuncWasSet()
        {
            var result = ObjectCreatorExtensions.Create<ClassWithActionAndFunc>();
            Assert.IsNotNull(result.Func);
        }

        [TestMethod]
        public void TestFuncHasResult()
        {
            var result = ObjectCreatorExtensions.Create<ClassWithActionAndFunc>();
            Assert.IsNotNull(result.Func.Invoke("a"));
        }
    }
}
