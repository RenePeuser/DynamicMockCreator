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
        // This has to be ignored because of fatal error !!
        private static readonly IEnumerable<string> IgnorTypes = new[]
        {
            "EventArgs",
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
                assembly.GetTypes().Where(item => item.IsPublic && !item.IsStatic() && !IgnorTypes.Any(i => item.FullName.EndsWith(i))).ToList();

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
        public void TestFailingSystemTypes()
        {
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.AuthenticationManager>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.FileWebRequest>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.FileWebResponse>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.FtpWebRequest>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.FtpWebResponse>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.HttpListenerContext>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.HttpListenerPrefixCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.HttpListenerRequest>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.HttpListenerResponse>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.HttpListenerTimeoutManager>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.ServicePoint>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.ServicePointManager>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.SocketAddress>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.EndpointPermission>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.OpenReadCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.OpenWriteCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.DownloadStringCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.DownloadDataCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.UploadStringCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.UploadDataCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.UploadFileCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.UploadValuesCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.DownloadProgressChangedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.UploadProgressChangedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.WebSockets.ClientWebSocketOptions>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.WebSockets.HttpListenerWebSocketContext>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.AlternateView>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.AlternateViewCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.AttachmentBase>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.Attachment>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.AttachmentCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.LinkedResource>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.LinkedResourceCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Mail.MailAddress>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.IPAddressInformationCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.UnicastIPAddressInformationCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.MulticastIPAddressInformationCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.IPAddressCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.GatewayIPAddressInformationCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.NetworkAvailabilityEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.PingCompletedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.PingException>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.PingOptions>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.NetworkInformation.PingReply>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Security.AuthenticatedStream>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Security.NegotiateStream>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Sockets.NetworkStream>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Sockets.SendPacketsElement>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Net.Sockets.TcpClient>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Authentication.ExtendedProtection.TokenBinding>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.AsnEncodedData>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.AsnEncodedDataEnumerator>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.OidEnumerator>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X500DistinguishedName>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X509Certificate2Enumerator>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X509ChainElement>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X509ChainElementCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X509Extension>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.Cryptography.X509Certificates.X509ExtensionEnumerator>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.AccessControl.SemaphoreAccessRule>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Security.AccessControl.SemaphoreAuditRule>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Media.SystemSounds>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Media.SystemSound>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Specialized.NotifyCollectionChangedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Specialized.StringEnumerator>());
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.ObjectModel.ObservableCollection`1 > ());
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.ObjectModel.ReadOnlyObservableCollection`1 > ());
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.LinkedList`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.LinkedListNode`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.Queue`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.SortedList`2
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.Stack`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.SortedDictionary`2
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.SortedSet`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Generic.ISet`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Concurrent.BlockingCollection`1
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Collections.Concurrent.ConcurrentBag`1
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Runtime.InteropServices.StandardOleMarshalObject>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.IO.Ports.SerialErrorReceivedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.IO.Ports.SerialPinChangedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.IO.Ports.SerialDataReceivedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.IO.Compression.DeflateStream>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.IO.Compression.GZipStream>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.CorrelationManager>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.DelimitedListTraceListener>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.Trace>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.TraceListenerCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.XmlWriterTraceListener>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.DataReceivedEventArgs>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.EventLogEntry>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.EventLogEntryCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.EventLogPermissionEntryCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.EventSourceCreationData>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.FileVersionInfo>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.PerformanceCounterPermissionEntryCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.ProcessModule>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.ProcessModuleCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.ProcessThread>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Diagnostics.ProcessThreadCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.AsyncOperation>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.BaseNumberConverter>());
            //Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.BindingList`1 > ());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.LicenseManager>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.NullableConverter>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.TypeDescriptor>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.ComponentModel.Design.DesigntimeLicenseContextSerializer>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.CodeDom.Compiler.CompilerInfo>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Text.RegularExpressions.Capture>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Text.RegularExpressions.CaptureCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Text.RegularExpressions.Group>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Text.RegularExpressions.GroupCollection>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create<System.Text.RegularExpressions.Match>());
            Assert.IsNotNull(ObjectCreatorExtensions.Create < System.Text.RegularExpressions.MatchCollection>());
        }

        private static string CreateErrorString(List<string> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            errors.ForEach(item => stringBuilder.AppendLine(item));
            return stringBuilder.ToString();
        }
    }
}