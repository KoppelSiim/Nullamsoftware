using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nullamsoftware.Models;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.SqlTypes;



namespace Nullamsoftware.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       

        [NonAction]
        public List<EventModel> ListOfEvents(string sqlproc)
        {
           // Console.Out.WriteLine(linkid);
            List<EventModel> eventlist = new List<EventModel>();
            SqlCommand com = new SqlCommand(sqlproc, con);
            con.Open();

            eventlist.Clear();

            using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
                {
                    eventlist.Add(new EventModel { Yritusenimi = rdr["Yritusenimi"].ToString(), Toimumisaeg = (DateTime)rdr["Toimumisaeg"] });
                }
            con.Close();

            return eventlist;

        }
        
        [NonAction]
        public List<EventModel> EventData()
        {
                
                List<EventModel> evlist = new List<EventModel>();
           
                SqlCommand com = new SqlCommand("SELECT * FROM (SELECT TOP 5 Id,Yritusenimi,Toimumisaeg,Koht,Lisainfo, ROW_NUMBER() OVER(ORDER BY Toimumisaeg) AS ROW FROM dbo.Eventlist WHERE Toimumisaeg > cast(sysdatetime() as date) ORDER BY Toimumisaeg) AS TMP ", con);
                con.Open();
                evlist.Clear();

                using (SqlDataReader rdr = com.ExecuteReader())
                    while (rdr.Read())
                    {
                        evlist.Add(new EventModel
                        {
                            Id = (int)rdr["Id"],
                            Yritusenimi = rdr["Yritusenimi"].ToString(),
                            Toimumisaeg = (DateTime)rdr["Toimumisaeg"],
                            Koht = rdr["Koht"].ToString(),
                            Lisainfo = rdr["Lisainfo"].ToString()
                        });
                    }
                con.Close();
            
                return evlist;
            
        }

        [NonAction]
        public EventModel EventDataById (string id)
        {
            System.Diagnostics.Debug.WriteLine("EventdatabyId: " + id);
            EventModel evmodel=new EventModel();

            SqlCommand com = new SqlCommand("SELECT * FROM (SELECT TOP 5 Id,Yritusenimi,Toimumisaeg,Koht,Lisainfo, ROW_NUMBER() OVER(ORDER BY Toimumisaeg) AS ROW FROM dbo.Eventlist WHERE Toimumisaeg > cast(sysdatetime() as date) ORDER BY Toimumisaeg) AS TMP WHERE ROW = @Id", con);
            com.Parameters.Add(new SqlParameter("Id", id));
            con.Open();
            

            using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
                {
                    System.Diagnostics.Debug.WriteLine("Leidsime: " + id);
                    evmodel = new EventModel()
                    {
                        Id = (int)rdr["Id"],
                        Yritusenimi = rdr["Yritusenimi"].ToString(),
                        Toimumisaeg = (DateTime)rdr["Toimumisaeg"],
                        Koht = rdr["Koht"].ToString(),
                        Lisainfo = rdr["Lisainfo"].ToString()
                    };
                }
            con.Close();
            System.Diagnostics.Debug.WriteLine("Leidsime evmodel: " + evmodel);
            return evmodel;

        }




        /*
        [HttpGet]
        public JsonResult ReturnEventData()
        {
            return Json(EventDataById());
        }
        */
        [HttpGet]
        public JsonResult ReturnEventList(string sqlproc)
        {
            sqlproc = "SELECT TOP 5 Yritusenimi,Toimumisaeg FROM dbo.Eventlist WHERE Toimumisaeg > cast(sysdatetime() as date) ORDER BY Toimumisaeg";
            return Json(ListOfEvents(sqlproc));
        }
        [HttpGet]
        public JsonResult ReturnPastEventList(string sqlproc)
        {
            sqlproc = "SELECT TOP 5 Yritusenimi,Toimumisaeg FROM dbo.Eventlist WHERE Toimumisaeg < cast(sysdatetime() as date) ORDER BY Toimumisaeg";
            return Json(ListOfEvents(sqlproc));
        }


        //SELLEGA SIIN ON VAJA TEGELEDA!!!!!!!!!!!!!!!!!!!!!!!!!! uus fn juurde andmete jaoks?

        [HttpPost]
        public JsonResult GetLinkId(string id)
        {
            System.Diagnostics.Debug.WriteLine("Mul on vaja seda: " + id);
           
            System.Diagnostics.Debug.WriteLine(EventDataById(id));
            return Json(EventDataById(id));
           // System.Diagnostics.Debug.WriteLine("Mul on vaja seda: "+ lm.Linkid);

           
        }
        
        public IActionResult Participants()
        {
          
            return View();
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


        ParticipantDb pdb = new ParticipantDb();
        [HttpPost]
        public IActionResult Participants([Bind]ParticipantModel pm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string res = pdb.ParticipantDbUpdate(pm);
                    TempData["msg"] = res;
                }
            }

            catch (Exception exp)
            {
                TempData["msg"] = exp.Message;
            }
            return View();
            
        }
        

        
    }
}



