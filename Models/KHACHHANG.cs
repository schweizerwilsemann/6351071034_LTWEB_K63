//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _6351071034_LTWEB_K63.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class KHACHHANG
    {
        public KHACHHANG()
        {
            this.DONDATHANGs = new HashSet<DONDATHANG>();
        }

        public int MaKH { get; set; }
        [Required(ErrorMessage = "Fullname required")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Username required")]
        public string Taikhoan { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Matkhau { get; set; }
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }
        public string DiachiKH { get; set; }
        public string DienthoaiKH { get; set; }
        public Nullable<System.DateTime> Ngaysinh { get; set; }


        public virtual ICollection<DONDATHANG> DONDATHANGs { get; set; }
    }
}
