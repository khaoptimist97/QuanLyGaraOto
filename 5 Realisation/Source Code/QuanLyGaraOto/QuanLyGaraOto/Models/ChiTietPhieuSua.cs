namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuSua")]
    public partial class ChiTietPhieuSua
    {
        [Key]
        [Column(Order = 0)]
        public int IDPhieu { get; set; }

        [Key]
        [Column(Order = 1)]
        public int IDPhuTung { get; set; }

        public int? SoLuongBan { get; set; }

        public int? DonGia { get; set; }

        public int IDTienCong { get; set; }

        public long? ThanhTien { get; set; }

        [StringLength(250)]
        public string NoiDung { get; set; }
        public bool Deleted { get; set; }
        public virtual PhieuSuaChua PhieuSuaChua { get; set; }

        public virtual PhuTung PhuTung { get; set; }
        public virtual TienCong TienCong { get; set; }
    }
}
