// task11/util/DBUtil.cs
// ========================
using System.Data.SqlClient;

namespace task11.util
{
    public class DBUtil
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=TicketBookingSystem;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;

        }
    }
}
