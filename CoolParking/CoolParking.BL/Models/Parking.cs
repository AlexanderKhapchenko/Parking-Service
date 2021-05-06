// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.
using System;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Globalization;

namespace CoolParking.BL.Models
{
    public class Parking
    {
        readonly public List<Vehicle> vehicles = new List<Vehicle>(Settings.capacity) { };

        public Parking(){}

        private static Parking instance;

        public static Parking getInstance()
        {
            if (instance == null)
                instance = new Parking();
            return instance;
        }
    }
}
