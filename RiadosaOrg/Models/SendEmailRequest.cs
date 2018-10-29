using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiadosaOrg.Models
{
    public class SendEmailRequest
    {
        public string ToName { get; set; }
        public string ToAddress { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}