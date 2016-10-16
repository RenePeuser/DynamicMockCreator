using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MockCreator
{
    [TestClass]
    public class TestEnumeration
    {
        [TestMethod]
        public void CreateIEnumerable()
        {
            Assert.IsNotNull(SubstituteExtensions.For<IEnumerable>());
        }

        [TestMethod]
        public void CreateGenericIEnumerable()
        {
            Assert.IsNotNull(SubstituteExtensions.For<IEnumerable<string>>());
        }

        [TestMethod]
        public void CreateGenericList()
        {
            Assert.IsNotNull(SubstituteExtensions.For<List<string>>());
        }

        [TestMethod]
        public void CreateGenericCollection()
        {
            Assert.IsNotNull(SubstituteExtensions.For<Collection<string>>());
        }

        [TestMethod]
        public void CreateGenericIList()
        {
            Assert.IsNotNull(SubstituteExtensions.For<IList<string>>());
        }

        [TestMethod]
        public void CreateArray()
        {
            Assert.IsNotNull(SubstituteExtensions.For<string[]>());
        }
    }
}