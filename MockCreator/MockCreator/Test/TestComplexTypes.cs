using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreatorTest.DataClasses;
using Container = ObjectCreatorTest.DataClasses.Container;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestComplexTypes
    {
        [TestMethod]
        public void CreateFromInterfaceType()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<INotifyPropertyChanged>());
        }

        [TestMethod]
        public void CreateFromClassWithPrimitiveArguments()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithPrimitiveTypes>());
        }

        [TestMethod]
        public void CreateFromClassWithInterfacesArguments()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithInterfaces>());
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