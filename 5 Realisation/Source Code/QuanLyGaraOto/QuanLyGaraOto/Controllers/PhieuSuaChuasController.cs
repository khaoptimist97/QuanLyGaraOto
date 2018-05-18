using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGaraOto.Models;
using PagedList;
using Newtonsoft.Json;
using QuanLyGaraOto.ViewModel;
using System.Collections;
using QuanLyGaraOto.Help;
using QuanLyGaraOto.DTO;

namespace QuanLyGaraOto.Controllers
{
    [Authorize]
    public class PhieuSuaChuasController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuSuaChuas
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            ViewBag.IDTienCong = new SelectList(db.TienCongs, "IDTienCong", "LoaiTC", "---Select TC---");
            //Customize dropdownlist for show IDPhieu+TenChuXe
            IQueryable<PhieuTiepNhan> model = from s in db.PhieuTiepNhans
                                              where s.Deleted == false
                                              select s;
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in model)
            {
                listItems.Add(new SelectListItem
                {
                    Value = item.IDPhieuTN.ToString(),
                    Text = item.IDPhieuTN.ToString() + " -  " + item.Xe.TenChuXe
                });
            }
            ViewBag.IDPhieuTN = new SelectList(listItems, "Value", "Text");
            //
            ViewBag.IDPhuTung = new SelectList(db.PhuTungs, "IDPhuTung", "TenPhuTung");
            //Search
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var phieuSuaChuas = db.PhieuSuaChuas.Include(p => p.PhieuTiepNhan);
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuSuaChuas = phieuSuaChuas.Where(s => s.PhieuTiepNhan.Xe.TenChuXe.Contains(searchString));
            }
            return View(phieuSuaChuas.Where(x => x.Deleted == false).ToList().ToPagedList(page ?? 1, 10));
        }
        public JsonResult GetSearchValue(string search)
        {
            List<Motor> allsearch = db.PhieuSuaChuas.Where(s => s.PhieuTiepNhan.Xe.TenChuXe.Contains(search)).Select(x => new Motor
            {  
                TenChuXe = x.PhieuTiepNhan.Xe.TenChuXe,
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetInfoTienCong(int IDTienCong)
        {
            return Json(db.TienCongs.Where(x => x.IDTienCong == IDTienCong).Select(s => new
            {
                TienCong = s.Gia
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInfoPhuTung(int IDPhuTung)
        {
            return Json(db.PhuTungs.Where(x => x.IDPhuTung == IDPhuTung).Select(s => new
            {
                GiaPhuTung = s.DonGiaHienHanh
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInfoChiTietPhieu(int IDPhieu)
        {
            var phieuSuaChua = db.PhieuSuaChuas.Where(x => x.Deleted == false && x.IDPhieu == IDPhieu).SingleOrDefault();
            var chiTiets = db.ChiTietPhieuSuas.Where(x => x.Deleted == false && x.IDPhieu == IDPhieu).ToList();
            //Tạo List<ChiTietPhieuSuas> của PhieuSuaChua(IDPhieu)
            List<ChiTietPhieuViewModel> chiTietPhieuViewModels = new List<ChiTietPhieuViewModel>();
            foreach (var ele in chiTiets)
            {
                ChiTietPhieuViewModel models = new ChiTietPhieuViewModel()
                {
                    IDPhieu = ele.IDPhieu,
                    IDPhuTung = ele.IDPhuTung,
                    SoLuongBan = ele.SoLuongBan,
                    DonGia = ele.DonGia,
                    IDTienCong = ele.IDTienCong,
                    ThanhTien = ele.ThanhTien,
                    NoiDung = ele.NoiDung
                };
                chiTietPhieuViewModels.Add(models);
            }
            //Tạo PhieuSuaChuas gửi lên view

            PhieuSuaChuaViewModel phieuSua = new PhieuSuaChuaViewModel()
            {
                IDPhieu = phieuSuaChua.IDPhieu,
                IDPhieuTN = (int)phieuSuaChua.IDPhieuTN,
                NgaySuaChua = phieuSuaChua.NgaySuaChua,
                chiTietPhieuSua = chiTietPhieuViewModels,
                TenChuXe = phieuSuaChua.PhieuTiepNhan.Xe.TenChuXe
            };

            return Json(phieuSua, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(int? IDPhieu, int idPhieuTN, DateTime date, ChiTietPhieuSua[] chitietphieusua)
        {
            string result = "Error! Thêm chi tiết không thể hoàn tất!";
            if (IDPhieu == null && idPhieuTN != 0 && date != null && chitietphieusua != null)
            {
                PhieuSuaChua phieuSua = new PhieuSuaChua();
                phieuSua.IDPhieuTN = idPhieuTN;
                phieuSua.NgaySuaChua = date;
                db.PhieuSuaChuas.Add(phieuSua);
                db.SaveChanges();
                int idPhieuSua = phieuSua.IDPhieu;
                foreach (var ct in chitietphieusua)
                {
                    ChiTietPhieuSua C = new ChiTietPhieuSua();
                    C.IDPhieu = idPhieuSua;
                    C.IDPhuTung = ct.IDPhuTung;
                    C.DonGia = ct.DonGia;
                    C.SoLuongBan = ct.SoLuongBan;
                    C.IDTienCong = ct.IDTienCong;
                    C.ThanhTien = ct.ThanhTien;
                    C.NoiDung = ct.NoiDung;
                    db.ChiTietPhieuSuas.Add(C);

                }
                db.SaveChanges();
                result = "Thành công! Thêm chi tiết hoàn tất!";
            }
            else
            {
                try
                {
                    //Trường hợp đã remove 1 hay nhiều ChiTietPhieuSua nào đó ...
                    ChiTietPhieuSua[] C = db.ChiTietPhieuSuas.Where(x => x.IDPhieu == IDPhieu).ToList().ToArray();

                    List<int> listC = Helper.GhiIDPhuTungThanhMang(C);
                    List<int> listChiTiet = Helper.GhiIDPhuTungThanhMang(chitietphieusua);
                    //Tìm phần tử có trong C mà ko có trong chitietphieusua
                    var excepts = listC.Except(listChiTiet).ToArray();
                    if (excepts.Count() !=0)
                    {
                        foreach (var e in excepts)
                        {
                            ChiTietPhieuSua chiTietPhieuSua = db.ChiTietPhieuSuas.Find(IDPhieu, e);
                            chiTietPhieuSua.Deleted = true;
                            //Update lại thành Số lượng tồn, Tổng tiền, Tiền nợ
                            Helper.UpdateAfterDeleteChiTietPhieu(chiTietPhieuSua);
                            //Flag
                            db.Entry(chiTietPhieuSua).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                    foreach (ChiTietPhieuSua chiTiet in chitietphieusua)
                    {
                        int idPhuTung = chiTiet.IDPhuTung;
                        //Nếu đã có ChiTietPhieu này thi chỉ thực hiện edit
                        if (db.ChiTietPhieuSuas.Any(x => x.IDPhieu == IDPhieu && x.IDPhuTung == idPhuTung))
                        {
                            ChiTietPhieuSua ct = db.ChiTietPhieuSuas.Find(IDPhieu, idPhuTung);
                            if (ct.Deleted == false) //Chi Edit vs ChiTiet chua xoa
                            {
                                ct.DonGia = chiTiet.DonGia;
                                ct.SoLuongBan = chiTiet.SoLuongBan;
                                ct.IDTienCong = chiTiet.IDTienCong;
                                ct.ThanhTien = chiTiet.ThanhTien;
                                ct.NoiDung = chiTiet.NoiDung;
                                db.Entry(ct).State = EntityState.Modified;
                            }
                            else //Neu xoa roi (Deleted = true) thi xoa hẳn giá trị đó, va them lai => To fire trigger, update Soluongton, ...
                            {
                                //Xóa
                                db.ChiTietPhieuSuas.Remove(ct);
                                db.SaveChanges();
                                //Gán & Add lại
                                ct.DonGia = chiTiet.DonGia;
                                ct.SoLuongBan = chiTiet.SoLuongBan;
                                ct.IDTienCong = chiTiet.IDTienCong;
                                ct.ThanhTien = chiTiet.ThanhTien;
                                ct.NoiDung = chiTiet.NoiDung;
                                ct.Deleted = false;
                                db.ChiTietPhieuSuas.Add(ct);
                                db.SaveChanges();
                            }
                        }
                        else //Nếu không có thì thêm mới
                        {
                            ChiTietPhieuSua ChiTiet = new ChiTietPhieuSua();
                            ChiTiet.IDPhieu = (int)IDPhieu;
                            ChiTiet.IDPhuTung = chiTiet.IDPhuTung;
                            ChiTiet.DonGia = chiTiet.DonGia;
                            ChiTiet.SoLuongBan = chiTiet.SoLuongBan;
                            ChiTiet.IDTienCong = chiTiet.IDTienCong;
                            ChiTiet.ThanhTien = chiTiet.ThanhTien;
                            ChiTiet.NoiDung = chiTiet.NoiDung;
                            db.ChiTietPhieuSuas.Add(ChiTiet);
                        }
                        db.SaveChanges();
                    }
                    result = "Sửa thành công!!";
                }
                catch (Exception ex)
                {
                    result = "Sửa không thành công!!";
                    throw ex;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: PhieuSuaChuas/Details/5
        public JsonResult GetDetails(int IDPhieu)
        {
            var chiTiets = db.ChiTietPhieuSuas.Where(x => x.Deleted == false && x.IDPhieu == IDPhieu).ToList();
            //Tạo List<ChiTietPhieuSuas> của PhieuSuaChua(IDPhieu)
            List<ChiTietPhieuViewModel> chiTietPhieuViewModels = new List<ChiTietPhieuViewModel>();
            foreach (var ele in chiTiets)
            {
                ChiTietPhieuViewModel models = new ChiTietPhieuViewModel()
                {
                    IDPhieu = ele.IDPhieu,
                    IDPhuTung = ele.IDPhuTung,
                    SoLuongBan = ele.SoLuongBan,
                    DonGia = ele.DonGia,
                    IDTienCong = ele.IDTienCong,
                    ThanhTien = ele.ThanhTien,
                    NoiDung = ele.NoiDung
                };
                chiTietPhieuViewModels.Add(models);
            }
            return Json(chiTietPhieuViewModels, JsonRequestBehavior.AllowGet);
        }    
        // POST: PhieuSuaChuas/Delete/5
        public JsonResult DeleteConfirmation(int IDPhieu)
        {
            bool result = false;
            PhieuSuaChua phieuSua = db.PhieuSuaChuas.Find(IDPhieu);
            if(phieuSua!=null)
            {
                phieuSua.Deleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
