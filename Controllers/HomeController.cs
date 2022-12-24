using Microsoft.AspNetCore.Mvc;
using Nullamsoftware.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Nullamsoftware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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



