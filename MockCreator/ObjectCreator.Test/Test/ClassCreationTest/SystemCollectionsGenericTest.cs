using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemCollectionGenericClassTest
    {
        [TestMethod]
        public void TestComparer()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Comparer<string>>());
        }

        [TestMethod]
        public void TestDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>>());
        }

        [TestMethod]
        public void TestEqualityComparer()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<EqualityComparer<string>>());
        }

        [TestMethod]
        public void TestHashset()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<HashSet<string>>());
        }

        [TestMethod]
        public void TestDictionaryKeyCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>.KeyCollection>());
        }

        [TestMethod]
        public void TestSortedDictionaryKeyCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>.KeyCollection>());
        }

        [TestMethod]
        public void TestKeyedByTypeCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<KeyedByTypeCollection<string>>());
        }

        [TestMethod]
        public void TestLinkedList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<LinkedList<string>>());
        }

        [TestMethod]
        public void TestLinkedListNode()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<LinkedListNode<string>>());
        }

        [TestMethod]
        public void TestList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<List<string>>());
        }

        [TestMethod]
        public void TestQueue()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Queue<string>>());
        }

        [TestMethod]
        public void TestSortedDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedDictionary<string, int>>());
        }

        [TestMethod]
        public void TestSortedList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedList<string, int>>());
        }

        [TestMethod]
        public void TestSortedSet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedSet<string>>());
        }

        [TestMethod]
        public void TestStack()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Stack<string>>());
        }

        [TestMethod]
        public void TestSynchronizedCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SynchronizedCollection<string>>());
        }

        [TestMethod]
        public void TestSynchronizedKeyedCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SynchronizedKeyedCollection<string, int>>());
        }

        [TestMethod]
        public void TestSynchronizedReadOnlyCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SynchronizedReadOnlyCollection<string>>());
        }

        [TestMethod]
        public void TestDictionaryValueCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>.ValueCollection>());
        }

        [TestMethod]
        public void TestSortedDictionaryValueCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedDictionary<string, int>.ValueCollection>());
        }
    }

    [TestClass]
    public class SystemCollectionGenericStructsTest
    {
        [TestMethod]
        public void TestDictionaryEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>.Enumerator>());
        }

        [TestMethod]
        public void TestDictionaryKeyCollectionEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>.KeyCollection.Enumerator>());
        }

        [TestMethod]
        public void TestDictionaryValueCollectionEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>.ValueCollection.Enumerator>());
        }

        [TestMethod]
        public void TestHashSetEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<HashSet<string>.Enumerator>());
        }

        [TestMethod]
        public void TestLinkedListEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<LinkedList<string>.Enumerator>());
        }

        [TestMethod]
        public void TestListEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<List<string>.Enumerator>());
        }

        [TestMethod]
        public void TestQueueEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Queue<string>.Enumerator>());
        }

        [TestMethod]
        public void TestSortedDictionaryEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedDictionary<string, int>.Enumerator>());
        }

        [TestMethod]
        public void TestSortedDictionaryKeyCollectionEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedDictionary<string, int>.KeyCollection.Enumerator>());
        }

        [TestMethod]
        public void TestSortedDictionaryValueCollectionEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedDictionary<string, int>.ValueCollection.Enumerator>());
        }

        [TestMethod]
        public void TestSortedSet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedSet<string>.Enumerator>());
        }

        [TestMethod]
        public void TestStack()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Stack<string>.Enumerator>());
        }

        [TestMethod]
        public void TestKeyValuePair()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<KeyValuePair<string, int>>());
        }
    }

    [TestClass]
    public class SystemCollectionGenericInterfacesTest
    {
        [TestMethod]
        public void TestICollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ICollection<string>>());
        }

        [TestMethod]
        public void TestIComparer()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IComparer<string>>());
        }

        [TestMethod]
        public void TestIDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IDictionary<string, int>>());
        }

        [TestMethod]
        public void TestIEnumerable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEnumerable<string>>());
        }

        [TestMethod]
        public void TestIEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEnumerator<string>>());
        }

        [TestMethod]
        public void TestIEqualityComparer()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEqualityComparer<string>>());
        }

        [TestMethod]
        public void TestIList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IList<string>>());
        }

        [TestMethod]
        public void TestIReadOnlyCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IReadOnlyCollection<string>>());
        }

        [TestMethod]
        public void TestIReadOnlyDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IReadOnlyDictionary<string, int>>());
        }

        [TestMethod]
        public void TestIReadOnlyList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IReadOnlyList<string>>());
        }

        [TestMethod]
        public void TestISet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ISet<string>>());
        }
    }
}