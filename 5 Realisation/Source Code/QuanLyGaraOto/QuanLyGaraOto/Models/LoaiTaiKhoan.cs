namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiTaiKhoan")]
    public partial class LoaiTaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }
    }
}
