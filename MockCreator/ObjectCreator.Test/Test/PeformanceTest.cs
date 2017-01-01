using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ObjectCreatorTest.Interfaces;

namespace ObjectCreatorTest.Test
{
    [TestClass]
    public class PerformanceTest
    {
        // This test is only to capture test initialize time here !!
        [TestMethod]
        public void Init()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CreateListByActivator()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(Activator.CreateInstance(typeof(List<string>)));
            }
        }
        [TestMethod]
        public void CreateListWithItemsByActivatorWithCtorParameter()
        {
            var items = new List<string>() { "a", "b", "c" };
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(Activator.CreateInstance(typeof(List<string>), items));
            }
        }

        [TestMethod]
        public void CreateListWithItemsByAdding()
        {
            for (int i = 0; i < 10000; i++)
            {
                var list = (List<string>)Activator.CreateInstance(typeof(List<string>));
                list.Add("a");
                list.Add("b");
                list.Add("c");
                Assert.IsNotNull(Activator.CreateInstance(typeof(List<string>)));
            }
        }

        [TestMethod]
        public void CreateListByMethodWithReflection()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(typeof(PerformanceTest).GetMethod(nameof(PerformanceTest.CreateListMethod),
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .Invoke(this, new object[] { }));
            }
        }

        private static List<string> CreateListMethod()
        {
            return new List<string>();
        }

        [TestMethod]
        public void CreateListWithNewOperator()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsNotNull(new List<string>());
            }
        }

        [TestMethod]
        public void CreateListWithMakeGenericType()
        {
            for (int i = 0; i < 10000; i++)
            {
                var type = typeof(List<>).MakeGenericType(typeof(string));
                Assert.IsNotNull(Activator.CreateInstance(type));
            }
        }

        [TestMethod]
        public void CreateListWithMakeGenericTypeWithAddingItems()
        {
            for (int i = 0; i < 10000; i++)
            {
                var type = typeof(List<>).MakeGenericType(typeof(string));
                var list = (IList)Activator.CreateInstance(type);
                list.Add("a");
                list.Add("b");
                list.Add("c");
            }
        }

        [TestMethod]
        public void InvokeMethodByReflection()
        {
            var value = "hello";
            var methodInfo = typeof(string).GetMethod(nameof(string.IndexOf), new[] { typeof(char) });

            for (int i = 0; i < 100000; i++)
            {
                Assert.AreEqual(4, methodInfo.Invoke(value, new object[] { 'o' }));
            }
        }

        [TestMethod]
        public void InvokeMethodByDelegate()
        {
            var value = "hello";
            var methodInfo = typeof(string).GetMethod(nameof(string.IndexOf), new[] { typeof(char) });
            var indexOfDelegate = (Func<char, int>)Delegate.CreateDelegate(typeof(Func<char, int>), value, methodInfo);

            for (int i = 0; i < 100000; i++)
            {
                Assert.AreEqual(4, indexOfDelegate('o'));
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
        public void CreateListWithCtorParamByMakeGenericType()
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

        [TestMethod]
        public void SubstituteFor()
        {
            var result = Substitute.For(new Type[] { typeof(IMyInterfaceWithEnumerations) }, new object[] { });

            var test = Substitute.For<IMyInterfaceWithEnumerations>();

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