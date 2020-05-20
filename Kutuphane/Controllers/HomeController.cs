using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kutuphane.Context;
using Kutuphane.Filters;
using Kutuphane.Models;
using Kutuphane.Session;

namespace Kutuphane.Controllers
{
    [UserAuth]
    public class HomeController : Controller
    {
        private Kutuphane.Context.Context db = new Kutuphane.Context.Context();
        private StandardUser currentUser;

        public HomeController()
        {
            currentUser = CurrentSession.getCurrentUser();
        }

        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}