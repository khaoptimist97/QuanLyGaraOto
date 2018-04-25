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
namespace QuanLyGaraOto.Controllers
{
    public class PhieuTiepNhansController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuTiepNhans
        //public ActionResult Index(int page=1, int pageSize = 10)
        //{
        //    var phieuTiepNhans = db.PhieuTiepNhans.Include(p => p.Xe).OrderByDescending(p=>p.Xe.TenChuXe).ToPagedList(page,pageSize);
        //    return View(phieuTiepNhans);
        //}
        public ViewResult Index(string currentFilter,string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var phieuTiepNhans = from s in db.PhieuTiepNhans
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuTiepNhans = phieuTiepNhans.Where(s => s.Xe.TenChuXe.Contains(searchString));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuTiepNhans.OrderBy(s => s.Xe.TenChuXe).ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index(string searchString, int? page)
        //{
        //    var phieuTiepNhans = db.PhieuTiepNhans.Include(p => p.Xe).OrderBy(p => p.Xe.TenChuXe).ToPagedList(page ?? 1, 10);
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        phieuTiepNhans = phieuTiepNhans.Where(s => s.Xe.TenChuXe.Contains(searchString)).OrderBy(p => p.Xe.TenChuXe).ToPagedList(page ?? 1, 10);
        //    }
        //    return View(phieuTiepNhans);
        //}
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
