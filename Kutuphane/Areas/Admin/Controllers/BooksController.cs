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

namespace Kutuphane.Areas.Admin.Controllers
{
    [AdminAuth]
    public class BooksController : Controller
    {
        private AdminUser currentAdmin;
        private Kutuphane.Context.Context db = new Kutuphane.Context.Context();

        public BooksController()
        {
            currentAdmin = CurrentSession.getCurrentAdmin();
        }
        // GET: Admin/Books
        public ActionResult Index()
        {
            var books = db.books;
            return View(books.ToList());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                ViewBag.error = "Isbn numarası girilmedi.";
                return View("Error");
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                ViewBag.error = "Isbn numaralı kitap bulunamadı.";
                return View("Error");
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            ViewBag.Isbn = new SelectList(db.userBookMaps, "Isbn", "Id");
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


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
                string[] words = result.Split(' ');

                List<string> wordList = words.ToList();
                string isbn = wordList[wordList.Count - 1];
                wordList.RemoveAt(wordList.Count() - 1);
                string name = string.Join(" ", wordList.ToArray());

                return Json(new { name = name, isbn = isbn }, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {

            if (ModelState.IsValid)
            {
                if (db.books.Find(book.Isbn) == null)
                {
                    db.books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Isbn", "Bu Isbn numarasını başka kitap kullanmaktadır.");
                    return View(book);
                }
            }


            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                ViewBag.error = "Isbn numarası girilmedi.";
                return View("Error");
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                ViewBag.error = "Isbn numaralı kitap bulunamadı.";
                return View("Error");
            }
            ViewBag.Isbn = new SelectList(db.userBookMaps, "Isbn", "Id", book.Isbn);
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Isbn,Name,AuthorName,Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Isbn = new SelectList(db.userBookMaps, "Isbn", "Id", book.Isbn);
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                ViewBag.error = "Isbn numarası girilmedi.";
                return View("Error");
            }
            Book book = db.books.Find(id);
            if (book == null)
            {
                ViewBag.error = "Isbn numaralı kitap bulunamadı.";
                return View("Error");
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book book = db.books.Find(id);
            db.books.Remove(book);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                
                ViewBag.error = " Bu kitap odunc alinmistir, silinemez. Lutfen kullanicidan isteyiniz.";
                return View("Error");
            }
            
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
