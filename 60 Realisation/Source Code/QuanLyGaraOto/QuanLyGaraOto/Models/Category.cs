namespace QuanLyGaraOto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Ten { get; set; }

        public int? IDCha { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }
    }
}
