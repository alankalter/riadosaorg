using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RiadosaOrg.Models
{
    public class MailingListRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string emailAddress { get; set; }
    }
}