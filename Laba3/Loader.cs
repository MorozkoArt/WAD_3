using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Laba3
{
    public class Loader
    {
        public int process;
        public Random rnd = new Random();
        public Dictionary<CarAndTruck, List<CarModel>> car_truck = new Dictionary<CarAndTruck, List<CarModel>>();
        public Dictionary<CarAndTruck, List<CarModel>> Load(CarAndTruck CARRR)
        {
            process = 0;
            if (!car_truck.ContainsKey(CARRR))
            {
                int value = rnd.Next(10, 21);
                car_truck.Add(CARRR, new List<CarModel>());
                for (int i = 0; i < value; i++)
                {
                    if (CARRR.CarType == "Truck")
                    {
                        Truck truck2 = new Truck(CARRR.CarModel,  RndStr(), RndWH(), RndV());
                        car_truck[CARRR].Add(truck2);
                    }
                    else if (CARRR.CarType == "passenger car")
                    {
                        Car car22 = new Car(CARRR.CarModel, RndStr(), RndMult(), rnd.Next(4, 30).ToString());
                        car_truck[CARRR].Add(car22);
                    }
                    else if (CARRR.CarType == "Airplane")
                    {
                        Airplane car223 = new Airplane(CARRR.CarModel, RndStr(), rnd.Next(2, 12).ToString(), rnd.Next(20, 70).ToString(), rnd.Next(100, 300).ToString());
                        car_truck[CARRR].Add(car223);
                    }
                    process = (int)(((double)(i + 1) / value) * 100);
                    Thread.Sleep(rnd.Next(0, 500));
                }
            }
            else
            {
                process = 100;
            }
            return car_truck;

        }
        public string RndStr()
        {
            int letters = 2;
            int numbers = 3;
            int letters2 = 1;
            StringBuilder sb = new StringBuilder(letters + numbers + letters2);
            string letterSet = "АВЕКМНОРСТУХ";
            string numberSet = "0123456789";
            for (int i = 0; i < letters; i++)
                sb.Append(letterSet[rnd.Next(letterSet.Length)]);
            for (int i = 0; i < numbers; i++)
                sb.Append(numberSet[rnd.Next(numberSet.Length)]);
            for (int i = 0; i < letters2; i++)
                sb.Append(letterSet[rnd.Next(letterSet.Length)]);
            return sb.ToString();
        }
        public string RndMult()
        {
            List<string> music = new List<string>() { "Prology Retro ONE", "JBL1Y-ggg", "MARSHALL-SOUND", "Alpine UTE-92BT", "Pioneer SPH-10BT", "Boss MR508UABW" };
            string ff = music[rnd.Next(music.Count)];
            return ff;
        }
        public string RndV()
        {
            StringBuilder sV = new StringBuilder( );
            sV.Append(rnd.Next(9, 60).ToString());
            sV.Append(" м³");
            return sV.ToString();
        }
        
        public string RndWH()
        {
            List<string> whils_list = new List<string>() { "4Х2", "6Х2", "8Х4", "6Х4", "12X2", "12X4", "8Х2", "14X2", "14X4", "14X6", "18X2", "18X4", "18X6" };
            string Whils = whils_list[rnd.Next(whils_list.Count)];
            return Whils;
        }
        public int getProcess()
        {
            return process;
        }

    }
}
