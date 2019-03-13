using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HalloWCF.Host
{
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return $"You entered: {value}";
        }

        public IEnumerable<Fahrrad> GetFahrräder()
        {
            yield return new Fahrrad()
            {
                AnzahlRäder = 1,
                Größe = 5,
                Baujahr = DateTime.Now.AddYears(-30),
                Hersteller = "ACME",
                Rahmennummer = "55-KF-88-33"
            };

            yield return new Fahrrad()
            {
                AnzahlRäder = 3,
                Größe = 2,
                Baujahr = DateTime.Now.AddYears(-3),
                Hersteller = "Toy'R'Us",
                Rahmennummer = Guid.NewGuid().ToString()
            };

            yield return new Fahrrad()
            {
                AnzahlRäder = 2,
                Größe = 28,
                Baujahr = DateTime.Now.AddYears(-3),
                Hersteller = "Fischer",
                Rahmennummer = Guid.NewGuid().ToString()
            };
        }

        public int Verdoppeln(int zahl)
        {
            return zahl * 2;
        }
    }
}
