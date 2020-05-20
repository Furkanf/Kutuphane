using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kutuphane.Context;
using Kutuphane.Filters;
using Kutuphane.Models;
using Kutuphane.Session;


//expection filter kullan
namespace Kutuphane.Controllers
{
    [UserAuth]
    public class BooksController : Controller
    {
        Kutuphane.Context.Context db = new Kutuphane.Context.Context();
        private StandardUser currentUser;

        public BooksController()
        {
            currentUser = currentUser = CurrentSession.getCurrentUser();
        }
        // GET: Books
        public ActionResult Index(string query)
        {
            if(query==null || query.Length == 0 )
            {

                var books = db.books.Include(b => b.userMap);
                return View(books.ToList());
            }
            else
            {
                var books = db.books.Include(b => b.userMap).Where(x => x.Name.ToLower().Contains(query.ToLower()) || x.Isbn.ToLower().Contains(query.ToLower())).ToList();
                ViewBag.query = query;
                return View(books.ToList());
            }
         
        }

        public ActionResult MyBooks()
        {
            var books = db.books.Include(b => b.userMap).Where(x=>x.userMap.userId==currentUser.Id);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                ViewBag.error = "Isbn no girilmedi.";
                return View("Error");
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                ViewBag.error = "Kitap bulunamadı";
                return View("Error");
            }
            return View(book);
        }

        public ActionResult Borrow(string id)
        {
            currentUser = db.standardUsers.Find(currentUser.Id);
            if (id == null)
            {
                ViewBag.error = "Isbn no girilmedi";
                return View("Error");
            }

            Book book = db.books.Find(id);

            if (book == null)
            {
                ViewBag.error = "Kitap bulunamadı";
                return View("Error");
            }
            if (book.userMap != null)
            {
                ViewBag.error = "Bu kitap teslim alınmış.";
                return View("Error");
            }

            List<Book> myBooks = db.books.Include(a=>a.userMap).Where(x => x.userMap.userId == currentUser.Id).ToList();

            if(myBooks.Count>=3)
            {
                ViewBag.error = "Bir kullanıcı 3'ten fazla sayıda kitap alamaz.";
                return View("Error");
            }

            if( myBooks.Where(x => x.userMap.deliveryDate < Kutuphane.Date.Date.date).ToList().Count>0)
            {
                ViewBag.error = "Teslim süren geçmiş kitapların var. Önce onları teslim et.";
                return View("Error");
            }


            UserBookMap willBeAdded = new UserBookMap();
            willBeAdded.book=book;
            willBeAdded.deliveryDate = Kutuphane.Date.Date.date.AddDays(7);
            willBeAdded.Isbn = book.Isbn;
            willBeAdded.user = currentUser;
            willBeAdded.userId = currentUser.Id;
            db.userBookMaps.Add(willBeAdded);
            db.SaveChanges();



            return RedirectToAction("MyBooks");
        }

        
        public ActionResult Deliver(string id)
        {
            currentUser = db.standardUsers.Find(currentUser.Id);
            if (id == null)
            {
                ViewBag.error = "Isbn no girilmedi";
                return View("Error");
            }
            Book book = db.books.Find(id);
            if (book == null )
            {
                ViewBag.error = "Kitap bulunamadı";
                return View("Error");
                
            }
            if(book.userMap == null)
            {
                ViewBag.error = "Kitap kütüphanemizdedir.";
                return View("Error");
            }
             if(book.userMap.userId!=currentUser.Id)
            {
                ViewBag.error = "Başka kişinin üzerinde olan kitabı sen teslim edemezsin";
                return View("Error");
            }
            
             else
             {
                UserBookMap willBeDeleted = db.userBookMaps.FirstOrDefault(a => a.Isbn == id && a.userId == currentUser.Id);
                   if(willBeDeleted!=null)
                {
                    db.userBookMaps.Remove(willBeDeleted);
                    db.SaveChanges();
                }
                    
             }
            return RedirectToAction("MyBooks");
        }

        public ActionResult DeliverBook()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase book)
        {
            HttpPostedFileBase bookImage = null;
            if (Request.Files.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            else
            {
                bookImage = Request.Files[0];
            }
            if (bookImage != null && (bookImage.ContentType == "image/jpeg" || bookImage.ContentType == "image/jpg" || bookImage.ContentType == "image/png"))
            {
                string fileName = bookImage.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromStream(bookImage.InputStream);
                Bitmap bmp = new Bitmap(image);
                string result = Ocr.Doit(bmp).Trim();
                if (result == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return Json(new { isbn = result }, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
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
