namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuSuaChua")]
    public partial class PhieuSuaChua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuSuaChua()
        {
            ChiTietPhieuSuas = new HashSet<ChiTietPhieuSua>();
            ChiTietPhieuSuas1 = new HashSet<ChiTietPhieuSua>();
        }

        [Key]
        public int IDPhieu { get; set; }

        public int? IDPhieuTN { get; set; }

        public DateTime? NgaySuaChua { get; set; }

        public long? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuSua> ChiTietPhieuSuas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuSua> ChiTietPhieuSuas1 { get; set; }

        public virtual PhieuTiepNhan PhieuTiepNhan { get; set; }
    }
}
