
using System.Data;
using System.Data.SqlClient;

namespace Nullamsoftware.Models
{
    public class EventDb
    {  // Database connection string
       SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        //Method for adding events using the events datamodel
    public string DbUpdate(EventModel emp)
        {
            try
            {   // Start the procedure for adding events
                SqlCommand com = new SqlCommand("db_add_event", con);
                com.CommandType = CommandType.StoredProcedure;
                // Add the values that correspond with our eventmodel
                com.Parameters.AddWithValue("@Yritusenimimi", emp.Yritusenimi);
                com.Parameters.AddWithValue("@Toimumisaeg", emp.Toimumisaeg);
                com.Parameters.AddWithValue("@Koht", emp.Koht);
                com.Parameters.AddWithValue("@Lisainfo", emp.Lisainfo);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("Andmebaas edukalt uuendatud, yritus lisatud");
            }
            catch (Exception exep)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (exep.Message.ToString());

            }
        }
        
    }
}
