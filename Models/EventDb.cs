using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Nullamsoftware.Models;


namespace Nullamsoftware.Models
{
    public class EventDb
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
    public string DbUpdate(EventModel emp)
        {
            try
            {
                SqlCommand com = new SqlCommand("db_add_event", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Yritusenimimi", emp.Yritusenimi);
                com.Parameters.AddWithValue("@Toimumisaeg", emp.Toimumisaeg);
                com.Parameters.AddWithValue("@Koht", emp.Koht);
                com.Parameters.AddWithValue("@Lisainfo", emp.Lisainfo);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("Andmebaas edukalt uuendatud");
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
