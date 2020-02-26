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

            //return View(new Shows
            //{
            //    Future = shows.Where(x => x.Date > DateTime.Now.AddDays(-1)).OrderBy(x => x.Date), Past = shows.Where(x => x.Date < DateTime.Now.AddDays(-1))  });
            return View(shows);
        }
        [Authorize]
        [HttpPost]
        public string UpdateEvent(Event show)
        {
            try
            {
                using (var data = new revocarr_RiadosaOrgEntities())
                {
                    var showForUpdate = data.Events.Where(x => x.EventId == show.EventId).FirstOrDefault();

                    if (showForUpdate == null)
                        throw new Exception("Event not found");

                    showForUpdate.Venue = show.Venue;
                    showForUpdate.Time = show.Time;
                    showForUpdate.Date = show.Date;
                    showForUpdate.Info = show.Info;
                    showForUpdate.Url = show.Url;
                    showForUpdate.Location = show.Location;

                    data.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost]
        public string CreateEvent(Event show)
        {
            try
            {
                using (var data = new revocarr_RiadosaOrgEntities())
                {
                    data.Events.Add(show);
                    data.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost]
        public string DeleteEvent(Event show)
        {
            try
            {
                using (var data = new revocarr_RiadosaOrgEntities())
                {
                    var showToDelete = data.Events.Where(x => x.EventId == show.EventId).Single();
                    data.Events.Remove(showToDelete);
                    data.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}