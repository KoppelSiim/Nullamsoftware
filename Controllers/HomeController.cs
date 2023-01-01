using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nullamsoftware.Models;
using System.Data.SqlClient;


namespace Nullamsoftware.Controllers
{

    public class HomeController : Controller
    {   // Connection string for all database connections
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Nullamdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       
        // Ideally, all my database operations should be using the models class with no sql in the controller, but right now, i am using this approuch that is not entirely correct

        // This function returns list of all the future events
        [NonAction]
        //We will return a list of the events and the data type is our eventmodel, we take the sql querystring as parameter
        public List<EventModel> ListOfEvents(string sqlproc)
        {
            
            List<EventModel> eventlist = new List<EventModel>();
            // Form the command and open the connection
            SqlCommand com = new SqlCommand(sqlproc, con);
            con.Open();
            // Clear the list 
            eventlist.Clear();
            // We use the datareader to execute the command
            using (SqlDataReader rdr = com.ExecuteReader())
                // Run the while loop if there are rows to read
                while (rdr.Read())
                {   // Add all the events to the list
                    eventlist.Add(new EventModel { Yritusenimi = rdr["Yritusenimi"].ToString(), Toimumisaeg = (DateTime)rdr["Toimumisaeg"] });
                }
            // Close the connection
            con.Close();
            // Return our list with events
            return eventlist;
        }
        // This function gathers the specifics events data, we use the linkid parameter to display the correct event
        [NonAction]
        public EventModel EventDataById(string linkid)
        {
            // Create an empty model
            EventModel evmodel = new EventModel();
            // We select the top 5 events and order them by the date in ascending order. Then we use the linkid to select the correct event.
            SqlCommand com = new SqlCommand("SELECT * FROM (SELECT TOP 5 Id,Yritusenimi,Toimumisaeg,Koht,Lisainfo, ROW_NUMBER() OVER(ORDER BY Toimumisaeg) AS ROW FROM dbo.Eventlist WHERE Toimumisaeg > cast(sysdatetime() as date) ORDER BY Toimumisaeg) AS TMP WHERE ROW = @Row", con);
            com.Parameters.Add(new SqlParameter("@Row", linkid));
            // Open the connection
            con.Open();

            using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
                {
                    // We store the event Id as a global so it can be accessed later by other functions
                    MyGlobalVariables.GlobalEventId = (int)rdr["Id"];
                    // Fill the model with the specifics events data that was requested by the user
                    evmodel = new EventModel()
                    {
                        Id = (int)rdr["Id"],
                        Yritusenimi = rdr["Yritusenimi"].ToString(),
                        Toimumisaeg = (DateTime)rdr["Toimumisaeg"],
                        Koht = rdr["Koht"].ToString(),
                        Lisainfo = rdr["Lisainfo"].ToString()
                    };
                }
            // Close the connection and return the data
            con.Close();
            return evmodel;

        }
        // This function returns a list of all the event participants
        [NonAction]
        public List<ParticipantModel> ReturnParticipants()
        {
            
            List<ParticipantModel> plist = new List<ParticipantModel>();
            // We use the global event Id to match the selected event with it's participants
            SqlCommand com = new SqlCommand("SELECT Eesnimi, Perenimi, Isikukood FROM dbo.Participants WHERE Fk_Participant = @EventId",con);
            // Add the event Id into the query string
            com.Parameters.Add(new SqlParameter("@EventId", MyGlobalVariables.GlobalEventId));
            con.Open();
            plist.Clear();
            // Use datareader to read the data and add it to the participants list
            using (SqlDataReader rdr = com.ExecuteReader())
                while (rdr.Read())
                {
                    plist.Add(new ParticipantModel
                    {
                        Eesnimi = rdr["Eesnimi"].ToString(),
                        Perenimi = rdr["Perenimi"].ToString(),
                        Isikukood = rdr["Isikukood"].ToString()
                    });
                }
            // Close the connection and return the data
            con.Close();
            return plist;
        }

        //We use Http get request to communicate with the controllers functions via Javascript
        [HttpGet]
        // We convert the participants list to Json format so we can access it with our front end
        public JsonResult ReturnPList()
        {
            return Json(ReturnParticipants());
        }
        // Convert the future events into Json format
        [HttpGet]
        public JsonResult ReturnEventList(string sqlproc)
        {
           
            sqlproc = "SELECT TOP 5 Yritusenimi,Toimumisaeg FROM dbo.Eventlist WHERE Toimumisaeg > cast(sysdatetime() as date) ORDER BY Toimumisaeg";
            return Json(ListOfEvents(sqlproc));
        }
        // Convert the past events into Json format
        [HttpGet]
        public JsonResult ReturnPastEventList(string sqlproc)
        {
            sqlproc = "SELECT TOP 5 Yritusenimi,Toimumisaeg FROM dbo.Eventlist WHERE Toimumisaeg < cast(sysdatetime() as date) ORDER BY Toimumisaeg";
            return Json(ListOfEvents(sqlproc));
        }

        // We use HttpPost the get the correct link
        [HttpPost]
        public JsonResult GetLinkId(string linkid)
        {
            return Json(EventDataById(linkid));
        }
        
        // Return the participants page
        public IActionResult Participants()
        {
            return View();
        }

        // Return home page
        public IActionResult Index()
        {
            return View();
        }
        // Return Error messages
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // Return the addevent page
        public IActionResult Addevent()
        {
            return View();
        }
        // We create the EventDb object that contains all our database operations for events
        EventDb edb = new EventDb();
        //HttpPost gets the data from front end asp form
        [HttpPost]
        //Bind the events to the Eventmodel
        public IActionResult Addevent([Bind]EventModel emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Insert eventdata to database
                    string res = edb.DbUpdate(emp);
                    // Store success message
                    TempData["msg"] = res;
                }
            }
            catch(Exception exp)
            {   //Store error message
                TempData["msg"] = exp.Message;
            }
            
            return View();
        }
        // Create the participant database object that contains our database methods for adding participants
        ParticipantDb pdb = new ParticipantDb();
        [HttpPost]
        public IActionResult Participants([Bind]ParticipantModel pm)
        {
            try
            {
                if (ModelState.IsValid)
                {   //Add participants and store success message
                    string res = pdb.ParticipantDbUpdate(pm);
                    TempData["msg"] = res;
                }
            }

            catch (Exception exp)

            {   //If something goes wrong store exception message
                TempData["msg"] = exp.Message;
            }
            return View();
            
        }
       
    }
}



