using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemCollectionsClassTest
    {
        [TestMethod]
        public void TestArrayList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ArrayList>());
        }

        [TestMethod]
        public void TestBitArray()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<BitArray>());
        }

        [TestMethod]
        public void TestCollectionBase()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<CollectionBase>());
        }

        [TestMethod]
        public void TestDictionaryBase()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<DictionaryBase>());
        }

        [TestMethod]
        public void TestReadOnlyCollectionBase()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ReadOnlyCollectionBase>());
        }

        [TestMethod]
        public void TestHashtable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Hashtable>());
        }

        [TestMethod]
        public void TestQueue()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Queue>());
        }

        [TestMethod]
        public void TestSortedList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedList>());
        }

        [TestMethod]
        public void TestStack()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Stack>());
        }

        [TestMethod]
        public void TestDictionaryEntry()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<DictionaryEntry>());
        }
    }

    [TestClass]
    public class SystemCollectionsStructTest
    {
        [TestMethod]
        public void TestDictionaryEntry()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<DictionaryEntry>());
        }
    }

    [TestClass]
    public class SystemCollectionInterfacesTest
    {
        [TestMethod]
        public void TestICollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ICollection>());
        }

        [TestMethod]
        public void TestIComparer()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IComparer>());
        }

        [TestMethod]
        public void TestIDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IDictionary>());
        }

        [TestMethod]
        public void TestIDictionaryEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IDictionaryEnumerator>());
        }

        [TestMethod]
        public void TestIEnumerable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEnumerable>());
        }

        [TestMethod]
        public void TestIEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEnumerator>());
        }

        [TestMethod]
        public void TestIEqualityComparer()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEqualityComparer>());
        }

        [TestMethod]
        public void TestIList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IList>());
        }

        [TestMethod]
        public void TestIStructuralComparable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IStructuralComparable>());
        }

        [TestMethod]
        public void TestIStructuralEquatable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IStructuralEquatable>());
        }
    }

}