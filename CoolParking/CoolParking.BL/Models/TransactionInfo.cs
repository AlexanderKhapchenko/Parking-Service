// TODO: implement struct TransactionInfo.
//       Necessarily implement the Sum property (decimal) - is used in tests.
//       Other implementation details are up to you, they just have to meet the requirements of the homework.


using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CoolParking.BL.Services;

namespace CoolParking.BL.Models
{
    public struct TransactionInfo
    {
        public decimal moneyTransaction;
        public string idTransaction;
        public DateTime timeTransaction;



        public void AddTransactionInfo(Vehicle vehicle, DateTime _timeTransaction)
        {
            idTransaction = vehicle.Id;
            moneyTransaction = Settings.Tariff(vehicle.VehicleType);
            timeTransaction = _timeTransaction;
        }
        public void ResetSum()
        {
            moneyTransaction = 0;
        }
        
        public decimal Sum
        {
            get
            {
                return moneyTransaction;
            }
        }
    }
}