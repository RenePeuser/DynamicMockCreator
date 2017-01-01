using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreatorTest.DataClasses;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestImmutableFuncAndAction
    {
        [TestMethod]
        public void TestClassWithActionAndFunc()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableActionAndFunc>());
        }

        [TestMethod]
        public void TestActionWasSet()
        {
            var result = ObjectCreatorExtensions.Create<ImmutableActionAndFunc>();
            Assert.IsNotNull(result.Action);
        }

        [TestMethod]
        public void TestFuncWasSet()
        {
            var result = ObjectCreatorExtensions.Create<ImmutableActionAndFunc>();
            Assert.IsNotNull(result.Func);
        }

        [TestMethod]
        public void TestFuncHasResult()
        {
            var result = ObjectCreatorExtensions.Create<ImmutableActionAndFunc>();
            Assert.IsNotNull(result.Func.Invoke("a"));
        }
    }

    [TestClass]
    public class TestClassWithFuncAndAction
    {
        private static ObjectCreationStrategy objectCreationStrategy = new ObjectCreationStrategy(true, false, 0, null);

        [TestMethod]
        public void TestClassWithActionAndFunc()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ActionAndFunc>(objectCreationStrategy));
        }

        [TestMethod]
        public void TestActionWasSet()
        {
            var result = ObjectCreatorExtensions.Create<ActionAndFunc>(objectCreationStrategy);
            Assert.IsNotNull(result.Action);
        }

        [TestMethod]
        public void TestFuncWasSet()
        {
            var result = ObjectCreatorExtensions.Create<ActionAndFunc>(objectCreationStrategy);
            Assert.IsNotNull(result.Func);
        }

        [TestMethod]
        public void TestFuncHasResult()
        {
            var result = ObjectCreatorExtensions.Create<ActionAndFunc>(objectCreationStrategy);
            Assert.IsNotNull(result.Func.Invoke("a"));
        }
    }
}
