using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    internal class Car_Truck
    {
        public string RegistrationNumber;
        public string NameMultimedia_NumberOfWheels;
        public string NumberOfAirbags_BodyVolume;
        public Car_Truck( string registrationNumber, string nameMultimedia, string numberOfAirbags) 
        {
            RegistrationNumber = registrationNumber;
            NameMultimedia_NumberOfWheels = nameMultimedia;
            NumberOfAirbags_BodyVolume = numberOfAirbags;
        }
    }
}
