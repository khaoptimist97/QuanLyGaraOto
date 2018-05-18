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
using QuanLyGaraOto.ViewModel;
using QuanLyGaraOto.DTO;

namespace QuanLyGaraOto.Controllers
{
    [Authorize]
    public class PhieuTiepNhansController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuTiepNhans
      
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
                                 join a in db.Xes on s.IDBienSo equals a.IDBienSo
                                 where  s.Deleted == false
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuTiepNhans = phieuTiepNhans.Where(s => s.Xe.TenChuXe.Contains(searchString));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuTiepNhans.OrderBy(s => s.Xe.TenChuXe).ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetSearchValue(string search)
        {
            List<Motor> allsearch = db.PhieuTiepNhans.Where(s => s.Xe.TenChuXe.Contains(search)).Select(x => new Motor
            {
                TenChuXe = x.Xe.TenChuXe,
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
            PhieuTiepNhan phieu = new PhieuTiepNhan();
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe", db.HieuXes.First().TenHieuXe);
            ViewBag.IDBienSo = new SelectList(db.Xes.Where(x=>x.Deleted==false), "IDBienSo", "TenChuXe", db.Xes.First().TenChuXe);
            return View(phieu);
        }

        // POST: PhieuTiepNhans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhieuTiepNhan phieu)
        {
            if (ModelState.IsValid)
            {
                db.PhieuTiepNhans.Add(phieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe",db.HieuXes.First().TenHieuXe);
            ViewBag.IDBienSo = new SelectList(db.Xes.Where(x => x.Deleted == false), "IDBienSo", "TenChuXe", db.Xes.First().TenChuXe);
            return View(phieu);
        }
        public JsonResult ConfirmThemMoiXe(string TenChuXe, string DiaChi, string SDT, int HieuXe)
        {
            int newIDBienSo = 0;
            if (TenChuXe != null && DiaChi != null && SDT != null && HieuXe != 0)
            {
                Xe xe = new Xe();
                xe.TenChuXe = TenChuXe;
                xe.DiaChi = DiaChi;
                xe.DienThoai = SDT;
                xe.IDHieuXe = HieuXe;
                db.Xes.Add(xe);
                db.SaveChanges();
                newIDBienSo = xe.IDBienSo;
            }
            return Json(newIDBienSo, JsonRequestBehavior.AllowGet);
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
            ViewBag.IDBienSo = new SelectList(db.Xes.Where(x=>x.Deleted==false), "IDBienSo", "TenChuXe", phieuTiepNhan.IDBienSo);
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
            ViewBag.IDBienSo = new SelectList(db.Xes.Where(x => x.Deleted == false), "IDBienSo", "TenChuXe", phieuTiepNhan.IDBienSo);
            phieuTiepNhan.Xe = db.Xes.Find(phieuTiepNhan.IDBienSo);
            return View(phieuTiepNhan);
        }

        // GET: PhieuTiepNhans/Delete/5
        public JsonResult DeleteConfirmation(int IDPhieu)
        {
            bool result = false;
            PhieuTiepNhan phieuTiep = db.PhieuTiepNhans.Find(IDPhieu);
            if (phieuTiep != null)
            {
                phieuTiep.Deleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
