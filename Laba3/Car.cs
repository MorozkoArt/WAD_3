using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    internal class Car : CarModel
    {
        public string RegistrationNumber1;
        public string NameMultimedia1;
        public string NumberOfAirbags1;
        public string CarModel1;
        public Car(string CarModel1, string RegistrationNumber1, string NameMultimedia1, string NumberOfAirbags1) 
        {
            this.RegistrationNumber1 = RegistrationNumber1;
            this.NameMultimedia1 = NameMultimedia1;
            this.NumberOfAirbags1 = NumberOfAirbags1;
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

