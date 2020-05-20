using Kutuphane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kutuphane.Areas.Admin.Controllers
{
    public class SecurityController : Controller
    {
        private Kutuphane.Context.Context db = new Kutuphane.Context.Context();
        // GET: Admin/Security
        [HttpGet]
        public ActionResult Login()
        {

            return View(new AdminUser());
        }

        [HttpPost]
        public ActionResult Login(AdminUser model)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Surname");
            if(ModelState.IsValid)
            {
               AdminUser admin = db.adminUsers.Find(model.Id);
                if (admin == null) {
                    ModelState.AddModelError("Id", "Admin kaydı bulunamadı.");
                    return View(model);
                }
                if(model.Password.Equals(admin.Password))
                {
                    Session.Add("admin", admin);
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