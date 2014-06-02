using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (Account.Login(model.Username, model.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "De gebruikersnaam en/of wachtwoord is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (Account.Register(new Account(model.Username, model.Password)))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "De gebruikersnaam is al in gebruik.");
                }
            }

            return View(model);
        }

        public ActionResult Menu()
        {
            return View(new MenuModel());
        }

        public ActionResult Logout()
        {
            Current.Logout();
            return RedirectToAction("Login");
        }
    }
}
