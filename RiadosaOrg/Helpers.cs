using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RiadosaOrg
{
    public static class Helpers
    {
        public static string GetRandomBgImage()
        {
            var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~\\Content\\Bg"));
            var random = new Random();
            var count = files.Count();
            var filenum = random.Next(0, count);

            FileInfo info = new FileInfo(files[filenum]);
            return "..//Content//bg//" + info.Name;
        }
    }
}