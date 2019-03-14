using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFSelfHost
{
    [ServiceContract]
    public interface ITrainDepot
    {
        [OperationContract]
        IEnumerable<Train> GetTrains();

        [OperationContract]
        [FaultContract(typeof(TrainException))]
        IEnumerable<Train> GetTrainsWithException();

        [OperationContract]
        void AddTrain(Train train);
    }

    [DataContract]
    public class TrainException
    {
        [DataMember]
        public Train DefectTrain { get; set; }
    }
}
