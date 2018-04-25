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
    public class PhieuTiepNhansController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuTiepNhans
        public ActionResult Index()
        {
            var phieuTiepNhans = db.PhieuTiepNhans.Include(p => p.Xe);
            return View(phieuTiepNhans.ToList());
        }

        // GET: PhieuTiepNhans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuTiepNhan phieuTiepNhan = db.PhieuTiepNhans.Find(id);
            if (phieuTiepNhan == null)
            {
                return HttpNotFound();
            }
            return View(phieuTiepNhan);
        }

        // GET: PhieuTiepNhans/Create
        public ActionResult Create()
        {
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe");
            return View();
        }

        // POST: PhieuTiepNhans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhieuTN,IDBienSo,NgayTiepNhan")] PhieuTiepNhan phieuTiepNhan)
        {
            if (ModelState.IsValid)
            {
                db.PhieuTiepNhans.Add(phieuTiepNhan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuTiepNhan.IDBienSo);
            return View(phieuTiepNhan);
        }

        // GET: PhieuTiepNhans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuTiepNhan phieuTiepNhan = db.PhieuTiepNhans.Find(id);
            if (phieuTiepNhan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuTiepNhan.IDBienSo);
            return View(phieuTiepNhan);
        }

        // POST: PhieuTiepNhans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhieuTN,IDBienSo,NgayTiepNhan")] PhieuTiepNhan phieuTiepNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuTiepNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuTiepNhan.IDBienSo);
            return View(phieuTiepNhan);
        }

        // GET: PhieuTiepNhans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuTiepNhan phieuTiepNhan = db.PhieuTiepNhans.Find(id);
            if (phieuTiepNhan == null)
            {
                return HttpNotFound();
            }
            return View(phieuTiepNhan);
        }

        // POST: PhieuTiepNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuTiepNhan phieuTiepNhan = db.PhieuTiepNhans.Find(id);
            db.PhieuTiepNhans.Remove(phieuTiepNhan);
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
