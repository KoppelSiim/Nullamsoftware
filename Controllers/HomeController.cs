using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nullamsoftware.Models;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;



namespace Nullamsoftware.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       
        [NonAction]
        public List<EventModel> ListOfEvents(string sqlproc)
         {
            List<EventModel> eventlist = new List<EventModel>();
            SqlCommand com = new SqlCommand(sqlproc, con);
            con.Open();

            eventlist.Clear();
            
         using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
            {
                eventlist.Add(new EventModel {Yritusenimi = rdr["Yritusenimi"].ToString(), Toimumisaeg = (DateTime)rdr["Toimumisaeg"]});
            }
            con.Close();
            
            return eventlist;

        }

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

        /*
        [NonAction]
        public List<EventModel> PList()
        {
            List<EventModel> evtlist = new List<EventModel>();
            SqlCommand com = new SqlCommand("get_event_id", con);
            con.Open();
            evtlist.Clear();

            using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
                {
                    evtlist.Add(item: new EventModel { Toimumisaeg = (DateTime)rdr["Toimumisaeg"], Koht = rdr["Koht"].ToString() });
                }
            con.Close();
            return evtlist;

        }
        [HttpGet]
        public JsonResult ReturnPList()
        {
            return Json(PList());
        }
        
        /* Siia tuleb fn juurde teha
        [NonAction]
        public List<ParticipantModel> ListOfParticipants()
        {
            List<ParticipantModel> plist = new List<ParticipantModel>();

        }
        */


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



