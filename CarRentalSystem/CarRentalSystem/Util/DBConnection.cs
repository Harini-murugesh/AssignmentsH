using System.Data.SqlClient;

namespace CarRentalSystem.util
{
    public class DBConnection
    {
        private static readonly string connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=CarRentalSystem;Integrated Security=True";

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}
