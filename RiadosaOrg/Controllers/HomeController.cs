using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Riadosa.org";

            return View();
        }
        public ActionResult Shows()
        {
            ViewBag.Title = "Riadosa.org";

            return View();
        }
    }
}
