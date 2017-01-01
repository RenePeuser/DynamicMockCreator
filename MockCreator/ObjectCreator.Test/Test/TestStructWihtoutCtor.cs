using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreatorTest.Structs;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestStructWithoutCtor
    {
        [TestMethod]
        public void TestStruct()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StructWithoutCtor>());
        }

        [TestMethod]
        public void TestStructWithoutConstructor()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StructWithoutCtor>());
        }
    }
}
