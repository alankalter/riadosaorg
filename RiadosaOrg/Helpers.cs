using RiadosaOrg.Models;
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
        public static CSSNumbers GetCSSNumbers()
        {
            var rando = new Random();
            var color1 = rando.Next(5, 100).ToString();

            return new CSSNumbers
            {
                NavRotate = (rando.Next(-10, 10) * .1).ToString(),
                BodyRotate = (rando.Next(-2, 10) * .1).ToString(),
                FooterRotate = (rando.Next(-10, 10) * .1).ToString(),
                Link1Rotate = (rando.Next(-20, 20) * .1).ToString(),
                Link2Rotate = (rando.Next(-20, 20) * .1).ToString(),
                Link3Rotate = (rando.Next(-20, 20) * .1).ToString(),
                Link4Rotate = (rando.Next(-20, 20) * .1).ToString(),
                BorderWidth = $"{rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px",
                BorderWidth2 = $"{rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px",
                BorderWidth3 = $"{rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px {rando.Next(1, 20).ToString()}px",
                BorderColor = $"rgb({color1}, {color1}, {color1})"
            };
        }

    }
}