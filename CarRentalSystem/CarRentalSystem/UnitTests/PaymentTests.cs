using NUnit.Framework;
using CarRentalSystem.entity;
using System;

namespace CarRentalSystem.tests
{
    [TestFixture]
    public class PaymentTests
    {
        [TestCase(1, 1, "2024-01-10", 450.00)]
        public void TestPaymentProperties(int paymentId, int leaseId, string date, double amount)
        {
            DateTime paymentDate = DateTime.Parse(date);

            Payment payment = new Payment(paymentId, leaseId, paymentDate, amount);

            Assert.That(payment.PaymentID, Is.EqualTo(paymentId));
            Assert.That(payment.LeaseID, Is.EqualTo(leaseId));
            Assert.That(payment.PaymentDate, Is.EqualTo(paymentDate));
            Assert.That(payment.Amount, Is.EqualTo(amount));
        }
    }
}
