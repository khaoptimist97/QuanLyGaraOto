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
    public class PhieuSuaChuasController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuSuaChuas
        public ViewResult Index(string currentFilter, string searchString, int? page)
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
            var phieuSuaChuas = from s in db.PhieuSuaChuas
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                //phieuSuaChuas = phieuSuaChuas.Where(s => s.Xe.TenChuXe.Contains(searchString));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuSuaChuas.OrderBy(s => s.PhieuTiepNhan.Xe.TenChuXe).ToPagedList(pageNumber, pageSize));
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
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                var phieuTiepNhan = db.PhieuTiepNhans.Where(i => i.IDPhieuTN == id);
                ViewBag.IDPhieuTN = new SelectList(phieuTiepNhan, "IDPhieuTN", "IDPhieuTN", phieuTiepNhan.First().IDPhieuTN);
                return View();
            }
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
