using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    internal class Truck : CarModel
    {
        public string RegistrationNumber1;
        public string NumberOfWheels1;
        public string BodyVolume1;
        public string CarModel1;
        public Truck(string CarModel1, string RegistrationNumber1, string NumberOfWheels1, string BodyVolume1)
        {
            this.RegistrationNumber1 = RegistrationNumber1;
            this.NumberOfWheels1 = NumberOfWheels1;
            this.BodyVolume1 = BodyVolume1;
            this.CarModel1 = CarModel1;
        }
        public string CarName
        {
            get { return CarName1; }
            set { CarName1 = value; }
        }
        string CarName1;

        public string CarModel
        {
            get { return CarModel1; }
            set { CarModel1 = value; }
        }
        

        public string Рorsepower
        {
            get { return Рorsepower1; }
            set { Рorsepower1 = value; }
        }
        string Рorsepower1;

        public string MaxSpeed
        {
            get { return MaxSpeed1; }
            set { MaxSpeed1 = value; }
        }
        string MaxSpeed1;

        public string CarType
        {
            get { return CarType1; }
            set { CarType1 = value; }
        }
        string CarType1;
    }
}
