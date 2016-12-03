using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestImmutableTypes
    {
        [TestMethod]
        public void TestImmutableableArrayGeneric()
        {
            ImmutableArray<string> a = ImmutableArray<string>.Empty;

            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableArray<string>>());
        }

        [TestMethod]
        public void TestImmutableableListGeneric()
        {
            ImmutableList<string> a = ImmutableList<string>.Empty;

            Assert.IsNotNull(ObjectCreatorExtensions.Create<ImmutableList<string>>());
        }
    }
}
