namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuThuTien")]
    public partial class PhieuThuTien
    {
        [Key]
        public int IDPhieuThu { get; set; }

        public int? IDBienSo { get; set; }

        public DateTime? NgayThu { get; set; }

        public int? SoTienThu { get; set; }

        public virtual Xe Xe { get; set; }
    }
}
