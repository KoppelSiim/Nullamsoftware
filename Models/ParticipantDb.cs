
using System.Data;
using System.Data.SqlClient;

namespace Nullamsoftware.Models
{
    public class ParticipantDb
    {   // Connection string
       SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public string ParticipantDbUpdate(ParticipantModel ptm)
        {
            try
            {   // Add data with value using the participant model
                SqlCommand com = new SqlCommand("INSERT INTO dbo.Participants (Isikut, Eesnimi, Perenimi, Isikukood, Maksmisviis, Lisainfoisik, Fk_Participant) VALUES(@Isikut, @Eesnimi, @Perenimi, @Isikukood, @Maksmisviis, @Lisainfoisik, @Fk_Participant)", con);
                com.Parameters.AddWithValue("@Isikut", ptm.Isikut);
                com.Parameters.AddWithValue("@Eesnimi", ptm.Eesnimi);
                com.Parameters.AddWithValue("@Perenimi", ptm.Perenimi);
                com.Parameters.AddWithValue("@Isikukood", ptm.Isikukood);
                com.Parameters.AddWithValue("@Maksmisviis", ptm.Maksmisviis);
                com.Parameters.AddWithValue("@Lisainfoisik", ptm.Lisainfoisik);
                com.Parameters.AddWithValue("@Fk_Participant", ptm.Fk_Participant);
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
