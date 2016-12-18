using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemTest
    {
        [TestMethod]
        public void TestAllTypesFrom_System_Dll()
        {
            var assembly = Assembly.LoadFile(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.dll");
            var result = assembly.GetTypes().Where(item => item.IsPublic && !item.IsStatic()).ToList();

            List<string> typesWhichCannotCreated = new List<string>();
            List<string> typesWhichCouldBeCreated = new List<string>();
            foreach (var type in result)
            {
                object createdType = null;
                try
                {
                    createdType = type.Create();
                }
                catch (Exception)
                {
                    // ignored, catch all types which has problems to create
                }

                if (createdType == null)
                {
                    typesWhichCannotCreated.Add(type.FullName);
                }
                else
                {
                    typesWhichCouldBeCreated.Add(type.FullName);
                    // Debug.WriteLine($"Type: {type.FullName} successfully created.");
                }
            }

            Debug.WriteLine($"All types which were successfully created: {typesWhichCouldBeCreated.Count}");
            Debug.WriteLine(CreateErrorString(typesWhichCouldBeCreated));

            // We know that we can not create all !!
            // This test is more for info which types we can not create.
            Assert.IsTrue(typesWhichCannotCreated.Any());
        }

        private static string CreateErrorString(List<string> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            errors.ForEach(item => stringBuilder.AppendLine(item));
            return stringBuilder.ToString();
        }
    }
}