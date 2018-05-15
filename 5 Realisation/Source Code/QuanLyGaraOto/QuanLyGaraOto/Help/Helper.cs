using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyGaraOto.Models;
namespace QuanLyGaraOto.Help
{
    public static class Helper
    {
        public static List<int> GhiIDPhuTungThanhMang(ChiTietPhieuSua[] ct)
        {
            List<int> list = new List<int>();
            foreach(ChiTietPhieuSua c in ct)
            {
                list.Add(c.IDPhuTung);
            }
            return list;
        }
    }
}