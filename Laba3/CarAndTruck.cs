using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    public class CarAndTruck : CarModel
    {
        string CarName1;
        string CarModel1;
        string Рorsepower1;
        string MaxSpeed1;
        string CarType1;
        public CarAndTruck(string CarName1, string CarModel1, string Рorsepower1, string MaxSpeed1, string CarType1)
        {
            this.CarName1 = CarName1;
            this.CarModel1 = CarModel1;
            this.Рorsepower1 = Рorsepower1;
            this.MaxSpeed1 = MaxSpeed1;
            this.CarType1 = CarType1;
        }
        public string CarName
        {
            get {return CarName1; }
            set { CarName1 = value; }
        }
        
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
        
        public string MaxSpeed
        {
            get { return MaxSpeed1; }
            set { MaxSpeed1 = value; }
        }
        
        public string CarType
        {
            get { return CarType1; }
            set { CarType1 = value; }
        }
        public CarAndTruck()
        {
            CarName1 = "" ;
            CarModel1 = "";
            Рorsepower1 = "";
            MaxSpeed1 = "";
            CarType1 = "";
        }

    }
}
