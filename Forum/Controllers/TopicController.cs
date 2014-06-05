using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class TopicController : Controller
    {
        public ActionResult Index(int id)
        {
            Topic topic = Topic.GetTopic(id);

            if (topic == null)
            {
                return View("NotFound");
            }
            else if (!topic.HasAccess())
            {
                return View("NoAccess");
            }

            topic.Read();

            return View(topic);
        }

        [HttpPost]
        public ActionResult Index(int id, MessageViewModel model)
        {
            Topic topic = Topic.GetTopic(id);

            if (topic == null)
            {
                return View("NotFound");
            }
            else if (!Current.IsLoggedIn || !topic.HasAccess())
            {
                return View("NoAccess");
            }
            else if (ModelState.IsValid)
            {
                topic.AddMessage(new Message(model.Text, DateTime.Now, Current.AccountId));
            }

            topic.Read();

            return View(topic);
        }

        [ChildActionOnly]
        public ActionResult Add(int id)
        {
            if (Current.IsLoggedIn)
            {
                return PartialView();
            }
            else
            {
                return null;
            }
        }
    }
}
