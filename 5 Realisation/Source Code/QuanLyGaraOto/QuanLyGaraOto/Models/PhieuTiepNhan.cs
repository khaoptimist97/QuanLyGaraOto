namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuTiepNhan")]
    public partial class PhieuTiepNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuTiepNhan()
        {
            PhieuSuaChuas = new HashSet<PhieuSuaChua>();
        }

        [Key]
        public int IDPhieuTN { get; set; }

        public int? IDBienSo { get; set; }
        [Display(Name = "Ngày tiếp nhận")]
        public DateTime? NgayTiepNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSuaChua> PhieuSuaChuas { get; set; }

        public virtual Xe Xe { get; set; }
    }
}
