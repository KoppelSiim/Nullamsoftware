
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Nullamsoftware.Models;
using System;
using System.Configuration;

namespace Nullamsoftware.Models
{
/*
    public class DisplayEvents
    {

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public string[] DispEvents()
        {
            con.Open();
            string[] resarray = Array.Empty<string>();
            SqlCommand com = new SqlCommand("displplay_events", con);
            SqlDataReader rdr = com.ExecuteReader();
            while (rdr.Read())
            {
                //This "for" iterates through each column of the current row!
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                   resarray[i]=rdr.GetValue(i).ToString();
                }
            }
            con.Close();
            return resarray;
        }
    }*/
}
