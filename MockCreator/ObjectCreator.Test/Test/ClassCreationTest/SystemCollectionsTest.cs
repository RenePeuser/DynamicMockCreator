using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;

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

        [TestMethod]
        public void TestMyEnumerable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<MyEnumerable>());
        }

        private class MyEnumerable : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                return new List<object>().GetEnumerator();
            }
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

        [TestMethod]
        public void TestOwnIEnumerable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IMyEnumerable>());
        }

        // Test class to test own implementation of IEnumerable.
        public interface IMyEnumerable : IEnumerable
        {
        }
    }

    [TestClass]
    public class SystemCollectionCreateAnyItems
    {
        private static readonly ObjectCreationStrategy ObjectCreationStrategy = new ObjectCreationStrategy(false, false, false, 4);
        private static readonly UniqueDefaultData UniqueDefaultData = new UniqueDefaultData();

        private static readonly List<Type> EnumerationTypes = new List<Type>
        {
            typeof(ArrayList),
            typeof(Hashtable),
            typeof(Queue),
            typeof(SortedList),
            typeof(Stack),
            typeof(HybridDictionary),
            typeof(ListDictionary),
            typeof(NameValueCollection),
            typeof(OrderedDictionary),
            typeof(StringCollection),
            typeof(StringDictionary),
            typeof(UriSchemeKeyedCollection),
            typeof(IEnumerable),
            typeof(ICollection),
            typeof(IList),
            typeof(IDictionary),
            typeof(IOrderedDictionary)
        };


        [TestMethod]
        public void TestEnumerationWithAnyItems()
        {
            var analyzeResult = Analyze(EnumerationTypes);

            Assert.IsFalse(analyzeResult.Any(), ToErrorString(analyzeResult));
        }

        private static IEnumerable<string> Analyze(List<Type> typesToCreate)
        {
            foreach (var type in typesToCreate)
            {
                var result = type.Create(UniqueDefaultData, ObjectCreationStrategy).Cast<IEnumerable>().OfType<object>();

                if (result.Count() != ObjectCreationStrategy.EnumerationCount)
                {
                    yield return $"Expected type:{type} has not expected items count {ObjectCreationStrategy.EnumerationCount}";
                }
            }
        }

        private static string ToErrorString(IEnumerable<string> errors)
        {
            var stringBuilder = new StringBuilder();
            errors.ForEach(e => stringBuilder.AppendLine(e));
            return stringBuilder.ToString();
        }
    }
}