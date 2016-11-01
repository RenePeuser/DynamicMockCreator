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
            Assert.IsNotNull(SubstituteExtensions.For<StructWithoutCtor>());
        }

        [TestMethod]
        public void TestStructWithoutCtor()
        {
            Assert.IsNotNull(SubstituteExtensions.For<StructWithoutCtor>());
        }
    }
}
