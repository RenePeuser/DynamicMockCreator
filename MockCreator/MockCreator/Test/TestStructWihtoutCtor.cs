using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockCreatorTest.Structs;
using ObjectCreator.Extensions;

namespace MockCreatorTest.Test
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
