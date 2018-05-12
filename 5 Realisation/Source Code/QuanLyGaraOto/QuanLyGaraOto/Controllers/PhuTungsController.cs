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
    public class PhuTungsController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhuTungs
        public ActionResult Index()
        {
            return View(db.PhuTungs.ToList());
        }

        // GET: PhuTungs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhuTung phuTung = db.PhuTungs.Find(id);
            if (phuTung == null)
            {
                return HttpNotFound();
            }
            return View(phuTung);
        }

        // GET: PhuTungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhuTungs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhuTung,TenPhuTung,SoLuongTon,DonGiaHienHanh,Deleted")] PhuTung phuTung)
        {
            if (ModelState.IsValid)
            {
                db.PhuTungs.Add(phuTung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phuTung);
        }

        // GET: PhuTungs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhuTung phuTung = db.PhuTungs.Find(id);
            if (phuTung == null)
            {
                return HttpNotFound();
            }
            return View(phuTung);
        }

        // POST: PhuTungs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhuTung,TenPhuTung,SoLuongTon,DonGiaHienHanh,Deleted")] PhuTung phuTung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phuTung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phuTung);
        }

        // GET: PhuTungs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhuTung phuTung = db.PhuTungs.Find(id);
            if (phuTung == null)
            {
                return HttpNotFound();
            }
            return View(phuTung);
        }

        // POST: PhuTungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhuTung phuTung = db.PhuTungs.Find(id);
            db.PhuTungs.Remove(phuTung);
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
