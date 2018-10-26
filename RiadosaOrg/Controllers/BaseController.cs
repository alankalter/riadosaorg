using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {
            ViewBag.BgUrl = WebApiConfig.BackgroundImage;
            var rando = new Random();

            ViewBag.NavRotate = WebApiConfig.CSSNumbers.NavRotate;
            ViewBag.BodyRotate = WebApiConfig.CSSNumbers.BodyRotate;
            ViewBag.FooterRotate = WebApiConfig.CSSNumbers.FooterRotate;
            ViewBag.Link1Rotate = WebApiConfig.CSSNumbers.Link1Rotate;
            ViewBag.Link2Rotate = WebApiConfig.CSSNumbers.Link2Rotate;
            ViewBag.Link3Rotate = WebApiConfig.CSSNumbers.Link3Rotate;
            ViewBag.Link4Rotate = WebApiConfig.CSSNumbers.Link4Rotate;
            ViewBag.BorderWidth = WebApiConfig.CSSNumbers.BorderWidth;
            ViewBag.BorderWidth2 = WebApiConfig.CSSNumbers.BorderWidth2;
            ViewBag.BorderWidth3 = WebApiConfig.CSSNumbers.BorderWidth3;
            ViewBag.BorderWidth4 = WebApiConfig.CSSNumbers.BorderWidth4;
            ViewBag.BorderColor = WebApiConfig.CSSNumbers.BorderColor;
        }

    }
}