
using System.Data;
using System.Data.SqlClient;

namespace Nullamsoftware.Models
{
    public class ParticipantDb
    {
       SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public string ParticipantDbUpdate(ParticipantModel ptm)
        {
            try
            {
                SqlCommand com = new SqlCommand("db_add_participant", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Isikut", ptm.Isikut);
                com.Parameters.AddWithValue("@Eesnimi", ptm.Eesnimi);
                com.Parameters.AddWithValue("@Perenimi", ptm.Perenimi);
                com.Parameters.AddWithValue("@Isikukood", ptm.Isikukood);
                com.Parameters.AddWithValue("@Maksmisviis", ptm.Maksmisviis);
                com.Parameters.AddWithValue("@Lisainfoisik", ptm.Lisainfoisik);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("Andmebaas edukalt uuendatud, osaleja lisatud");
            }
            catch(Exception exep)
            {
               if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return (exep.Message.ToString());
            }
            
        }
    }
}
