using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /Topic/

        public ActionResult Index(int id)
        {

            return View();
        }

        public ActionResult Add(int id)
        {

            return View();
        }

    }
}
