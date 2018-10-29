using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class MusicController : BaseController
    {
        // GET: Music
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Visuals()
        {
            return View();
        }
    }
}