using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarRentalSystem.entity;
using CarRentalSystem.myexceptions;
using CarRentalSystem.util;
using CarRentalSystem.dao;
using System.Reflection;


namespace CarRentalSystem.dao
{
    public class ICarLeaseRepositoryImpl : ICarLeaseRepository
    {
        // --- Car Management ---

        public void AddCar(Vehicle car)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Vehicle (Make, Model, Year, DailyRate, Status, PassengerCapacity, EngineCapacity) " +
                        "VALUES (@Make, @Model, @Year, @DailyRate, @Status, @PassengerCapacity, @EngineCapacity)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        // Use the properties of the 'car' object to set the parameters
                        cmd.Parameters.AddWithValue("@Make", car.Make);
                        cmd.Parameters.AddWithValue("@Model", car.Model);
                        cmd.Parameters.AddWithValue("@Year", car.Year);
                        cmd.Parameters.AddWithValue("@DailyRate", car.DailyRate);
                        cmd.Parameters.AddWithValue("@Status", car.Status); // Ensure this is set, e.g. "available" or "notAvailable"
                        cmd.Parameters.AddWithValue("@PassengerCapacity", car.PassengerCapacity);
                        cmd.Parameters.AddWithValue("@EngineCapacity", car.EngineCapacity);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Vehicle added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add vehicle.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }


