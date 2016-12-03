using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestEnumeration
    {
        [TestMethod]
        public void CreateIEnumerable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEnumerable>());
        }

        [TestMethod]
        public void CreateGenericIEnumerable()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IEnumerable<string>>());
        }

        [TestMethod]
        public void CreateGenericList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<List<string>>());
        }

        [TestMethod]
        public void CreateGenericCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Collection<string>>());
        }

        [TestMethod]
        public void CreateGenericICollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ICollection<string>>());
        }

        [TestMethod]
        public void CreateGenericIList()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<IList<string>>());
        }

        [TestMethod]
        public void CreateArray()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<string[]>());
        }

        [TestMethod]
        public void CreateObservableCollection()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ObservableCollection<string>>());
        }

        [TestMethod]
        public void CreateDictionary()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Dictionary<string, int>>());
        }
    }
}