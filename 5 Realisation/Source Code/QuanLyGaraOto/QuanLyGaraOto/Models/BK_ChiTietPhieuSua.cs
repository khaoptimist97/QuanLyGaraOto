using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models
{
    public class BK_ChiTietPhieuSua
    {
        [Key]
        [Column(Order = 0)]
        public DateTime NgayBackup { get; set; }
        [Key]
        [Column(Order = 1)]
        public int IDPhieu { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IDPhuTung { get; set; }

        public int? SoLuongBan { get; set; }

        public int? DonGia { get; set; }

        public int IDTienCong { get; set; }

        public long? ThanhTien { get; set; }

        [StringLength(250)]
        public string NoiDung { get; set; }
        public bool Deleted { get; set; }
    }
}