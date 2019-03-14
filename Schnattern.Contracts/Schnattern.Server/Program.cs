using Schnattern.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
                host.AddServiceEndpoint(typeof(IServer), tcp, "");

                host.Open();
                Console.WriteLine("Server wurde gestartet");
                Console.ReadLine();
            }

            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
