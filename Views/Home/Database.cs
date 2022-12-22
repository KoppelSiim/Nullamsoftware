// Database actions


using System.Data;
using System.Data.SqlClient;

namespace Nullamsoftware.Views.Home
{
    public class Database
    {
        public static void Connectdb()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Use the connection here
                connection.Close();
            }
            
        }
    }
}

