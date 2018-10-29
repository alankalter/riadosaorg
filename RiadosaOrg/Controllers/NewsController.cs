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
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index()
        {
            var data = new DataProvider();
            var blogs = data.GetBlogs(Server.MapPath("~/blogs.csv")).OrderByDescending(x => x.Date);

            return View(blogs);
        }        
    }
}