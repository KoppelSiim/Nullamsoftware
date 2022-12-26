using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Nullamsoftware.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Nullamsoftware.Controllers
{
    public class HomeController : Controller
    {

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       
        public IActionResult ListOfEvents()
         {
            //SqlDataReader data;
            List<EventModel> eventlist = new List<EventModel>();
            SqlCommand com = new SqlCommand("get_future_events", con);
            con.Open();
            //data = com.ExecuteReader();
            eventlist.Clear();
            
         using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
            {
                eventlist.Add(new EventModel { Yritusenimi = rdr["Yritusenimi"].ToString(), Toimumisaeg = (DateTime)rdr["Toimumisaeg"] });
               
                
            }
            con.Close();
            return View(eventlist);
        }
        
        public IActionResult Index()
        {
            return View();
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        public IActionResult Addevent()
        {
            return View();
        }

        EventDb edb = new EventDb();
        [HttpPost]
        public IActionResult Addevent([Bind]EventModel emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string res = edb.DbUpdate(emp);
                    TempData["msg"] = res;
                }
            }
            catch(Exception exp)
            {
                TempData["msg"] = exp.Message;
            }


            return View();
        }
       
    }
}



