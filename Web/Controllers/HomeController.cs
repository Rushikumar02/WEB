using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

       private DatabaseEntities context=new DatabaseEntities();

        public ActionResult HomePage()
        {
            return View();
        }
    }
}
