using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kutuphane.Context;
using Kutuphane.Filters;
using Kutuphane.Models;
using Kutuphane.Session;

namespace Kutuphane.Areas.Admin.Controllers
{
    [AdminAuth]
    public class StandardUsersController : Controller
    {
        private AdminUser currentAdmin;
        private Kutuphane.Context.Context db = new Kutuphane.Context.Context();

        public StandardUsersController()
        {
            currentAdmin = CurrentSession.getCurrentAdmin();
        }
        // GET: Admin/StandardUsers
        public ActionResult Index()
        {
            List<StandardUser> users = currentAdmin.GetUsers();
            return View(users);
        }

        // GET: Admin/StandardUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                ViewBag.error = "User id girilmedi.";
                return View("Error");
            }
            StandardUser standardUser = currentAdmin.GetUserDetail(id);
            if (standardUser == null)
            {
                ViewBag.error = "User bulunamadı";
                return View("Error");
            }
            return View(standardUser);
        }

        // GET: Admin/StandardUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/StandardUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Password,Name,Surname")] StandardUser standardUser)
        {
            if (ModelState.IsValid)
            {
                db.standardUsers.Add(standardUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(standardUser);
        }

        // GET: Admin/StandardUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                ViewBag.error = "User id girilmedi.";
                return View("Error");
            }
            StandardUser standardUser = db.standardUsers.Find(id);
            if (standardUser == null)
            {
                ViewBag.error = "User bulunamadı.";
                return View("Error");
            }
            return View(standardUser);
        }

        // POST: Admin/StandardUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Password,Name,Surname")] StandardUser standardUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standardUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(standardUser);
        }

        // GET: Admin/StandardUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                ViewBag.error = "User id girilmedi.";
                return View("Error");
            }
            StandardUser standardUser = db.standardUsers.Find(id);
            if (standardUser == null)
            {
                ViewBag.error = "User bulunamadı.";
                return View("Error");
            }
            return View(standardUser);
        }

        // POST: Admin/StandardUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                ViewBag.error = "User id girilmedi.";
                return View("Error");
            }
            StandardUser standardUser = db.standardUsers.Find(id);
            if (standardUser == null)
            {
                ViewBag.error = "User bulunamadı.";
                return View("Error");
            }
            db.standardUsers.Remove(standardUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
