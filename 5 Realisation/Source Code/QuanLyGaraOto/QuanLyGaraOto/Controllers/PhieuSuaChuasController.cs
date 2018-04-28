using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.Controllers
{
    public class PhieuSuaChuasController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuSuaChuas
        public ActionResult Index()
        {
            ViewBag.IDTienCong = new SelectList(db.TienCongs, "IDTienCong", "LoaiTC");
            ViewBag.IDPhieuTN = new SelectList(db.PhieuTiepNhans, "IDPhieuTN", "IDPhieuTN");
            ViewBag.IDPhuTung = new SelectList(db.PhuTungs, "IDPhuTung", "TenPhuTung");
            var phieuSuaChuas = db.PhieuSuaChuas.Include(p => p.PhieuTiepNhan);
            return View(phieuSuaChuas.ToList());
        }
        public ActionResult Save(int idPhieuTN, DateTime date, ChiTietPhieuSua[] chitietphieusua)
        {
            string result = "Error! Thêm chi tiết không thể hoàn tất!";
            if (idPhieuTN != 0 && date != null && chitietphieusua != null)
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
                    db.ChiTietPhieuSuas.Add(C);

                }
                db.SaveChanges();
                result = "Thành công! Thêm chi tiết hoàn tất!";
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        // GET: PhieuSuaChuas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuSuaChua phieuSuaChua = db.PhieuSuaChuas.Find(id);
            if (phieuSuaChua == null)
            {
                return HttpNotFound();
            }
            return View(phieuSuaChua);
        }

        // GET: PhieuSuaChuas/Create
        public ActionResult Create()
        {
            ViewBag.IDPhieuTN = new SelectList(db.PhieuTiepNhans, "IDPhieuTN", "IDPhieuTN");
            return View();
        }

        // POST: PhieuSuaChuas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhieu,IDPhieuTN,NgaySuaChua,TongTien")] PhieuSuaChua phieuSuaChua)
        {
            if (ModelState.IsValid)
            {
                db.PhieuSuaChuas.Add(phieuSuaChua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPhieuTN = new SelectList(db.PhieuTiepNhans, "IDPhieuTN", "IDPhieuTN", phieuSuaChua.IDPhieuTN);
            return View(phieuSuaChua);
        }

        // GET: PhieuSuaChuas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuSuaChua phieuSuaChua = db.PhieuSuaChuas.Find(id);
            if (phieuSuaChua == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPhieuTN = new SelectList(db.PhieuTiepNhans, "IDPhieuTN", "IDPhieuTN", phieuSuaChua.IDPhieuTN);
            return View(phieuSuaChua);
        }

        // POST: PhieuSuaChuas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhieu,IDPhieuTN,NgaySuaChua,TongTien")] PhieuSuaChua phieuSuaChua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuSuaChua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPhieuTN = new SelectList(db.PhieuTiepNhans, "IDPhieuTN", "IDPhieuTN", phieuSuaChua.IDPhieuTN);
            return View(phieuSuaChua);
        }

        // GET: PhieuSuaChuas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuSuaChua phieuSuaChua = db.PhieuSuaChuas.Find(id);
            if (phieuSuaChua == null)
            {
                return HttpNotFound();
            }
            return View(phieuSuaChua);
        }

        // POST: PhieuSuaChuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuSuaChua phieuSuaChua = db.PhieuSuaChuas.Find(id);
            db.PhieuSuaChuas.Remove(phieuSuaChua);
            db.SaveChanges();
            return RedirectToAction("Index");
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
