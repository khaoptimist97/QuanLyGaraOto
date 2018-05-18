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
        static QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();
        public static List<int> GhiIDPhuTungThanhMang(ChiTietPhieuSua[] ct)
        {
            List<int> list = new List<int>();
            foreach(ChiTietPhieuSua c in ct)
            {
                list.Add(c.IDPhuTung);
            }
            return list;
        }
        public static void UpdateAfterDeleteChiTietPhieu(ChiTietPhieuSua ct)
        {
            QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();
            PhieuSuaChua phieuSua = db.PhieuSuaChuas.Find(ct.IDPhieu);
            PhuTung phuTung = db.PhuTungs.Find(ct.IDPhuTung);
            Xe xe = db.Xes.Find(phieuSua.PhieuTiepNhan.Xe.IDBienSo);
            //Xu ly
            //Update so luong ton
            phuTung.SoLuong += (int)ct.SoLuongBan;
            //Update tong tien
            phieuSua.TongTien -= (int) ct.ThanhTien;
            //Update tien no
            xe.TienNo -= (int)ct.ThanhTien;
            //Save
            db.SaveChanges();
        }
        public static void UpdateAfterPhieuThuTienUpdating(PhieuThuTien phieuThu, int soTienThuCu)
        {
            Xe xe = db.Xes.Find(phieuThu.IDBienSo);
            xe.TienNo += soTienThuCu;
            xe.TienNo -= phieuThu.SoTienThu;
            db.SaveChanges();
        }
    }
}