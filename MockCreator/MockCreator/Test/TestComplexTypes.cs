using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;
using ObjectCreatorTest.DataClasses;
using Container = ObjectCreatorTest.DataClasses.Container;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestComplexTypes
    {
        private static readonly RandomDefaultData RandomDefaultData = new RandomDefaultData();

        [TestMethod]
        public void CreateFromInterfaceType()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<INotifyPropertyChanged>(RandomDefaultData));
        }

        [TestMethod]
        public void CreateFromClassWithPrimitiveArguments()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithPrimitiveTypes>(RandomDefaultData));
        }

        [TestMethod]
        public void CreateFromClassWithInterfacesArguments()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithInterfaces>(RandomDefaultData));
        }

        [TestMethod]
        public void CreateFromAbstractClass()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassAbstract>());
        }

        [TestMethod]
        public void CreateFromClassWithMultiLevelDepthArguments()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Container>());
        }
    }
}