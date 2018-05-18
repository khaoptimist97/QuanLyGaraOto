using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models
{
    public class BK_PhieuSuaChua
    {
        [Key]
        [Column(Order = 0)]
        public DateTime NgayBackup { get; set; }
        [Key]
        [Column(Order = 1)]
        public int IDPhieu { get; set; }

        public int? IDPhieuTN { get; set; }
        [DataType(DataType.Date)]
        public DateTime? NgaySuaChua { get; set; }

        public long TongTien { get; set; }
        public bool Deleted { get; set; }
    }
}