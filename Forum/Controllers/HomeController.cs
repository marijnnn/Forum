﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(Forum.MainCategories);
        }

        public ActionResult MarkAsRead()
        {
            if (!Current.IsLoggedIn)
            {
                return View("NoAccess");
            }

            Forum.MarkAsRead();

            return RedirectToAction("Index", "Home");
        }
    }
}
