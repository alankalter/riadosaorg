﻿using Microsoft.VisualBasic.FileIO;
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
            var data = new DataProvider();
            var shows = data.GetShows(Server.MapPath("~/shows.csv"));
            return View(new Shows { Future = shows.Where(x=>x.Date > DateTime.Now).OrderBy(x => x.Date),
                Past = shows.Where(x => x.Date < DateTime.Now).OrderByDescending(x => x.Date)
            });
        }
        
    }
}