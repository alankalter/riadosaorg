using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RiadosaOrg.Models
{
    public class Listing
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string AuxField { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }        
        public DateTime Date { get; set; }
    }
}