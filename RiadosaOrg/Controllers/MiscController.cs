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
            

            using (var data = new revocarr_RiadosaOrgEntities())
            {
                var existing = data.Contacts.Where(x => x.Email == request.emailAddress).SingleOrDefault();

                if (existing != null)
                {
                    ModelState.Clear();
                    return View("MailingList", new MailingList { SuccessMessage = $"{request.emailAddress} saved." });
                }

                data.Contacts.Add(new Contact
                {
                    Email = request.emailAddress,
                    DateAdded = DateTime.Now
                });
                data.SaveChanges();

            }

            var email = new EmailProvider();
            email.SendEmail(new SendEmailRequest { ToAddress = "riadosaorg@gmail.com", ToName = "Chief", Subject = "MailingList Submission", Body = $"New email address for the MailingList: {request.emailAddress}" });

            var model = new MailingList { SuccessMessage = $"{request.emailAddress} saved. Thanks for subscribing." };
            ModelState.Clear();
            return View("MailingList", model);
            
        }
    }
}