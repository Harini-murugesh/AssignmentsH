using NUnit.Framework;
using CarRentalSystem.entity;

namespace CarRentalSystem.tests
{
    [TestFixture]
    public class CustomerTests
    {
        [TestCase(1, "John", "Doe", "john@example.com", "9876543210")]
        public void TestCustomerProperties(int id, string firstName, string lastName, string email, string phone)
        {
            Customer customer = new Customer(id, firstName, lastName, email, phone);

            Assert.That(customer.CustomerID, Is.EqualTo(id));
            Assert.That(customer.FirstName, Is.EqualTo(firstName));
            Assert.That(customer.LastName, Is.EqualTo(lastName));
            Assert.That(customer.Email, Is.EqualTo(email));
            Assert.That(customer.PhoneNumber, Is.EqualTo(phone));
        }
    }
}
