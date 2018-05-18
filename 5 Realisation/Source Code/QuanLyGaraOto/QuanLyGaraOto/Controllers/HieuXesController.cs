using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGaraOto.Filters;
using QuanLyGaraOto.Models;

namespace QuanLyGaraOto.Controllers
{
    [Authorize]
    [AdminFilter]
    public class HieuXesController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: HieuXes
        public ActionResult Index()
        {
            return View(db.HieuXes.ToList());
        }

        // GET: HieuXes/Details/5
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

        // GET: HieuXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HieuXes/Create
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

        // GET: HieuXes/Edit/5
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

        // POST: HieuXes/Edit/5
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

        // GET: HieuXes/Delete/5
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

        // POST: HieuXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                HieuXe hieuXe = db.HieuXes.Find(id);
                db.HieuXes.Remove(hieuXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ModelState.AddModelError("ErrorMessage", "Hiệu xe này đã có trong bảng khác, xóa thất bại.");
                return View();
            }
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
