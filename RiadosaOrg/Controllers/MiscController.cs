using RiadosaOrg.Models;
using RiadosaOrg.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class MiscController : BaseController
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult MailingList()
        {
            return View(new MailingList());
        }

        public ActionResult SubmitEmail(MailingListRequest request)
        {
            if (!ModelState.IsValid)
            {

                //write code to update student 

                return View("MailingList", new MailingList());
            }
            var data = new DataProvider();
            data.WriteToCsv(Server.MapPath(("~/mailinglist.csv")), 
                    new { date = DateTime.Now, emailAddress = request.emailAddress });

            var email = new EmailProvider();
            email.SendEmail(new SendEmailRequest { ToAddress = "riadosaorg@gmail.com", ToName = "Chief", Subject = "MailingList Submission", Body = $"New email address for the MailingList: {request.emailAddress}" });

            var model = new MailingList { SuccessMessage="Thanks for subscribing"};

            return View("MailingList", model);
        }
    }
}