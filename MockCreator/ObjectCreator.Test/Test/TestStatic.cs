using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestStatic
    {
        [TestMethod]
        [Ignore]
        public void TestStaticClass()
        {
            Assert.IsNull(typeof(TypeExtensions).Create());
        }
    }
}
