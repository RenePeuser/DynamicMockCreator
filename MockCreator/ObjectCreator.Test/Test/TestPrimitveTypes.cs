using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class TestPrimitiveTypes
    {
        [TestMethod]
        public void TestBool()
        {
            Assert.AreNotEqual(default(bool), ObjectCreatorExtensions.Create<bool>());
        }

        [TestMethod]
        public void TestDateTime()
        {
            Assert.AreNotEqual(default(DateTime), ObjectCreatorExtensions.Create<DateTime>());
        }

        [TestMethod]
        public void TestChar()
        {
            Assert.AreNotEqual(default(char), ObjectCreatorExtensions.Create<char>());
        }

        [TestMethod]
        public void TestString()
        {
            Assert.AreNotEqual(default(string), ObjectCreatorExtensions.Create<string>());
        }

        [TestMethod]
        public void TestDecimal()
        {
            Assert.AreNotEqual(default(decimal), ObjectCreatorExtensions.Create<decimal>());
        }

        [TestMethod]
        public void TestByte()
        {
            Assert.AreNotEqual(default(byte), ObjectCreatorExtensions.Create<byte>());
        }

        [TestMethod]
        public void TestShort()
        {
            Assert.AreNotEqual(default(short), ObjectCreatorExtensions.Create<short>());
        }

        [TestMethod]
        public void TestInt()
        {
            Assert.AreNotEqual(default(int), ObjectCreatorExtensions.Create<int>());
        }

        [TestMethod]
        public void TestLong()
        {
            Assert.AreNotEqual(default(long), ObjectCreatorExtensions.Create<long>());
        }

        [TestMethod]
        public void TestFloat()
        {
            Assert.AreNotEqual(default(float), ObjectCreatorExtensions.Create<float>());
        }

        [TestMethod]
        public void TestDouble()
        {
            Assert.AreNotEqual(default(double), ObjectCreatorExtensions.Create<double>());
        }

        [TestMethod]
        public void TestSByte()
        {
            Assert.AreNotEqual(default(sbyte), ObjectCreatorExtensions.Create<sbyte>());
        }

        [TestMethod]
        public void TestUShort()
        {
            Assert.AreNotEqual(default(ushort), ObjectCreatorExtensions.Create<ushort>());
        }

        [TestMethod]
        public void TestUInt()
        {
            Assert.AreNotEqual(default(uint), ObjectCreatorExtensions.Create<uint>());
        }

        [TestMethod]
        public void TestULong()
        {
            Assert.AreNotEqual(default(ulong), ObjectCreatorExtensions.Create<ulong>());
        }
    }
}