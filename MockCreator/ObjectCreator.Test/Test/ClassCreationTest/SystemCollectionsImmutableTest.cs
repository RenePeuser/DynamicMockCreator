using System.Collections.Immutable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemCollectionImmutableGenericClassTest
    {
        [TestMethod]
        public void TestImmutableArray()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableArray<string>>());
        }

        [TestMethod]
        public void TestImmutableDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableDictionary<string, int>>());
        }

        [TestMethod]
        public void TestImmutableHashSet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableHashSet<string>>());
        }

        [TestMethod]
        public void TestImmutableList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableList<string>>());
        }

        [TestMethod]
        public void TestImmutableSortedDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableSortedDictionary<string, int>>());
        }

        [TestMethod]
        public void TestImmutableSortedSet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableSortedSet<string>>());
        }

        [TestMethod]
        public void TestImmutableQueue()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableQueue<string>>());
        }

        [TestMethod]
        public void TestImmutableStack()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableStack<string>>());
        }
    }

    [TestClass]
    public class SystemCollectionImmutableGenericStructsTest
    {
        [TestMethod]
        public void TestImmutableArray()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableArray<string>>());
        }

        [TestMethod]
        public void TestImmutableDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableDictionary<string, int>>());
        }

        [TestMethod]
        public void TestImmutableHashSet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableHashSet<string>>());
        }

        [TestMethod]
        public void TestImmutableListEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableList<string>.Enumerator>());
        }

        [TestMethod]
        public void TestImmutableSortedDictionaryEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableSortedDictionary<string, int>.Enumerator>());
        }

        [TestMethod]
        public void TestImmutableSortedSetEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableSortedSet<string>.Enumerator>());
        }

        [TestMethod]
        public void TestImmutableQueueEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableQueue<string>.Enumerator>());
        }

        [TestMethod]
        public void TestImmutableStackEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableStack<string>.Enumerator>());
        }
    }

    [TestClass]
    public class SystemCollectionImmutableGenericInterfacesTest
    {
        [TestMethod]
        public void TestIImmutableDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IImmutableDictionary<string, int>>());
        }

        [TestMethod]
        public void TestIImmutableList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IImmutableList<string>>());
        }

        [TestMethod]
        public void TestIImmutableSet()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IImmutableSet<string>>());
        }

        [TestMethod]
        public void TestImmutableQueue()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IImmutableQueue<string>>());
        }

        [TestMethod]
        public void TestImmutableStack()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IImmutableStack<string>>());
        }
    }
}