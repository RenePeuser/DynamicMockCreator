using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class PeformanceTest
    {
        [TestMethod]
        public void Init()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestActivator()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(Activator.CreateInstance(typeof(List<string>)));
            }
        }

        [TestMethod]
        public void TestActivator111()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(typeof(PeformanceTest).GetMethod("CreateList",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .Invoke(this, new object[] { }));
            }
        }

        private static List<string> CreateList()
        {
            return new List<string>();
        }

        [TestMethod]
        public void TestNew()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(new List<string>());
            }
        }

        [TestMethod]
        public void MakeGenericType()
        {
            for (int i = 0; i < 10000; i++)
            {
                var type = typeof(List<>).MakeGenericType(typeof(string));
                Assert.IsNotNull(Activator.CreateInstance(type));
            }
        }

        [TestMethod]
        public void InvokeMethodByReflection()
        {
            var value = "abcbcc";
            var methodInfo = typeof(string).GetMethod(nameof(string.IndexOf), new[] { typeof(char) });

            for (int i = 0; i < 100000; i++)
            {
                Assert.AreEqual(2, methodInfo.Invoke(value, new object[] { 'c' }));
            }
        }

        [TestMethod]
        public void InvokeMethodByDelegate()
        {
            var value = "abcbcc";
            var methodInfo = typeof(string).GetMethod(nameof(string.IndexOf), new[] { typeof(char) });
            var indexOfDelegate = (Func<char, int>)Delegate.CreateDelegate(typeof(Func<char, int>), value, methodInfo);

            for (int i = 0; i < 100000; i++)
            {
                Assert.AreEqual(2, indexOfDelegate('c'));
            }
        }


        [TestMethod]
        public void InvokeGenericMethodByReflection()
        {
            var value = new Collection<string>();
            var methodInfo = typeof(MethodSample).GetMethod(nameof(MethodSample.ToList), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            for (int i = 0; i < 1000; i++)
            {
                // Think in productive mode you have always different types.
                var genericMethod = methodInfo.MakeGenericMethod(new[] { typeof(string) });
                Assert.IsNotNull(genericMethod.Invoke(null, new object[] { value }));
            }
        }

        [TestMethod]
        public void InvokeGenericMethodByDelegate()
        {
            var methodInfo = typeof(MethodSample).GetMethod(nameof(MethodSample.ToList), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            for (int i = 0; i < 1000; i++)
            {
                // Think in productive mode you have always different types.
                var genericMethod = methodInfo.MakeGenericMethod(new[] { typeof(string) });
                var toListDelegate =
                    (Func<IEnumerable<string>, List<string>>)
                    Delegate.CreateDelegate(typeof(Func<IEnumerable<string>, List<string>>), null, genericMethod);

                Assert.IsNotNull(toListDelegate(new Collection<string>()));
            }
        }

        [TestMethod]
        public void ActivatorCreation()
        {
            var collection = new Collection<string>();
            for (int i = 0; i < 10000; i++)
            {
                var type = typeof(List<>).MakeGenericType(new[] { typeof(string) });
                Assert.IsNotNull(Activator.CreateInstance(type, collection));
            }
        }

        [TestMethod]
        public void CreateByGenericMethod()
        {
            var collection = new Collection<string>();
            var methodInfo = typeof(MethodSample).GetMethod(nameof(MethodSample.ToList), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            for (int i = 0; i < 10000; i++)
            {
                var genericMethod = methodInfo.MakeGenericMethod(typeof(string));
                genericMethod.Invoke(null, new object[] { collection });
                Assert.IsNotNull(genericMethod.Invoke(null, new object[] { collection }));
            }
        }

    }

    public static class MethodSample
    {
        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            return new List<T>(source);
        }
    }
}