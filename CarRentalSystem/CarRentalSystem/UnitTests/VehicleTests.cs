using NUnit.Framework;
using CarRentalSystem.entity;

namespace CarRentalSystem.tests
{
    [TestFixture]
    public class VehicleTests
    {
        [Test]
        public void TestVehicleProperties()
        {
            // Arrange
            int id = 101;
            string make = "Honda";
            string model = "Civic";
            int year = 2021;
            double rate = 45.50;
            string status = "Available";
            int capacity = 5;
            int engine = 1500; // keeping it int as per your requirement

            // Act
            Vehicle vehicle = new Vehicle(id, make, model, year, rate, status, capacity, engine);

            // Assert
            Assert.That(vehicle.VehicleID, Is.EqualTo(id));
            Assert.That(vehicle.Make, Is.EqualTo(make));
            Assert.That(vehicle.Model, Is.EqualTo(model));
            Assert.That(vehicle.Year, Is.EqualTo(year));
            Assert.That(vehicle.DailyRate, Is.EqualTo(rate));
            Assert.That(vehicle.Status, Is.EqualTo(status));
            Assert.That(vehicle.PassengerCapacity, Is.EqualTo(capacity));
            Assert.That(vehicle.EngineCapacity, Is.EqualTo(engine));
        }
    }
}
