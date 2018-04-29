namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Xe")]
    public partial class Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Xe()
        {
            PhieuThuTiens = new HashSet<PhieuThuTien>();
            PhieuTiepNhans = new HashSet<PhieuTiepNhan>();
        }

        [Key]
        public int IDBienSo { get; set; }
        [Display(Name ="Tên Chủ Xe")]
        [StringLength(50)]
        [Required(ErrorMessage ="Vui lòng nhập tên chủ xe")]
        public string TenChuXe { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [StringLength(20)]
        [RegularExpression("^[0-9]{9,13}$", ErrorMessage = "Chỉ nhập 0-9 gồm 9-13 số")]
        [Display(Name = "Điện Thoại")]
        public string DienThoai { get; set; }
        [Display(Name = "Hiệu Xe")]
        public int? IDHieuXe { get; set; }
        [Display(Name = "Tiền nợ")]
        public int? TienNo { get; set; }

        public virtual HieuXe HieuXe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuThuTien> PhieuThuTiens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuTiepNhan> PhieuTiepNhans { get; set; }
    }
}
