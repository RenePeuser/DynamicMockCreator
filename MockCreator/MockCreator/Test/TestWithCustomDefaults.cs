using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;

namespace MockCreatorTest.Test
{
    [TestClass]
    public class TestWithCustomDefaults
    {
        private const sbyte ExpectedSByte = 1;
        private const byte ExpectedByte = 2;
        private const short ExpectedShort = 3;
        private const ushort ExpectedUShort = 4;
        private const int ExpectedInt = 5;
        private const uint ExpectedUInt = 6;
        private const long ExpectedLong = (long) 1.1;
        private const ulong ExpectedULong = (ulong) 1.2;
        private const char ExpectedChar = '?';
        private const float ExpectedFloat = (float) 1.3;
        private const double ExpectedDouble = 1.4;
        private const bool ExpectedBool = true;
        private const string ExpectedString = "15102016";
        private static DefaultData _defaultData;
        private static readonly decimal ExpectedDecimal = new decimal(1.5);
        private static readonly DateTime ExpectedDateTime = new DateTime(2016, 10, 15);
        private static readonly List<string> ExpectedList = new List<string> {"A", "B", "C"};

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _defaultData = new DefaultData(ExpectedSByte, ExpectedByte, ExpectedShort, ExpectedUShort, ExpectedInt,
                ExpectedUInt, ExpectedLong, ExpectedULong, ExpectedChar, ExpectedFloat, ExpectedDouble, ExpectedBool,
                ExpectedDecimal,
                ExpectedString, ExpectedDateTime, ExpectedList);
        }

        [TestMethod]
        public void TestSByte()
        {
            Assert.AreEqual(ExpectedSByte, SubstituteExtensions.For<sbyte>(_defaultData));
        }

        [TestMethod]
        public void TestByte()
        {
            Assert.AreEqual(ExpectedByte, SubstituteExtensions.For<byte>(_defaultData));
        }

        [TestMethod]
        public void TestShort()
        {
            Assert.AreEqual(ExpectedShort, SubstituteExtensions.For<short>(_defaultData));
        }

        [TestMethod]
        public void TestUShort()
        {
            Assert.AreEqual(ExpectedUShort, SubstituteExtensions.For<ushort>(_defaultData));
        }

        [TestMethod]
        public void TestInt()
        {
            Assert.AreEqual(ExpectedInt, SubstituteExtensions.For<int>(_defaultData));
        }

        [TestMethod]
        public void TestUInt()
        {
            Assert.AreEqual(ExpectedUInt, SubstituteExtensions.For<uint>(_defaultData));
        }

        [TestMethod]
        public void TestLong()
        {
            Assert.AreEqual(ExpectedLong, SubstituteExtensions.For<long>(_defaultData));
        }

        [TestMethod]
        public void TestULong()
        {
            Assert.AreEqual(ExpectedULong, SubstituteExtensions.For<ulong>(_defaultData));
        }

        [TestMethod]
        public void TestChar()
        {
            Assert.AreEqual(ExpectedChar, SubstituteExtensions.For<char>(_defaultData));
        }

        [TestMethod]
        public void TestFloat()
        {
            Assert.AreEqual(ExpectedFloat, SubstituteExtensions.For<float>(_defaultData));
        }

        [TestMethod]
        public void TestDouble()
        {
            Assert.AreEqual(ExpectedDouble, SubstituteExtensions.For<double>(_defaultData));
        }

        [TestMethod]
        public void TestBool()
        {
            Assert.AreEqual(ExpectedBool, SubstituteExtensions.For<bool>(_defaultData));
        }

        [TestMethod]
        public void TestDecimal()
        {
            Assert.AreEqual(ExpectedDecimal, SubstituteExtensions.For<decimal>(_defaultData));
        }

        [TestMethod]
        public void TestString()
        {
            Assert.AreEqual(ExpectedString, SubstituteExtensions.For<string>(_defaultData));
        }

        [TestMethod]
        public void TestDateTime()
        {
            Assert.AreEqual(ExpectedDateTime, SubstituteExtensions.For<DateTime>(_defaultData));
        }

        [TestMethod]
        public void TestGenericList()
        {
            Assert.AreEqual(ExpectedList, SubstituteExtensions.For<List<string>>(_defaultData));
        }
    }
}