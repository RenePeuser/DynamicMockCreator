using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class SystemCollectionsObjectModelClassTest
    {
        [TestMethod]
        public void TestCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Collection<string>>());
        }

        [TestMethod]
        public void TestReadOnlyDictionaryKeyCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ReadOnlyDictionary<string, int>.KeyCollection>());
        }

        [TestMethod]
        public void TestKeyedCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<KeyedCollection<string, int>>());
        }

        [TestMethod]
        public void TestObservableCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ObservableCollection<string>>());
        }

        [TestMethod]
        public void TestReadOnlyCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ReadOnlyCollection<string>>());
        }

        [TestMethod]
        public void TestReadOnlyDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ReadOnlyDictionary<string, int>>());
        }

        [TestMethod]
        public void TestReadOnlyObservableCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ReadOnlyObservableCollection<string>>());
        }

        [TestMethod]
        public void TestReadOnlyDictionaryValueCollction()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ReadOnlyDictionary<string, int>.ValueCollection>());
        }
    }
}
