// TODO: implement class Settings.
//+       Implementation details are up to you, they just have to meet the requirements of the home task.
using System;

namespace CoolParking.BL.Models
{
    public class Settings
    {
        public static decimal initialBalance = 0;
        public static int capacity = 10;
        public static int payTime = 5000;
        public static int logTime = 60000;
        public static decimal coefPanalty = 2.5m;
        public static string logPath = "Transaction.log";
        public static decimal Tariff(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.PassengerCar: return 2;
                case VehicleType.Truck: return 5;
                case VehicleType.Bus: return 3.5m;
                case VehicleType.Motorcycle: return 1;
                default: Console.WriteLine("Such type of transport not founded"); return 0;
            }
        } 
    }
}