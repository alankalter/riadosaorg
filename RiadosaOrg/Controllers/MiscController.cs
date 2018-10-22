using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class MiscController : BaseController
    {
        public ActionResult About()
        {
            return View();
        }
    }
}