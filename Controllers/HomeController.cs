using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using _6351071034_LTWEB_K63.Models;

namespace _6351071034_LTWEB_K63.Controllers
{
    public class HomeController : Controller
    {
        // Declare DbContext here
        private QLBansachEntities db = new QLBansachEntities();

        public ActionResult Index()
        {
            // Eagerly load related entities
            var books = db.SACHes.Include(b => b.NHAXUATBAN).ToList();
            ViewBag.Categories = db.CHUDEs.ToList();
            ViewBag.Books = books;

            return View(books); // Return list of books to view
        }

        public ActionResult CHUDE()
        {
            var categories = db.CHUDEs.ToList(); // Query all categories
            return PartialView(categories); // Return list of categories to view
        }

        public ActionResult NHAXUATBAN()
        {
            var nxb = db.NHAXUATBANs.ToList(); // Query all publishers
            return PartialView(nxb); // Return list of publishers to view
        }

        public ActionResult BooksByCategory(int id)
        {
            var books = db.SACHes.Where(b => b.MaCD == id).ToList(); // Query books by category
            return PartialView(books); // Return list of books to view
        }
        public ActionResult BooksByPublisher(int id)
        {
            var books = db.SACHes.Where(b => b.MaNXB == id).ToList(); // Query books by category
            return PartialView(books); // Return list of books to view
        }
        public ActionResult BookDetail(int id)
        {
            var book = db.SACHes.Include(b => b.NHAXUATBAN).FirstOrDefault(b => b.Masach == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return PartialView(book);
        }

        // Dispose of the DbContext properly
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
