using RiadosaOrg.Helpers;
using RiadosaOrg.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace RiadosaOrg.Providers
{
    public class EmailProvider
    {
        public bool SendEmail(SendEmailRequest request)
        {
            try
            {
                var fromAddress = new MailAddress(AppSettings.EmailFromAddress, "RiadosaOrg");
                var toAddress = new MailAddress(request.ToAddress, request.ToName);
                string fromPassword = AppSettings.EmailPass;
                string subject = request.Subject;
                string body = request.Body;

                var smtp = new SmtpClient
                {
                    Host = "relay-hosting.secureserver.net",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true
                    //Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }





                //MailMessage message = new MailMessage();
                //message.From = new MailAddress("riadosaorg@gmail.com");

                //message.To.Add(new MailAddress("trevorcwilliams@gmail.com"));

                //message.Subject = request.Subject;
                //message.Body = "content of your email";

                //SmtpClient client = new SmtpClient();
                //client.Send(message);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}



