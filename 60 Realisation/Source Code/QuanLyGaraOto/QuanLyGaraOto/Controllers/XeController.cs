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
    public class XeController : Controller
    {
        private QLGaraContext db = new QLGaraContext();

        // GET: /Xe/
        public ActionResult Index()
        {
            var xes = db.Xes.Include(x => x.HieuXe);
            return View(xes.ToList());
        }

        // GET: /Xe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // GET: /Xe/Create
        public ActionResult Create()
        {
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe");
            return View();
        }

        // POST: /Xe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IDBienSo,TenChuXe,DiaChi,DienThoai,IDHieuXe,TienNo")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe", xe.IDHieuXe);
            return View(xe);
        }

        // GET: /Xe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe", xe.IDHieuXe);
            return View(xe);
        }

        // POST: /Xe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IDBienSo,TenChuXe,DiaChi,DienThoai,IDHieuXe,TienNo")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe", xe.IDHieuXe);
            return View(xe);
        }

        // GET: /Xe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // POST: /Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Xe xe = db.Xes.Find(id);
            db.Xes.Remove(xe);
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
