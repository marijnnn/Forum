using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index(int id)
        {
            Category category = Category.GetCategory(id);

            if (category == null)
            {
                return View("NotFound");
            }
            else if (!category.HasAccess())
            {
                return View("NoAccess");
            }

            ViewData["CategoryId"] = id;

            return View(Category.GetCategory(id));
        }

        public ActionResult Add(int id)
        {
            Category category = Category.GetCategory(id);

            if (category == null)
            {
                return View("NotFound");
            }
            else if (!Current.IsLoggedIn || !category.HasAccess())
            {
                return View("NoAccess");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Add(int id, TopicModel model)
        {
            Category category = Category.GetCategory(id);

            if (category == null)
            {
                return View("NotFound");
            }
            else if (!Current.IsLoggedIn || !category.HasAccess())
            {
                return View("NoAccess");
            }
            else if (ModelState.IsValid)
            {
                Topic topic = new Topic(model.Title, Current.AccountId);
                category.AddTopic(topic, new Message(model.Text, DateTime.Now, Current.AccountId));
                return RedirectToAction("Index", "Topic", new { id=topic.Id });
            }

            return View(model);
        }

        public ActionResult MarkAsRead(int id)
        {
            Category category = Category.GetCategory(id);

            if (category == null)
            {
                return View("NotFound");
            }
            else if (!Current.IsLoggedIn || !category.HasAccess())
            {
                return View("NoAccess");
            }

            category.MarkAsRead();

            return RedirectToAction("Index", "Category", new { id = category.Id });
        }
    }
}