        public void RemoveCar(int carID)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "DELETE FROM Vehicle WHERE VehicleID = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", carID);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new CarNotFoundException($"Car with ID {carID} not found.");
                }
            }
        }

        public List<Vehicle> ListAvailableCars()
        {
            return GetCarsByStatus("available");
        }

        public List<Vehicle> ListRentedCars()
        {
            return GetCarsByStatus("rented");
        }

        private List<Vehicle> GetCarsByStatus(string status)
        {
            List<Vehicle> list = new List<Vehicle>();
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE LOWER(Status) = @status";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status.ToLower());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Vehicle(
                                (int)reader["VehicleID"],
                                reader["Make"].ToString(),
                                reader["Model"].ToString(),
                                Convert.ToInt32(reader["Year"]),
                                Convert.ToDouble(reader["DailyRate"]),
                                reader["Status"].ToString(),
                                Convert.ToInt32(reader["PassengerCapacity"]),
                                Convert.ToInt32(reader["EngineCapacity"])
                            ));
                        }
                    }
                }
            }
            return list;
        }


        public Vehicle FindCarById(int carID)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE VehicleID = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", carID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Vehicle(
                                (int)reader["VehicleID"],
                                reader["Make"].ToString(),
                                reader["Model"].ToString(),
                                Convert.ToInt32(reader["Year"]),
                                Convert.ToDouble(reader["DailyRate"]),
                                reader["Status"].ToString(),
                                Convert.ToInt32(reader["PassengerCapacity"]),
                                Convert.ToInt32(reader["EngineCapacity"])
                            );
                        }
                        else
                        {
                            throw new CarNotFoundException($"Car with ID {carID} not found.");
                        }
                    }
                }
            }
        }



        // --- Customer Management ---

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();

                // SQL query to insert customer data
                string insertQuery = "INSERT INTO Customer (firstName, lastName, email, phoneNumber) " +
                                     "VALUES (@FirstName, @LastName, @Email, @PhoneNumber);";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    // Add parameters using Customer object properties
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                    try
                    {
                        // Execute the query and check if it was successful
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Customer added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add customer.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur during the insert
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }



        public void RemoveCustomer(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "DELETE FROM Customer WHERE CustomerID = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", customerID);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new CustomerNotFoundException($"Customer with ID {customerID} not found.");
                }
            }
        }

        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM Customer";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        });
                    }
                }
            }
            return customers;
        }
        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate, string type)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();

                // Query to check if the car exists and if it is available
                string checkCarQuery = "SELECT Status FROM Vehicle WHERE VehicleID = @carID";
                string insertLeaseQuery = @"INSERT INTO Lease (CustomerID, VehicleID, StartDate, EndDate, Type)
                                    VALUES (@customerID, @carID, @startDate, @endDate, @Type); 
                                    SELECT SCOPE_IDENTITY();"; // Returns the LeaseID
                string updateVehicleStatus = "UPDATE Vehicle SET Status = 'notAvailable' WHERE VehicleID = @carID";

                // Check the availability of the car
                using (SqlCommand checkCmd = new SqlCommand(checkCarQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@carID", carID);
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string status = reader["Status"].ToString().ToLower();
                            if (status != "available")
                            {
                                throw new Exception("Vehicle is not available for lease.");
                            }
                        }
                        else
                        {
                            throw new CarNotFoundException($"Car with ID {carID} not found.");
                        }
                    }
                }

                // Insert the lease into the Lease table
                int leaseID = 0;  // Initialize leaseID to store the newly generated ID
                using (SqlCommand insertCmd = new SqlCommand(insertLeaseQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@customerID", customerID);
                    insertCmd.Parameters.AddWithValue("@carID", carID);
                    insertCmd.Parameters.AddWithValue("@startDate", startDate);
                    insertCmd.Parameters.AddWithValue("@endDate", endDate);
                    insertCmd.Parameters.AddWithValue("@Type", type); // Include the leaseType

                    // Get the LeaseID of the newly created lease
                    leaseID = Convert.ToInt32(insertCmd.ExecuteScalar());
                }

                // Update the car's status to 'notAvailable' when it is leased
                using (SqlCommand updateCmd = new SqlCommand(updateVehicleStatus, conn))
                {
                    updateCmd.Parameters.AddWithValue("@carID", carID);
                    updateCmd.ExecuteNonQuery();
                }

                // Return the created lease object (including Type)
                return new Lease(leaseID, customerID, carID, startDate, endDate, type); // Include the leaseType
            }
        }



        public List<Lease> ListActiveLeases()
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();

                // Modify the query to find active leases based on endDate
                string query = "SELECT * FROM Lease WHERE EndDate >= GETDATE() AND VehicleID IN (SELECT VehicleID FROM Vehicle WHERE Status = 'notAvailable')";

                List<Lease> activeLeases = new List<Lease>();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int leaseID = Convert.ToInt32(reader["LeaseID"]);
                            int customerID = Convert.ToInt32(reader["CustomerID"]);
                            int vehicleID = Convert.ToInt32(reader["VehicleID"]);
                            DateTime startDate = Convert.ToDateTime(reader["StartDate"]);
                            DateTime endDate = Convert.ToDateTime(reader["EndDate"]);
                            string type = reader["Type"].ToString();

                            Lease lease = new Lease(leaseID, customerID, vehicleID, startDate, endDate, type);
                            activeLeases.Add(lease);
                        }
                    }
                }

                return activeLeases;
            }
        }

        
        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leases = new List<Lease>();
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM Lease";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        leases.Add(new Lease
                        {
                            LeaseID = (int)reader["LeaseID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                           
                            Type = ((bool)reader["IsActive"]) ? "active" : "returned"
                        });
                    }
                }
            }
            return leases;
        }

        public void RemoveLease(int leaseID)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();

                // Step 1: Delete the lease from the database
                string deleteQuery = "DELETE FROM Lease WHERE LeaseID = @LeaseID";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@LeaseID", leaseID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Lease not found or already removed.");
                    }
                }

                // Step 2: Update vehicle status to 'Available'
                string updateVehicleStatusQuery = @"
            UPDATE Vehicle
            SET Status = 'Available'
            WHERE VehicleID IN (
                SELECT VehicleID FROM Lease WHERE LeaseID = @LeaseID
            );";

                using (SqlCommand cmdUpdate = new SqlCommand(updateVehicleStatusQuery, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@LeaseID", leaseID);
                    cmdUpdate.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Lease {leaseID} removed successfully.");
        }


        public Lease ReturnCar(int leaseID)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();

                // First, check if the lease is active and can be returned
         // Get EndDate
string checkLeaseStatusQuery = "SELECT EndDate FROM Lease WHERE LeaseID = @LeaseID";
using (SqlCommand cmdCheckLeaseStatus = new SqlCommand(checkLeaseStatusQuery, conn))
{
    cmdCheckLeaseStatus.Parameters.AddWithValue("@LeaseID", leaseID);
    var result = cmdCheckLeaseStatus.ExecuteScalar();

    if (result != null)
    {
        DateTime endDate = Convert.ToDateTime(result);

        if (endDate <= DateTime.Today) // Already ended
        {
            throw new Exception("This lease has already ended.");
        }
    }
}
                // Second, update the Vehicle's status to 'Available'
                string updateVehicleQuery = @"
        UPDATE v
        SET v.Status = 'Available'
        FROM Vehicle v
        INNER JOIN Lease l ON v.VehicleID = l.VehicleID
        WHERE l.LeaseID = @LeaseID;";

                using (SqlCommand cmdUpdateVehicle = new SqlCommand(updateVehicleQuery, conn))
                {
                    cmdUpdateVehicle.Parameters.AddWithValue("@LeaseID", leaseID);
                    cmdUpdateVehicle.ExecuteNonQuery(); // Execute the update query
                }

                // Third, update the Lease end date to current date
                // Ensure that the EndDate is logically valid per business rule
                string updateLeaseQuery = @"
        UPDATE Lease
        SET EndDate = GETDATE()  -- Set the current date as the end date for the lease
        WHERE LeaseID = @LeaseID AND EndDate IS NULL;";

                using (SqlCommand cmdUpdateLease = new SqlCommand(updateLeaseQuery, conn))
                {
                    cmdUpdateLease.Parameters.AddWithValue("@LeaseID", leaseID);
                    int rowsAffected = cmdUpdateLease.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Lease cannot be returned. It might already be returned or the date is not valid.");
                    }
                }

                // Finally, retrieve the updated Lease
                string selectLeaseQuery = @"
        SELECT l.LeaseID, l.CustomerID, l.VehicleID, l.StartDate, l.EndDate, l.Type
        FROM Lease l
        WHERE l.LeaseID = @LeaseID;";

                using (SqlCommand cmdSelectLease = new SqlCommand(selectLeaseQuery, conn))
                {
                    cmdSelectLease.Parameters.AddWithValue("@LeaseID", leaseID);

                    using (SqlDataReader reader = cmdSelectLease.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int leaseIDResult = Convert.ToInt32(reader["LeaseID"]);
                            int customerID = Convert.ToInt32(reader["CustomerID"]);
                            int vehicleID = Convert.ToInt32(reader["VehicleID"]);
                            DateTime startDate = Convert.ToDateTime(reader["StartDate"]);
                            DateTime endDate = Convert.ToDateTime(reader["EndDate"]);
                            string type = reader["Type"].ToString();

                            // Return the updated lease object
                            return new Lease(leaseIDResult, customerID, vehicleID, startDate, endDate, type);
                        }
                        else
                        {
                            throw new Exception("Lease not found.");
                        }
                    }
                }
            }
        }

        public void AddPayment(Payment payment)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();

                string query = @"INSERT INTO Payment (LeaseID, PaymentDate, Amount)
                         VALUES (@LeaseID, @PaymentDate, @Amount)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeaseID", payment.LeaseID);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new Exception("Failed to insert payment.");
                    }
                }
            }

            Console.WriteLine("Payment added successfully.");
        }

        public Customer FindCustomerById(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM Customer WHERE CustomerID = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", customerID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = (int)reader["CustomerID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            };
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"Customer with ID {customerID} not found.");
                        }
                    }
                }
            }
        }


    }
}