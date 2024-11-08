using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _6351071034_LTWEB_K63.Models
{
    public class Giohang
    {
        private readonly QLBansachEntities _contex = new QLBansachEntities();
        public int iMasach { get; set; }
        public string sTensach { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoluong { get; set; }
        public Double dThanhtien { get { return iSoluong * dDongia; } }

        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH s = _contex.SACHes.Single(n => n.Masach == Masach);
            sTensach = s.Tensach;
            sAnhbia = s.Anhbia;
            dDongia = double.Parse(s.Giaban.ToString());
            iSoluong = 1;
        }
    }
}