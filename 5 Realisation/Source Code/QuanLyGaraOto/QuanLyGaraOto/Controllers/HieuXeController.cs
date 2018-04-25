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
    public class HieuXeController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: HieuXe
        public ActionResult Index()
        {
            return View(db.HieuXes.ToList());
        }

        // GET: HieuXe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HieuXe hieuXe = db.HieuXes.Find(id);
            if (hieuXe == null)
            {
                return HttpNotFound();
            }
            return View(hieuXe);
        }

        // GET: HieuXe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HieuXe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHieuXe,TenHieuXe")] HieuXe hieuXe)
        {
            if (ModelState.IsValid)
            {
                db.HieuXes.Add(hieuXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hieuXe);
        }

        // GET: HieuXe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HieuXe hieuXe = db.HieuXes.Find(id);
            if (hieuXe == null)
            {
                return HttpNotFound();
            }
            return View(hieuXe);
        }

        // POST: HieuXe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHieuXe,TenHieuXe")] HieuXe hieuXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hieuXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hieuXe);
        }

        // GET: HieuXe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HieuXe hieuXe = db.HieuXes.Find(id);
            if (hieuXe == null)
            {
                return HttpNotFound();
            }
            return View(hieuXe);
        }

        // POST: HieuXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HieuXe hieuXe = db.HieuXes.Find(id);
            db.HieuXes.Remove(hieuXe);
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
