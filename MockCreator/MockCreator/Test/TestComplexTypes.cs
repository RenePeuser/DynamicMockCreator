using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockCreator.DataClasses;
using MockCreator.Extensions;
using Container = MockCreator.DataClasses.Container;

namespace MockCreator.Test
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