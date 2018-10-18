using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class HomeController : Controller
    {
        public const string title = "Riadosa Org ";
        public ActionResult Index()
        {
            ViewBag.Title = title;

            return View();
        }
        public ActionResult Shows()
        {
            ViewBag.Title = title + "- Shows";

            return View();
        }
    }
}
