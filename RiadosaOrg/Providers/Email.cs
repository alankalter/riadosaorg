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
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}



