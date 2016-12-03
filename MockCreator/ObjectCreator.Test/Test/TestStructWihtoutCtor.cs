using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreatorTest.Structs;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestStructWihtoutCtor
    {
        [TestMethod]
        public void TestStruct()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StructWithoutCtor>());
        }

        [TestMethod]
        public void TestStructWithoutCtor()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StructWithoutCtor>());
        }
    }
}
