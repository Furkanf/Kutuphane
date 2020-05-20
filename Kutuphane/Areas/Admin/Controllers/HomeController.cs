using Kutuphane.Filters;
using Kutuphane.Models;
using Kutuphane.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Kutuphane.Areas.Admin.Controllers
{
    [AdminAuth]
    public class HomeController : Controller
    {
        private AdminUser currentAdmin;
        private Kutuphane.Context.Context db = new Kutuphane.Context.Context();

        public HomeController()
        {
            currentAdmin = CurrentSession.getCurrentAdmin();
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
    
            return View();
        }

        public ActionResult SetTime(int? day)
        {
            if(day==null || day > 100 || day < -100)
            {
                ViewBag.error = "Hatali sayi girdiniz.";
                return View("error");
            }
            Kutuphane.Date.Date.date = Kutuphane.Date.Date.date.AddDays(Convert.ToDouble(day));

            return View("Index");
        }
    }
}