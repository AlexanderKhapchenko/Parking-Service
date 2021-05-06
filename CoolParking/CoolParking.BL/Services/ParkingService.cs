// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.
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
using System.IO;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        Parking parking = Parking.getInstance();
        public decimal balance = Settings.initialBalance;
        /*private static ParkingService _instance;*/
        readonly ITimerService withdrawTimer;
        readonly ITimerService logTimer;
        readonly ILogService logService;
        List<TransactionInfo> transactionInfo = new List<TransactionInfo>();
        TransactionInfo TransactionInfo;
        public ParkingService(ITimerService _withdrawTimer, ITimerService _logTimer, ILogService _logService)
        {
            /*payTimer.Start();
            payTimer.Interval = 5000;
            payTimer.Elapsed += TimeToPay;
            */
            if (withdrawTimer == null && logTimer == null && logService == null) 
            {
                withdrawTimer = _withdrawTimer;
                logTimer = _logTimer;
                logService = _logService;

                //? 
                logTimer.Start();
                logTimer.Interval = Settings.logTime;
                logTimer.Elapsed += LogTimer_Elapsed;

                withdrawTimer.Start();
                withdrawTimer.Interval = Settings.payTime;
                withdrawTimer.Elapsed += WithdrawTimer_Elapsed;
            }
        }

        private void WithdrawTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            foreach (Vehicle _vehicle in parking.vehicles)
            {
                decimal payBalance = Settings.Tariff(_vehicle.VehicleType);
                if(_vehicle.Balance < Settings.Tariff(_vehicle.VehicleType))
                {
                    payBalance *= Settings.coefPanalty;
                }
                if(_vehicle.Balance < -2000000000) { //Fix error with overflow Vehicle balance
                    payBalance = 0;
                }
                _vehicle.Balance -= payBalance;
                 
                if(balance < 2000000000) //Fix error with overflow balance
                    balance += payBalance;

                TransactionInfo.AddTransactionInfo(_vehicle, DateTime.Now);
                transactionInfo.Add(TransactionInfo);
            }
        }

        private void LogTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
          //  if(!transactionInfo.Any())
          //  {
          //      return;
          //  }
            string transactionsString = "";
            foreach(TransactionInfo item in transactionInfo)
            {
                //transactionsString += $"ID: {item.idTransaction} | TIME: {item.timeTransaction}sec | MONEY: {item.moneyTransaction}";
                transactionsString += $"{item.timeTransaction} {item.moneyTransaction} money withdrawn from vehicle with Id='{item.idTransaction}'.\n";
            }            
            try
            {   
                logService.Write(transactionsString);
                transactionInfo.Clear();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }

        public Vehicle GetVehicle(string vehicleId)
        {
            try
            {
                if (!(Vehicle.rgx.IsMatch(vehicleId)))
                {
                    throw new InvalidDataException();
                }
                Vehicle vehicle = parking.vehicles.Single(r => r.Id == vehicleId);
               
                return vehicle;
            }
            catch(InvalidOperationException)
            {
                throw new ArgumentException();
            }
        }


        public void RemoveVehicle(string vehicleId)
        {

            try
            {             
                if (!(Vehicle.rgx.IsMatch(vehicleId)))
                {
                    throw new InvalidDataException();
                }
                Vehicle vehicle = parking.vehicles.Single(r => r.Id == vehicleId);
                if (vehicle.Balance < 0)
                {
                    throw new InvalidDataException("Vehicle have negative balance");
                }
                parking.vehicles.Remove(vehicle);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException();
            }
            catch (ArgumentNullException)
            {
                throw new InvalidDataException();
            }
            /////
            /*try
            {
                if (!(Vehicle.rgx.IsMatch(vehicleId)))
                {
                    throw new InvalidDataException();
                }
                Vehicle vehicle = parking.vehicles.Single(r => r.Id == vehicleId);
                if (vehicle.Balance < 0)
                {
                    throw new InvalidOperationException("Vehicle have negative balance");
                }
                else if (!parking.vehicles.Exists(x => x.Id == vehicleId))
                {
                    throw new ArgumentException();
                }
                parking.vehicles.Remove(vehicle);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException(String.Format("{0} id not found", vehicleId));
            }
            catch (InvalidDataException)
            {
                throw new ArgumentException();
            }*/
        }
        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                if (parking.vehicles.Exists(x => x.Id == vehicle.Id))
                {
                    throw new System.ArgumentException();
                }
                if(parking.vehicles.Count >= Settings.capacity)
                {
                    throw new System.InvalidOperationException();
                }
                parking.vehicles.Add(vehicle);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }

        }
        public decimal GetBalance()
        {
            return balance;
        }
        public int GetCapacity()
        {
            return Settings.capacity;
        }
        public int GetFreePlaces()
        {
            return Settings.capacity - parking.vehicles.Count;
        }
        public TransactionInfo[] GetLastParkingTransactions()
        {
            TransactionInfo[] item = transactionInfo.ToArray();
            return item;
        }
        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            return new ReadOnlyCollection<Vehicle>(parking.vehicles);
        }
        public string ReadFromLog()
        {
            return logService.Read();
        }
        public void TopUpVehicle(string vehicleId, decimal sum)
        {        
            try
            {
                if (sum < 0)
                {
                    throw new InvalidDataException();
                }
                if(!Vehicle.rgx.IsMatch(vehicleId))
                {
                    throw new ArgumentException();
                }
                Vehicle vehicle = parking.vehicles.Single(r => r.Id == vehicleId);
                vehicle.AddBalance(sum);
            }
            catch (System.InvalidOperationException)
            {
                throw new ArgumentException(String.Format("{0} id not found", vehicleId));
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    withdrawTimer.Stop();
                    withdrawTimer.Dispose();
                    logTimer.Stop();
                    logTimer.Dispose();

                }
               transactionInfo.Clear();
               parking.vehicles.Clear();
               disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            //GC.SuppressFinalize(this);
        }
        #endregion

    }
}