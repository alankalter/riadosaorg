using Microsoft.VisualBasic.FileIO;
using RiadosaOrg.Models;
using RiadosaOrg.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class ShowsController : BaseController
    {
        // GET: Shows
        public ActionResult Index()
        {
            List<Event> shows;
            using (var data = new revocarr_RiadosaOrgEntities())
            {
                shows = data.Events.Select(x => x).OrderByDescending(x => x.Date).ToList();
            }

            return View(new Shows { Future = shows.Where(x=>x.Date > DateTime.Now), Past = shows.Where(x => x.Date < DateTime.Now)  });
        }
        
    }
}