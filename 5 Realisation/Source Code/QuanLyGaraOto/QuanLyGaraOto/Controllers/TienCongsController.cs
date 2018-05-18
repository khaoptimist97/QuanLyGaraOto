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
    public class TienCongsController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: TienCongs
        public ActionResult Index()
        {
            return View(db.TienCongs.ToList());
        }

        // GET: TienCongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienCong tienCong = db.TienCongs.Find(id);
            if (tienCong == null)
            {
                return HttpNotFound();
            }
            return View(tienCong);
        }

        // GET: TienCongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TienCongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTienCong,LoaiTC,Deleted,Gia")] TienCong tienCong)
        {
            if (ModelState.IsValid)
            {
                db.TienCongs.Add(tienCong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tienCong);
        }

        // GET: TienCongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienCong tienCong = db.TienCongs.Find(id);
            if (tienCong == null)
            {
                return HttpNotFound();
            }
            return View(tienCong);
        }

        // POST: TienCongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTienCong,LoaiTC,Deleted,Gia")] TienCong tienCong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tienCong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tienCong);
        }

        // GET: TienCongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienCong tienCong = db.TienCongs.Find(id);
            if (tienCong == null)
            {
                return HttpNotFound();
            }
            return View(tienCong);
        }

        // POST: TienCongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TienCong tienCong = db.TienCongs.Find(id);
                db.TienCongs.Remove(tienCong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ModelState.AddModelError("ErrorMessage", "Tiền công này đã có trong bảng khác, xóa thất bại.");
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
