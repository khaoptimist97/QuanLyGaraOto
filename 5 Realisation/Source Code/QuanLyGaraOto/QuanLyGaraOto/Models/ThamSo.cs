namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThamSo")]
    public partial class ThamSo
    {
        [Key]
        public int IDTS { get; set; }

        [StringLength(250)]
        public string NoiDung { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }
    }
}
