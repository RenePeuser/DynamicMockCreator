using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectCreator.Extensions;

namespace ObjectCreatorTest.Test.ClassCreationTest
{
    [TestClass]
    public class SystemTest
    {
        // This has to be ignored because of fatal error !!
        private static readonly IEnumerable<string> IgnorTypes = new[]
        {
            "EventHandler",
            "Handle",
            "Microsoft.Win32.SystemEvents",
            "System.Net.AuthenticationSchemeSelector",
            "System.Net.HttpContinueDelegate",
            "System.Net.BindIPEndPoint",
            "System.Net.Security.RemoteCertificateValidationCallback",
            "System.Net.Security.LocalCertificateSelectionCallback",
            "System.Net.Security.SslStream",
            "System.Security.Cryptography.X509Certificates.PublicKey",
            "System.Security.Cryptography.X509Certificates.X509Certificate2",
            "System.ComponentModel.Design.DesignerVerb",
            "System.ComponentModel.Design.MenuCommand",
            "System.ComponentModel.Design.ServiceCreatorCallback",
            "System.Text.RegularExpressions.Regex",
            "System.Text.RegularExpressions.MatchEvaluator",

        };

        // ToDo: Make all types could be created, crazy but very very cool stuff !!!!

        [TestMethod]
        public void TestAllTypesFrom_System_Dll()
        {
            var assembly =
                Assembly.LoadFile(
                    @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.dll");
            var result =
                assembly.GetTypes().Where(item => item.IsPublic && !IgnorTypes.Any(i => item.FullName.EndsWith(i))).ToList();

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
            Assert.IsFalse(typesWhichCannotCreated.Any(), CreateErrorString(typesWhichCannotCreated));
        }

        [TestMethod]
        public void TestUri()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<Uri>());
        }

        [TestMethod]
        public void Test()
        {
            var assembly =
                Assembly.LoadFile(
                    @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.dll");

            var types = assembly.GetTypes();

            var result = types.Any(t => t == typeof(IEnumerable));

            var allEnumerations =
                types
                    .Where(item => item.IsInterface);

            var grouped = allEnumerations.GroupBy(item => item.Namespace);
            var stringBuilder = new StringBuilder();
            foreach (var grouping in grouped)
            {
                stringBuilder.AppendLine($"Namespace: {grouping.Key}");
                foreach (var type in grouping)
                {
                    stringBuilder.AppendLine(type.Name);
                }
            }

            Debug.Write(stringBuilder.ToString());
        }

        private static string CreateErrorString(List<string> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            errors.ForEach(item => stringBuilder.AppendLine(item));
            return stringBuilder.ToString();
        }
    }
}