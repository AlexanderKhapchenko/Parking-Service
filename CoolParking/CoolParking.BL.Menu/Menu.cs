using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Services;
using CoolParking.BL.Models;
using System.Timers;
using System.Linq;

namespace CoolParking.BL.Menu
{
    class Menu
    {
        public static ITimerService withdrawTimer = new TimerService();//TimerService();
        public static ITimerService logTimer = new TimerService();
        public static ILogService logService = new LogService(Settings.logPath);
        //public static int userMode;
        ///private Vehicle vh = new Vehicle("AA-0001-AA", VehicleType.Truck, 100);
        ParkingService CoolParkingMenu = new ParkingService(withdrawTimer, logTimer, logService);
        //Regex rgx = new Regex(@"^[a-zA-Z]{2}-[0-9]{4}-[a-zA-Z]{2}$");
        public static int MainMenu()
        {
            Console.WriteLine("1. Current parking balance");
            Console.WriteLine("2. Amount of money earned for the current period.");
            Console.WriteLine("3. Amount free/occupied parking places.");
            Console.WriteLine("4. All Parking Transactions for the current period.");
            Console.WriteLine("5. Transaction history.");
            Console.WriteLine("6. List of Vehicles located in the Parking.");
            Console.WriteLine("7. Add Vehicles in the Parking");
            Console.WriteLine("8. Remove the Vehicle from Parking.");
            Console.WriteLine("9. Top up the balance of Vehicle");
            Console.WriteLine("0. End and Exit");
            return Convert.ToInt32(Console.ReadLine());
        }
        public void AddVenicleMenu()
        {
            try
            {
                Console.WriteLine("Enter your vehicle number");
                string idNumber = Console.ReadLine();
                Console.WriteLine("Choose type of your vehicle");
                Console.WriteLine("1 - PassengerCar, 2 - Truck, 3 - Bus, 4 - Motorcycle");

                int typecar = Convert.ToInt32(Console.ReadLine()) - 1;
                VehicleType wdEnum;
                Enum.TryParse<VehicleType>(typecar.ToString(), out wdEnum);
                Console.WriteLine(wdEnum);

                Console.Write("Enter your balance - ");
                decimal balanceUser = Convert.ToDecimal(Console.ReadLine());
                Vehicle vehicle = new Vehicle(idNumber, wdEnum, balanceUser);
                CoolParkingMenu.AddVehicle(vehicle);
                Console.WriteLine("Your vehicle added on parking");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
        }
        public void RemoveVehicleMenu()
        {
            try
            {
                Console.WriteLine("Next step helps you remove vehicle from parking");
                Console.WriteLine("Enter vehicle id");
                string userVehicleIdToRemove = Console.ReadLine();
                CoolParkingMenu.RemoveVehicle(userVehicleIdToRemove);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
        }
        public void TopUpVehicleMenu()
        {
            try
            {
                Console.WriteLine("Next step helps you add money");
                Console.WriteLine("Enter vehicle id");
                string userVehicleIdToAddMoney = Console.ReadLine();
                Console.WriteLine("The amount of money you need to add ");
                decimal userMoney = Convert.ToDecimal(Console.ReadLine());
                CoolParkingMenu.TopUpVehicle(userVehicleIdToAddMoney, userMoney);
                Console.WriteLine("Well, wait some time...");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
        }
        public void ReadFromLogMenu()
        {
            Console.WriteLine("Transactions.log" + CoolParkingMenu.ReadFromLog());
        }
        public void GetVehiclesMenu()
        {
            Console.WriteLine("Vehicle in the parking" + CoolParkingMenu.GetVehicles());

        }
        public void GetLastParkingTransactionsMenu()
        {
            Console.WriteLine("All transaction log for last minutes" + CoolParkingMenu.GetLastParkingTransactions());
        }
        public void FreeOccupiedMenu()
        {
            try
            {
                Console.WriteLine("Free parking spaces - " + CoolParkingMenu.GetFreePlaces());
                Console.WriteLine("Occupied parking spaces" + (Settings.capacity - CoolParkingMenu.GetFreePlaces()));
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
        }
        public void CurrentBalanceMenu()
        {
            try
            {
                Console.WriteLine("Current parking balance - " + CoolParkingMenu.GetBalance());
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Soory, incorrect incoming data. Try again! ");
            }
        }
        public void AmountMenu()
        {
            Console.WriteLine("Money have - " + Settings.initialBalance);
        }
    }
}

