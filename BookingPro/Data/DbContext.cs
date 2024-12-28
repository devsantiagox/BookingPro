using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BookingPro.Data
{
    public class DbContext
    {
        private readonly string connectionString;

        public DbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
