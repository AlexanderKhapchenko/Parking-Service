// TODO: implement class Vehicle.
//+       Properties: Id (string), VehicleType (VehicleType), Balance (decimal).
//*       The format of the identifier is explained in the description of the home task.
//+        Id and VehicleType should not be able for changing.
//-       The Balance should be able to change only in the CoolParking.BL project.
//+       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//*       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.
using System;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Xml;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        string id;
        VehicleType vehicleType;
        decimal balance;
        public static Regex rgx = new Regex(@"^[a-zA-Z]{2}-[0-9]{4}-[a-zA-Z]{2}$");
        public string Id
        {
            get { return id; }
        }
        public VehicleType VehicleType
        {
            get { return vehicleType; }
        }
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }
      //  public object PartID { get; internal set; }

        public void AddBalance(decimal cash)
        {
            balance += cash;
        }

        public static string GenerateRandomRegistrationPlateNumber()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[=]", "");
        }
        public Vehicle(string _id, VehicleType _vehicleType, decimal _balance)
        {
            if (_balance < 0 || !rgx.IsMatch(_id) || !Enum.IsDefined(typeof(VehicleType), _vehicleType))
            {
                throw new System.ArgumentException();
            }
            this.id = _id;
            this.vehicleType = _vehicleType;
            this.balance = _balance;
        }
    }
}