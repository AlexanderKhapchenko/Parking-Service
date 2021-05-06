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

    class Program : Menu
    {
        static void Main()
        {
            var CoolParkingMenu = new Program();
            bool state = true;
            while (state)
            {
                //CoolParkingMenu.MainMenu();
                switch (Menu.MainMenu())
                {
                    case 1:
                        CoolParkingMenu.CurrentBalanceMenu();
                        break;
                    case 2:
                        CoolParkingMenu.AmountMenu();
                        break;
                    case 3:
                        CoolParkingMenu.FreeOccupiedMenu();
                        break;
                    case 4:
                        CoolParkingMenu.GetLastParkingTransactionsMenu();
                        break;
                    case 5:
                        CoolParkingMenu.ReadFromLogMenu();
                        break;
                    case 6:
                        CoolParkingMenu.GetVehiclesMenu();
                        break;
                    case 7:
                        CoolParkingMenu.AddVenicleMenu();
                        break;
                    case 8:
                        CoolParkingMenu.RemoveVehicleMenu();
                        break;
                    case 9:
                        CoolParkingMenu.TopUpVehicleMenu();
                        break;
                    case 0:
                        state = false;
                        break;
                    default:
                        Menu.MainMenu();
                        break;
                }
            }

        }
    }
}
