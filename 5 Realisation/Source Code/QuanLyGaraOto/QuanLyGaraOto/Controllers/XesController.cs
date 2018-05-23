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
using QuanLyGaraOto.DTO;
using QuanLyGaraOto.Filters;

namespace QuanLyGaraOto.Controllers
{
    [Authorize]
    public class XesController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: Xes
        public ViewResult Index(string option, string currentFilter, string searchString, int? page)
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
            var xes = from s in db.Xes
                      where s.Deleted == false
                      select s;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (option == "BrandName")
                    return View(xes.Where(s => s.HieuXe.TenHieuXe.Contains(searchString)).OrderBy(s => s.TenChuXe).ToPagedList(pageNumber, pageSize));
                else
                    return View(xes.Where(s => s.TenChuXe.Contains(searchString)).OrderBy(s => s.TenChuXe).ToPagedList(pageNumber, pageSize));
            }
            return View(xes.OrderBy(s => s.TenChuXe).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public JsonResult GetSearchValue(string search)
        {
            List<Motor> allsearch = db.Xes.Where(s => s.TenChuXe.Contains(search) && s.Deleted == false).Select(x => new Motor
            {
                IDBienSo = x.IDBienSo,
                TenChuXe = x.TenChuXe,
                DiaChi = x.DiaChi,
                DienThoai = x.DienThoai,
                IDHieuXe = x.IDHieuXe,
                TienNo = x.TienNo
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: Xes/Details/5
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

        // GET: Xes/Create
        [AdminFilter]
        public ActionResult Create()
        {
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe");
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBienSo,TenChuXe,DiaChi,DienThoai,IDHieuXe,TienNo")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index", "Xes", new { id = xe.IDBienSo });
            }

            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe", xe.IDHieuXe);
            return View(xe);
        }

        // GET: Xes/Edit/5
        [AdminFilter]
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

        // POST: Xes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult Edit([Bind(Include = "IDBienSo,TenChuXe,DiaChi,DienThoai,IDHieuXe,TienNo")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHieuXe = new SelectList(db.HieuXes, "IDHieuXe", "TenHieuXe", xe.IDHieuXe);
            xe.HieuXe = db.HieuXes.Find(xe.IDHieuXe);
            return View(xe);
        }

        // GET: Xes/Delete/5
        public JsonResult DeleteConfirmation(int BienSoID)
        {
            bool result = false;
            Xe xe = db.Xes.Find(BienSoID);
            if (xe != null)
            {
                xe.Deleted = true;
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
