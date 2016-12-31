using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;

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
        public void TestHashSet()
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
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedDictionary<string, int>.KeyCollection>());
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
        public void TestUriSchemeKeyedCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<UriSchemeKeyedCollection>());
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
        public void TestSortedSetEnumerator()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<SortedSet<string>.Enumerator>());
        }

        [TestMethod]
        public void TestStackEnumerator()
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

    [TestClass]
    public class SystemCollectionGenericCreateAnyItems
    {
        private static readonly ObjectCreationStrategy ObjectCreationStrategy = new ObjectCreationStrategy(false, false, false, 4);
        private static readonly UniqueDefaultData UniqueDefaultData = new UniqueDefaultData();
        private static readonly List<Type> EnumerationTypes = new List<Type>
        {
           typeof(List<int>),
           typeof(Collection<int>),
           typeof(ObservableCollection<int>),
           typeof(ConcurrentBag<int>),
           typeof(BlockingCollection<int>),
           typeof(ReadOnlyCollection<int>),
           typeof(Queue<int>),
           //typeof(KeyedByTypeCollection<object>),
           typeof(HashSet<int>),
           typeof(Stack<int>),
           typeof(LinkedList<int>),
           typeof(SynchronizedCollection<int>),
           typeof(SynchronizedReadOnlyCollection<int>),
           typeof(SortedSet<int>),
           typeof(ConcurrentQueue<int>),
           typeof(ConcurrentStack<int>),
           typeof(ReadOnlyObservableCollection<int>),
           typeof(ImmutableList<int>),
           typeof(ImmutableArray<int>),
           typeof(ImmutableHashSet<int>),
           typeof(ImmutableSortedSet<int>),
           typeof(ImmutableQueue<int>),
           typeof(ImmutableStack<int>),

           typeof(ImmutableSortedDictionary<int,int>),
           typeof(ImmutableDictionary<int,int>),
           //typeof(KeyedCollection<int,int>),
           typeof(ReadOnlyDictionary<int,int>),
           typeof(SortedDictionary<int,int>),
           typeof(SortedList<int,int>),
           typeof(ConcurrentDictionary<int,int>),
           typeof(Dictionary<int,int>),
           typeof(SortedDictionary<int,int>.KeyCollection),
           typeof(SortedDictionary<int,int>.ValueCollection),
           typeof(Dictionary<int,int>.ValueCollection),
           typeof(Dictionary<int,int>.KeyCollection),
           typeof(ReadOnlyDictionary<int,int>.KeyCollection),
           typeof(ReadOnlyDictionary<int,int>.ValueCollection),
           typeof(IEnumerable<int>),
           typeof(ICollection<int>),
           typeof(IList<int>),
           typeof(IReadOnlyCollection<int>),
           typeof(IReadOnlyList<int>),
           typeof(ISet<int>),
           typeof(IProducerConsumerCollection<int>),
           typeof(IImmutableList<int>),
           typeof(IImmutableQueue<int>),
           typeof(IImmutableSet<int>),
           typeof(IImmutableStack<int>),
        };


        [TestMethod]
        public void TestEnumerationWithAnyItems()
        {
            var analyzeResult = AnalyzeEnumerationTypes(EnumerationTypes);

            Assert.IsFalse(analyzeResult.Any(), ToErrorString(analyzeResult));
        }

        private static IEnumerable<string> AnalyzeEnumerationTypes(List<Type> typesToCreate)
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

        private static IEnumerable<string> AnalyzeDictionary(List<Type> typesToCreate)
        {
            foreach (var type in typesToCreate)
            {
                var result = type.Create(UniqueDefaultData, ObjectCreationStrategy).Cast<IDictionary>();

                if (result.Keys.Count != ObjectCreationStrategy.EnumerationCount)
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