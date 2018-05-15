using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.ViewModel
{
    public class ChiTietPhieuViewModel
    {
        public int IDPhieu { get; set; }
        public int IDPhuTung { get; set; }
        public int? SoLuongBan { get; set; }

        public int? DonGia { get; set; }

        public int IDTienCong { get; set; }

        public long? ThanhTien { get; set; }
        public string NoiDung { get; set; }
    }
}