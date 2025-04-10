namespace CarRentalSystem.entity
{
    public class Vehicle
    {
//vehicle
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double DailyRate { get; set; }
        public string Status { get; set; }
        public int PassengerCapacity { get; set; }
        public int EngineCapacity { get; set; }

        
        public Vehicle() { }

        public Vehicle(int vehicleID, string make, string model, int year, double dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }
    }
}
