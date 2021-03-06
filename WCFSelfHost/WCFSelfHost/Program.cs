﻿using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCFSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Self Host ***");

            using (var host = new ServiceHost(typeof(TrainDepot)))
            {
                var tcp = new NetTcpBinding();
                var smb = new ServiceMetadataBehavior()
                {
                    HttpGetEnabled = true,
                    HttpGetUrl = new Uri("http://localhost:2/mex")
                };
                host.Description.Behaviors.Add(smb);

                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://localhost:1/mex");
                host.AddServiceEndpoint(typeof(ITrainDepot), tcp, "net.tcp://localhost:1");
                host.AddServiceEndpoint(typeof(ITrainDepot), new BasicHttpBinding(), "http://localhost:2");
                host.AddServiceEndpoint(typeof(ITrainDepot), new NetNamedPipeBinding(), "net.pipe://localhost/Trains");
                host.AddServiceEndpoint(typeof(ITrainDepot), new WSHttpBinding(), "http://localhost:3");

                host.Open();

                Console.WriteLine("Service wurde gestartet");
                Console.ReadLine();
            }
            Console.WriteLine("Ende");
            Console.ReadKey();
        }
    }
}
