using System;
using CarRentalSystem.entity;
using CarRentalSystem.dao;
using System.Collections.Generic;


namespace CarRentalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            static void AddPayment(ICarLeaseRepository repository)
            {
                int leaseId = ReadInt("Enter Lease ID: ");
                double amount = ReadDouble("Enter Payment Amount: ");
                DateTime paymentDate = DateTime.Now;

                Payment payment = new Payment(0, leaseId, paymentDate, amount);
                repository.AddPayment(payment);

                Console.WriteLine("Payment added successfully.");
            }

            ICarLeaseRepository repository = new ICarLeaseRepositoryImpl();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n----- Car Rental System Menu -----");
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. List Available Cars");
                Console.WriteLine("3. Remove Car");
                Console.WriteLine("4. Add Customer");
                Console.WriteLine("5. List Customers");
                Console.WriteLine("6. Remove Customer");
                Console.WriteLine("7. Create Lease");
                Console.WriteLine("8. List Active Leases");
                Console.WriteLine("9. Return Car");
                Console.WriteLine("10. Remove Lease");
               
                Console.WriteLine("11. Add Payment");
                Console.WriteLine("12. Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {

                        case 1:
                            Vehicle car = new Vehicle
                            {
                                VehicleID = ReadInt("Enter Vehicle ID: "),
                                Make = ReadString("Enter Make: "),
                                Model = ReadString("Enter Model: "),
                                Year = ReadInt("Enter Year: "),
                                DailyRate = ReadDouble("Enter Daily Rate: "),
                                Status = "Available",
                                PassengerCapacity = ReadInt("Enter Passenger Capacity: "),
                                EngineCapacity = ReadInt("Enter Engine Capacity: ")
                            };

                            repository.AddCar(car);
                            Console.WriteLine("Car added successfully.");
                            break;

                        case 2:
                            List<Vehicle> availableCars = repository.ListAvailableCars();
                            Console.WriteLine("--- Available Cars ---");
                            foreach (var v in availableCars)
                            {
                                Console.WriteLine($"{v.VehicleID} - {v.Make} {v.Model}, {v.Year}, Rate: {v.DailyRate}");
                            }
                            break;

                        case 3:
                            int carIdToRemove = ReadInt("Enter Car ID to remove: ");
                            repository.RemoveCar(carIdToRemove);
                            Console.WriteLine("Car removed successfully.");
                            break;

                        case 4:
                            Customer customer = new Customer
                            {
                                CustomerID = ReadInt("Enter Customer ID: "),
                                FirstName = ReadString("Enter First Name: "),
                                LastName = ReadString("Enter Last Name: "),
                                Email = ReadString("Enter Email: "),
                                PhoneNumber = ReadString("Enter Phone Number: ")
                            };
                            repository.AddCustomer(customer);
                            Console.WriteLine("Customer added successfully.");
                            break;

                        case 5:
                            List<Customer> customers = repository.ListCustomers();
                            Console.WriteLine("--- Customers ---");
                            foreach (var c in customers)
                            {
                                Console.WriteLine($"{c.CustomerID} - {c.FirstName} {c.LastName}, Email: {c.Email}, Phone: {c.PhoneNumber}");
                            }
                            break;

                        case 6:
                            int customerIdToRemove = ReadInt("Enter Customer ID to remove: ");
                            repository.RemoveCustomer(customerIdToRemove);
                            Console.WriteLine("Customer removed successfully.");
                            break;

                        case 7:
                       
                            int custId = ReadInt("Enter Customer ID: ");
                            int vehicleId = ReadInt("Enter Vehicle ID: ");
                            DateTime startDate = ReadDate("Enter Lease Start Date (yyyy-MM-dd): ");
                            DateTime endDate = ReadDate("Enter Lease End Date (yyyy-MM-dd): ");

                            // Ensure the type entered is valid
                            string type = ReadString("Enter Lease Type (DailyLease or MonthlyLease): ").Trim();
                            if (type != "DailyLease" && type != "MonthlyLease")
                            {
                                Console.WriteLine("Invalid Lease Type. Please enter either 'DailyLease' or 'MonthlyLease'.");
                                break; // Exit the current case if the type is invalid
                            }

                            // Create the lease with the valid type
                            Lease lease = repository.CreateLease(custId, vehicleId, startDate, endDate, type);
                            Console.WriteLine($"Lease created with ID: {lease.LeaseID}");
                            break;


                        case 8:
                            List<Lease> leases = repository.ListActiveLeases();
                            Console.WriteLine("--- Active Leases ---");
                            foreach (var l in leases)
                            {
                                Console.WriteLine($"Lease ID: {l.LeaseID}, Vehicle: {l.VehicleID}, Customer: {l.CustomerID}, From {l.StartDate.ToShortDateString()} to {l.EndDate.ToShortDateString()}");
                            }
                            break;

                        case 9:
                            int leaseId = ReadInt("Enter Lease ID to return: ");
                            Lease returnedLease = repository.ReturnCar(leaseId);
                            Console.WriteLine($"Lease {returnedLease.LeaseID} returned successfully.");
                            break;

                           
                            
                                case 10:
                                    int leaseIdToRemove = ReadInt("Enter Lease ID to remove: ");
                                    repository.RemoveLease(leaseIdToRemove);
                                    Console.WriteLine("Lease removed successfully.");
                                    break;
                            
                         case 12:
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                        case 11:
                            AddPayment(repository);  // pass the repository instance
                            break;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.WriteLine("Exiting Car Rental System.");
        }

        static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return Convert.ToInt32(Console.ReadLine());
        }

        static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            return Convert.ToDouble(Console.ReadLine());
        }

        static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static DateTime ReadDate(string prompt)
        {
            Console.Write(prompt);
            return DateTime.Parse(Console.ReadLine());
        }
    }
}
