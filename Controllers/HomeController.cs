using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using _6351071034_LTWEB_K63.Models;

using PagedList;
using PagedList.Mvc;

namespace _6351071034_LTWEB_K63.Controllers
{
    public class HomeController : Controller
    {
        // Declare DbContext here
        private QLBansachEntities db = new QLBansachEntities();

        public ActionResult Index(int? page)
        {
            int pageSize = 5;               // Số mục trên mỗi trang
            int pageNum = (page ?? 1);      // Trang hiện tại, mặc định là 1 nếu chưa chọn trang nào

            // Lấy dữ liệu từ database và phân trang
            var books = db.SACHes.Include(b => b.NHAXUATBAN).ToList();
            var pagedBooks = books.ToPagedList(pageNum, pageSize); // Tạo IPagedList cho Model

            ViewBag.CurrentPage = pageNum;
            ViewBag.TotalPages = pagedBooks.PageCount; // Số lượng trang tổng cộng

            return View(pagedBooks); // Truyền IPagedList vào View
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
