namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhuTung")]
    public partial class PhuTung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhuTung()
        {
            ChiTietPhieuSuas = new HashSet<ChiTietPhieuSua>();
        }

        [Key]
        public int IDPhuTung { get; set; }

        [StringLength(50)]
        public string TenPhuTung { get; set; }

        public int? SoLuongTon { get; set; }

        public int? DonGiaHienHanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuSua> ChiTietPhieuSuas { get; set; }
    }
}
