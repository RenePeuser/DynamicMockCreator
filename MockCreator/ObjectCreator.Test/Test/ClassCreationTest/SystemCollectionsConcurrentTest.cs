using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemCollectionsConcurrentClassTest
    {
        [TestMethod]
        public void TestBlockingCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<BlockingCollection<string>>());
        }

        [TestMethod]
        public void TestConcurrentBag()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ConcurrentBag<string>>());
        }

        [TestMethod]
        public void TestConcurrentDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ConcurrentDictionary<string, int>>());
        }

        [TestMethod]
        public void TestConcurrentQueue()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ConcurrentQueue<string>>());
        }

        [TestMethod]
        public void TestConcurrentStack()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ConcurrentStack<string>>());
        }

        [TestMethod]
        public void TestOrderablePartitioner()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<OrderablePartitioner<string>>());
        }

        [TestMethod]
        public void TestPartitioner()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Partitioner<string>>());
        }
    }

    [TestClass]
    public class SystemCollectionsConcurrentInterfacesTest
    {
        [TestMethod]
        public void TestIProducerConsumerCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IProducerConsumerCollection<string>>());
        }
    }
}
