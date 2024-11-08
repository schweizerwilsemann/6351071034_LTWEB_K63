using _6351071034_LTWEB_K63.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;


namespace _6351071034_LTWEB_K63.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang

        private readonly QLBansachEntities _context = new QLBansachEntities();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lsGiohang = Session["Giohang"] as List<Giohang>;

            if (lsGiohang == null)
            {
                lsGiohang = new List<Giohang>();
                Session["Giohang"] = lsGiohang;
            }
            Debug.WriteLine("Returning Giohang List:"); 
            foreach (var item in lsGiohang)
            {
                Debug.WriteLine($"Item: {item.sTensach}, Image: {item.sAnhbia} Quantity: {item.iSoluong}, Price: {item.dDongia}");
            }
            return lsGiohang;
        }


        // Tong so luong
        private int TongSoLuong()
        {
            int iTongSL = 0;
            List<Giohang> lsGiohang = Session["GioHang"] as List<Giohang>;
            if (lsGiohang != null)
                iTongSL = lsGiohang.Sum(n => n.iSoluong);
            return iTongSL;

        }

        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lsGiohang = Session["GioHang"] as List<Giohang>;
            if (lsGiohang != null)
                iTongTien = lsGiohang.Sum(n => n.dThanhtien);
            return iTongTien;
        }

        // Them hang vao gio
        public ActionResult ThemGiohang(int iMasach, string strURL)
        {
            List<Giohang> lsGiohang = Laygiohang();

            Giohang sanpham = lsGiohang.Find(n => n.iMasach == iMasach);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMasach);
                lsGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        // Xay dung trang gio hang

        public ActionResult EmptyGioHang() { return View(); }
        public ActionResult GioHang()
        {
            List<Giohang> lsGiohang = Laygiohang();
            if (lsGiohang.Count == 0)
                return RedirectToAction("EmptyGioHang", "GioHang"); // temporary like this

            ViewBag.Tongsl = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lsGiohang);
        }

        // GET: Giohang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGiohang(int iMaSP)
        {
            List<Giohang> lsGiohang = Laygiohang();
            Giohang sanpham = lsGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if (sanpham != null)
            {
                lsGiohang.RemoveAll(n => n.iMasach == iMaSP);
                return RedirectToAction("GioHang");
            }

            if (lsGiohang.Count == 0)
                return RedirectToAction("Index", "BookStore");
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<Giohang> lsGiohang = Laygiohang();
            Giohang sp = lsGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if (sp != null)
                sp.iSoluong = int.Parse(f["txtSoluong"].ToString());

            return RedirectToAction("Giohang");
        }

        public ActionResult XoaALL()
        {
            List<Giohang> lsGiohang = Laygiohang();
            lsGiohang.Clear();
            return RedirectToAction("Index", "BookStore");
        }

        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }

            if (Session["Giohang"] == null)
                return RedirectToAction("GioHang", "EmptyGioHang");

            List<Giohang> lsGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lsGiohang);
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}