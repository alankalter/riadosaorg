using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiadosaOrg.Models
{
    public class Shows
    {
        public IEnumerable<Event> Future { get; set; }
        public IEnumerable<Event> Past { get; set; }
    }
}