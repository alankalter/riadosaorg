﻿using System;
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
        }
       
    }
}