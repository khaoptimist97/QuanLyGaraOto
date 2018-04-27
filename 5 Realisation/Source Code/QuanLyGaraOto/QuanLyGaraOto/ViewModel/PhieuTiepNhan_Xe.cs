using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.ViewModel
{
    public class PhieuTiepNhan_Xe
    {
        public int IDPhieuTN { get; set; }
        public int IDBienSo { get; set; }
        [Display(Name = "Tên Chủ Xe")]
        public string TenChuXe { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Điện Thoại")]
        public string DienThoai { get; set; }
        [Display(Name = "Hiệu Xe")]
        public int? IDHieuXe { get; set; }
        [Display(Name = "Tiền nợ")]
        public int? TienNo { get; set; }
        [DisplayName("Hiệu xe")]
        public string TenHieuXe { get; set; }
        public DateTime? NgayTiepNhan { get; set; }
    }
}