using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace HalloWCF.Host
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        int Verdoppeln(int zahl);

        [OperationContract]
        IEnumerable<Fahrrad> GetFahrräder();
    }

    [DataContract]
    public class Fahrrad
    {
        [DataMember]
        public int AnzahlRäder { get; set; }

        [DataMember]
        public int Größe { get; set; }

        [DataMember]
        public string Hersteller { get; set; }

        [DataMember]
        public DateTime Baujahr { get; set; }
        //geheim
        public string Rahmennummer { get; set; }
    }
}
