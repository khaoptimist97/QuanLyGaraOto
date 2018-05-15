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
        [Display(Name = "Ngày thu")]
        public DateTime? NgayThu { get; set; }
        [Display(Name = "Số tiền thu")]
        public int SoTienThu { get; set; }
        public bool Deleted { get; set; }
        public virtual Xe Xe { get; set; }
    }
}
