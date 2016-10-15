using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MockCreator
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

    [TestClass]
    public class TestPrimitveTypes
    {
        [TestMethod]
        public void TestBool()
        {
            Assert.AreNotEqual(default(bool), SubstituteExtensions.For<bool>());
        }

        [TestMethod]
        public void TestDateTime()
        {
            Assert.AreNotEqual(default(DateTime), SubstituteExtensions.For<DateTime>());
        }

        [TestMethod]
        public void TestChar()
        {
            Assert.AreNotEqual(default(char), SubstituteExtensions.For<char>());
        }

        [TestMethod]
        public void TestString()
        {
            Assert.AreNotEqual(default(string), SubstituteExtensions.For<string>());
        }

        [TestMethod]
        public void TestDecimal()
        {
            Assert.AreNotEqual(default(decimal), SubstituteExtensions.For<decimal>());
        }

        [TestMethod]
        public void TestByte()
        {
            Assert.AreNotEqual(default(byte), SubstituteExtensions.For<byte>());
        }

        [TestMethod]
        public void TestShort()
        {
            Assert.AreNotEqual(default(short), SubstituteExtensions.For<short>());
        }

        [TestMethod]
        public void TestInt()
        {
            Assert.AreNotEqual(default(int), SubstituteExtensions.For<int>());
        }

        [TestMethod]
        public void TestLong()
        {
            Assert.AreNotEqual(default(long), SubstituteExtensions.For<long>());
        }

        [TestMethod]
        public void TestFloat()
        {
            Assert.AreNotEqual(default(float), SubstituteExtensions.For<float>());
        }

        [TestMethod]
        public void TestDouble()
        {
            Assert.AreNotEqual(default(double), SubstituteExtensions.For<double>());
        }

        [TestMethod]
        public void TestSByte()
        {
            Assert.AreNotEqual(default(sbyte), SubstituteExtensions.For<sbyte>());
        }

        [TestMethod]
        public void TestUShort()
        {
            Assert.AreNotEqual(default(ushort), SubstituteExtensions.For<ushort>());
        }

        [TestMethod]
        public void TestUInt()
        {
            Assert.AreNotEqual(default(uint), SubstituteExtensions.For<uint>());
        }

        [TestMethod]
        public void TestULong()
        {
            Assert.AreNotEqual(default(ulong), SubstituteExtensions.For<ulong>());
        }
    }
}

