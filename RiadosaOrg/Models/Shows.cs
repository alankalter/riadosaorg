using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiadosaOrg.Models
{
    public class Shows
    {
        public IEnumerable<Show> Future { get; set; }
        public IEnumerable<Show> Past { get; set; }
    }
}