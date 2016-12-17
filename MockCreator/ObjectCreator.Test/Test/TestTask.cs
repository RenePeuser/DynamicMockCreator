using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestTask
    {
        [TestMethod]
        public void TestTaskCreation()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Task>());
        }
    }
}
