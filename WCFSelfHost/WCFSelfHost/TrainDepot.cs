using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WCFSelfHost
{
    public class TrainDepot : ITrainDepot
    {
        List<Train> trains = new List<Train>();
        public TrainDepot()
        {
            AddTrain(new Train { Antrieb = "Elektro", Leistung = 5600, Wert = 275.99m });
            AddTrain(new Train { Antrieb = "Diesel", Leistung = 500, Wert = 187.89m });
            AddTrain(new Train { Antrieb = "Elektro", Leistung = 5000, Wert = 409.23m });

        }
        public void AddTrain(Train train)
        {
            train.Id = trains.Count();
            trains.Add(train);
        }

        public IEnumerable<Train> GetTrains()
        {
            return trains;
        }

        public IEnumerable<Train> GetTrainsWithException()
        {
            var defTrain = new Train() { Leistung = -100, Antrieb = "Kaputt", Wert = 100 };
            throw new FaultException<TrainException>(new TrainException() { DefectTrain = defTrain }, "Der Zug ist kaputt!");
        }
    }
}
