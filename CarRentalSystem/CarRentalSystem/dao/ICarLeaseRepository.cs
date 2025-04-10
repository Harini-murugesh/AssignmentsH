using CarRentalSystem.entity;

namespace CarRentalSystem.dao
{
    public interface ICarLeaseRepository
    {
        // Car Management
        void AddCar(Vehicle car);                        // Add new car
        void RemoveCar(int carID);
        List<Vehicle> ListAvailableCars();               // List all available cars
        List<Vehicle> ListRentedCars();                  // List all rented cars
        Vehicle FindCarById(int carID);                  // Find car by ID

        // Customer Management
        void AddCustomer(Customer customer);             // Add a new customer
        void RemoveCustomer(int customerID);             // Remove a customer by ID
        List<Customer> ListCustomers();                  // List all customers
        Customer FindCustomerById(int customerID);       // Find customer by ID

        // Lease Management
        Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate, string type); // Create a new lease
        Lease ReturnCar(int leaseID);                    // Return the car and close the lease
        List<Lease> ListActiveLeases();                  // List all active leases
        List<Lease> ListLeaseHistory();                  // List all completed leases
        void RemoveLease(int leaseID);                   // Remove a lease by ID
        void AddPayment(Payment payment);

    }
}
