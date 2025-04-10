using NUnit.Framework;
using CarRentalSystem.entity;
using System;

namespace CarRentalSystem.tests
{
    [TestFixture]
    public class LeaseTests
    {
        [TestCase(1, 101, 1, "2024-01-01", "2024-01-10", "DailyLease")]
        public void TestLeaseProperties(int leaseId, int vehicleId, int customerId, string start, string end, string type)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            Lease lease = new Lease(leaseId, vehicleId, customerId, startDate, endDate, type);

            Assert.That(lease.LeaseID, Is.EqualTo(leaseId));
            Assert.That(lease.VehicleID, Is.EqualTo(vehicleId));
            Assert.That(lease.CustomerID, Is.EqualTo(customerId));
            Assert.That(lease.StartDate, Is.EqualTo(startDate));
            Assert.That(lease.EndDate, Is.EqualTo(endDate));
            Assert.That(lease.Type, Is.EqualTo(type));
        }
    }
}
