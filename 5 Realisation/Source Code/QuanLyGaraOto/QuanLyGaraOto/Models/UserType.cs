using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models
{
    public class UserType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<UserDetail>  UserDetails { get; set; }
    }
}