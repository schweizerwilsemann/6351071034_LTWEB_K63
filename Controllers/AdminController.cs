using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _6351071034_LTWEB_K63.Models;
using Antlr.Runtime.Tree;
using PagedList;
using PagedList.Mvc;

namespace _6351071034_LTWEB_K63.Controllers
{
    public class BookChartViewModel
    {
        public string ChuDe { get; set; }
        public int SoLuong { get; set; }
    }
    public class AdminController : Controller
    {
        private QLBansachEntities db = new QLBansachEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(FormCollection collection)
        {
            var username = collection["username"];
            var password = collection["password"];


            // Kiểm tra xem có bỏ trống username không
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "Username is required");
            }

            // Kiểm tra xem có bỏ trống password không
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Password is required");
            }

            // Nếu cả hai trường đều được nhập và hợp lệ
            if (ModelState.IsValid)
            {
                ADMIN admin = db.ADMINs.SingleOrDefault(n => n.UserAdmin == username && n.PassAdmin == password);
                if (admin != null)
                {
                    Session["adminLoginSession"] = admin;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Info = "Invalid username or password!";
                }
            }

            return View();
        }

        public ActionResult SACH(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var pagedList = db.SACHes.OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize);
            if (!pagedList.Any())
            {
                ViewBag.Message = "Không có sách nào để hiển thị.";
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = pagedList.PageCount;
            return View(pagedList);
        }
        [HttpGet]
        public ActionResult ThemmoiSach()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisach(SACH sach)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            if (ModelState.IsValid)
            {
       
                db.SACHes.Add(sach);  // Thêm đối tượng mới
                db.SaveChanges();
            }

            return RedirectToAction("SACH");

        }

        public ActionResult Chitietsach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault( n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult Xoasach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("SACH");
        }

        [HttpGet]
        public ActionResult Suasach(int id) {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(sach);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Suasach(SACH sach)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            if (ModelState.IsValid)
            {
                // Retrieve the existing entity
                var existingSach = db.SACHes.SingleOrDefault(n => n.Masach == sach.Masach);
                if (existingSach == null)
                {
                    // Handle the case where the entity no longer exists
                    ModelState.AddModelError("", "The record no longer exists.");
                    return View(sach);
                }

                // Update the existing entity with values from the model
                existingSach.Tensach = sach.Tensach;
                existingSach.Giaban = sach.Giaban;
                existingSach.Mota = sach.Mota;
                existingSach.Anhbia = sach.Anhbia;
                existingSach.Ngaycapnhat = sach.Ngaycapnhat;
                existingSach.Soluongton = sach.Soluongton;
                existingSach.MaCD = sach.MaCD;
                existingSach.MaNXB = sach.MaNXB;

                // Mark the entity as modified and save changes
                db.Entry(existingSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SACH");
            }

            // Return the view with validation errors
            return View(sach);
        }

        public ActionResult SanphamChartData()
        {
            var data = db.SACHes
                .Where(s => s.CHUDE != null)
                .GroupBy(s => s.CHUDE.TenChuDe)
                .Select(g => new BookChartViewModel
                {
                    ChuDe = g.Key,
                    SoLuong = g.Count()
                }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SanphamChart()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}