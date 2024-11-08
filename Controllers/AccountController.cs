using _6351071034_LTWEB_K63.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _6351071034_LTWEB_K63.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private QLBansachEntities db = new QLBansachEntities();

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KHACHHANG model)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            // Nếu không hợp lệ, trả về cùng biểu mẫu và hiển thị lỗi
            return View(model);
        }
        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(KHACHHANG model)
        {
            
                // Kiểm tra tài khoản và mật khẩu
                var user = db.KHACHHANGs
                             .FirstOrDefault(u => u.Taikhoan == model.Taikhoan && u.Matkhau == model.Matkhau);

                if (user != null)
                {
                    Session["UserId"] = user.MaKH;
                    Session["UserName"] = user.HoTen;

                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ sau khi đăng nhập thành công
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                }
        
            return View(model); 
        }
        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
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