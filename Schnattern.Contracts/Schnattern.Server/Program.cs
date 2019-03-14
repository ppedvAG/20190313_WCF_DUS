using Schnattern.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Schnattern.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Schnattern Server v0.1 ***");

            using (var host = new ServiceHost(typeof(SchnatternServer), new Uri("net.tcp://localhost:1")))
            {
                var tcp = new NetTcpBinding();
                tcp.Security.Mode = SecurityMode.TransportWithMessageCredential;
                tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

                tcp.MaxReceivedMessageSize = int.MaxValue;

                host.AddServiceEndpoint(typeof(IServer), tcp, "");


                host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.Root,
                    X509FindType.FindByThumbprint, "7adab23928ac9f943035cbcc0f89cd45d7d1030d");

                host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                AppContext.SetSwitch("Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate", true);
                host.Open();
                Console.WriteLine("Server wurde gestartet");
                Console.ReadLine();
            }

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
