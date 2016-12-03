using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestTask
    {
        [TestMethod]
        public void TestTaskCreation()
        {
            var result = ObjectCreatorExtensions.Create<Task>();
        }
    }
}
