using QuanLyGaraOto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.ViewModel
{
    public class PhieuSuaChuaViewModel
    {
        public int IDPhieu { get; set; }
        public int IDPhieuTN { get; set; }
        public DateTime? NgaySuaChua { get; set; }
        public List<ChiTietPhieuViewModel> chiTietPhieuSua { get; set; }
        public string TenChuXe { get; set; }
    }
}