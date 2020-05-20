using Kutuphane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kutuphane.Controllers
{
    public class SecurityController : Controller
    {
        private Kutuphane.Context.Context db = new Kutuphane.Context.Context();
        
        // GET: Admin/Security
        [HttpGet]
        public ActionResult Login()
        {

            return View(new StandardUser());
        }

        [HttpPost]
        public ActionResult Login(StandardUser model)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Surname");
            if (ModelState.IsValid)
            {
                StandardUser user = db.standardUsers.Find(model.Id);
                if (user == null) {
                    ModelState.AddModelError("Id", "Kullanıcı bulunamadı.");
                    return View(model);
                }
                if (model.Password.Equals(user.Password))
                {
                    Session.Add("user", user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Parola Hatalıdır");
                return View(model);
            }

            return View(model);

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}