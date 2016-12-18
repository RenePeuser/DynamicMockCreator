using System;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemCollectionsSpecializedClassTest
    {
        [TestMethod]
        public void TestHybridDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<HybridDictionary>());
        }

        [TestMethod]
        [Ignore]
        public void TestNameObjectCollectionBaseKeysCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<NameObjectCollectionBase.KeysCollection>());
        }

        [TestMethod]
        public void TestListDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ListDictionary>());
        }

        [TestMethod]
        public void TestNameObjectCollectionBase()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<NameObjectCollectionBase>());
        }

        [TestMethod]
        public void TestNameValueCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<NameValueCollection>());
        }

        [TestMethod]
        public void TestOrderedDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<OrderedDictionary>());
        }

        [TestMethod]
        public void TestStringCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StringCollection>());
        }

        [TestMethod]
        public void TestStringDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StringDictionary>());
        }

        [TestMethod]
        [Ignore]
        public void TestStringEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<StringEnumerator>());
        }
    }

    [TestClass]
    public class SystemCollectionsSpecializedStructTest
    {
        [TestMethod]
        public void TestBitVector32()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<BitVector32>());
        }

        [TestMethod]
        public void TestBitVector32Section()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<BitVector32.Section>());
        }
    }

    [TestClass]
    public class SystemCollectionsSpecializedInterfacesTest
    {
        [TestMethod]
        public void TestIOrderedDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IOrderedDictionary>());
        }
    }
}