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
        [Key]
        public int IDTienCong { get; set; }

        [StringLength(50)]
        public string LoaiTC { get; set; }

        public int? Gia { get; set; }
    }
}
