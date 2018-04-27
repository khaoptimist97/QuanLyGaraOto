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
    public class ChiTietPhieuSuasController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: ChiTietPhieuSuas
        public ActionResult Index()
        {
            var chiTietPhieuSuas = db.ChiTietPhieuSuas.Include(c => c.PhieuSuaChua).Include(c => c.PhuTung).Include(c => c.TienCong);
            return View(chiTietPhieuSuas.ToList());
        }

        // GET: ChiTietPhieuSuas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuSua chiTietPhieuSua = db.ChiTietPhieuSuas.Find(id);
            if (chiTietPhieuSua == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuSua);
        }

        // GET: ChiTietPhieuSuas/Create
        public ActionResult Create()
        {
            ViewBag.IDPhieu = new SelectList(db.PhieuSuaChuas, "IDPhieu", "IDPhieu");
            ViewBag.IDPhuTung = new SelectList(db.PhuTungs, "IDPhuTung", "TenPhuTung");
            ViewBag.IDTienCong = new SelectList(db.TienCongs, "IDTienCong", "LoaiTC");
            return View();
        }

        // POST: ChiTietPhieuSuas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhieu,IDPhuTung,SoLuongBan,DonGia,IDTienCong,ThanhTien,NoiDung")] ChiTietPhieuSua chiTietPhieuSua)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuSuas.Add(chiTietPhieuSua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPhieu = new SelectList(db.PhieuSuaChuas, "IDPhieu", "IDPhieu", chiTietPhieuSua.IDPhieu);
            ViewBag.IDPhuTung = new SelectList(db.PhuTungs, "IDPhuTung", "TenPhuTung", chiTietPhieuSua.IDPhuTung);
            ViewBag.IDTienCong = new SelectList(db.TienCongs, "IDTienCong", "LoaiTC", chiTietPhieuSua.IDTienCong);
            return View(chiTietPhieuSua);
        }

        // GET: ChiTietPhieuSuas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuSua chiTietPhieuSua = db.ChiTietPhieuSuas.Find(id);
            if (chiTietPhieuSua == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPhieu = new SelectList(db.PhieuSuaChuas, "IDPhieu", "IDPhieu", chiTietPhieuSua.IDPhieu);
            ViewBag.IDPhuTung = new SelectList(db.PhuTungs, "IDPhuTung", "TenPhuTung", chiTietPhieuSua.IDPhuTung);
            ViewBag.IDTienCong = new SelectList(db.TienCongs, "IDTienCong", "LoaiTC", chiTietPhieuSua.IDTienCong);
            return View(chiTietPhieuSua);
        }

        // POST: ChiTietPhieuSuas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhieu,IDPhuTung,SoLuongBan,DonGia,IDTienCong,ThanhTien,NoiDung")] ChiTietPhieuSua chiTietPhieuSua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuSua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPhieu = new SelectList(db.PhieuSuaChuas, "IDPhieu", "IDPhieu", chiTietPhieuSua.IDPhieu);
            ViewBag.IDPhuTung = new SelectList(db.PhuTungs, "IDPhuTung", "TenPhuTung", chiTietPhieuSua.IDPhuTung);
            ViewBag.IDTienCong = new SelectList(db.TienCongs, "IDTienCong", "LoaiTC", chiTietPhieuSua.IDTienCong);
            return View(chiTietPhieuSua);
        }

        // GET: ChiTietPhieuSuas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuSua chiTietPhieuSua = db.ChiTietPhieuSuas.Find(id);
            if (chiTietPhieuSua == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuSua);
        }

        // POST: ChiTietPhieuSuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietPhieuSua chiTietPhieuSua = db.ChiTietPhieuSuas.Find(id);
            db.ChiTietPhieuSuas.Remove(chiTietPhieuSua);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Partial view
        public PartialViewResult ShowAllPhuTung()
        {
            return PartialView("_PhuTungPartial", db.PhuTungs);
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
