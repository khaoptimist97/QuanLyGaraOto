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
    public class PhieuThuTiensController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuThuTiens
        public ActionResult Index()
        {
            var phieuThuTiens = db.PhieuThuTiens.Include(p => p.Xe);
            return View(phieuThuTiens.ToList());
        }

        // GET: PhieuThuTiens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(id);
            if (phieuThuTien == null)
            {
                return HttpNotFound();
            }
            return View(phieuThuTien);
        }

        // GET: PhieuThuTiens/Create
        public ActionResult Create()
        {

            var listXes = from s in db.Xes.ToList()
                                select new {
                                                IDBienSo = s.IDBienSo,
                                                IDBienSo_TenChuXe = s.IDBienSo+" - "+s.TenChuXe,
                                            };
            ViewBag.IDBienSo = new SelectList(listXes, "IDBienSo", "IDBienSo_TenChuXe",listXes.First().IDBienSo_TenChuXe);
            return View();
        }
        public ActionResult GetInfoXe(int dropDownID)
        {                      
            return Json(db.Xes.Where(x => x.IDBienSo == dropDownID).Select(s=> new {
                TenChuXe = s.TenChuXe,
                DienThoai = s.DienThoai
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        // POST: PhieuThuTiens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhieuThu,IDBienSo,NgayThu,SoTienThu")] PhieuThuTien phieuThuTien)
        {
            if (ModelState.IsValid)
            {
                db.PhieuThuTiens.Add(phieuThuTien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
            return View(phieuThuTien);
        }

        // GET: PhieuThuTiens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(id);
            if (phieuThuTien == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
            return View(phieuThuTien);
        }

        // POST: PhieuThuTiens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhieuThu,IDBienSo,NgayThu,SoTienThu")] PhieuThuTien phieuThuTien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuThuTien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
            return View(phieuThuTien);
        }

        // GET: PhieuThuTiens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(id);
            if (phieuThuTien == null)
            {
                return HttpNotFound();
            }
            return View(phieuThuTien);
        }

        // POST: PhieuThuTiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(id);
            db.PhieuThuTiens.Remove(phieuThuTien);
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
