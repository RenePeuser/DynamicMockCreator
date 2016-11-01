using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockCreatorTest.DataClasses;
using ObjectCreator.Extensions;
using Container = MockCreatorTest.DataClasses.Container;

namespace MockCreatorTest.Test
{
    [TestClass]
    public class TestComplexTypes
    {
        [TestMethod]
        public void CreateFromInterfaceType()
        {
            Assert.IsNotNull(SubstituteExtensions.For<INotifyPropertyChanged>());
        }

        [TestMethod]
        public void CreateFromClassWithPrimitiveArguments()
        {
            Assert.IsNotNull(SubstituteExtensions.For<ClassWithPrimitiveTypes>());
        }

        [TestMethod]
        public void CreateFromClassWithInterfacesArguments()
        {
            Assert.IsNotNull(SubstituteExtensions.For<ClassWithInterfaces>());
        }

        [TestMethod]
        public void CreateFromAbstractClass()
        {
            Assert.IsNotNull(SubstituteExtensions.For<ClassAbstract>());
        }

        [TestMethod]
        public void CreateFromClassWithMultiLevelDepthArguments()
        {
            Assert.IsNotNull(SubstituteExtensions.For<Container>());
        }
    }
}