using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RiadosaOrg.Helpers
{
    public class AppSettings
    {
        public static string EmailPass => ConfigurationManager.AppSettings["EmailPass"];
        public static string EmailFromAddress => ConfigurationManager.AppSettings["EmailFromAddress"];
    }
}