namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TienCong")]
    public partial class TienCong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TienCong()
        {
            ChiTietPhieuSuas = new HashSet<ChiTietPhieuSua>();
        }
        [Key]
        public int IDTienCong { get; set; }

        [StringLength(50)]
        public string LoaiTC { get; set; }
        public bool Deleted { get; set; }
        public int? Gia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuSua> ChiTietPhieuSuas { get; set; }
    }
}
