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
using QuanLyGaraOto.Help;

namespace QuanLyGaraOto.Controllers
{
    [Authorize]
    public class PhieuThuTiensController : Controller
    {
        private QuanLyGaraOtoContext db = new QuanLyGaraOtoContext();

        // GET: PhieuThuTiens
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
            var phieuThuTiens = from s in db.PhieuThuTiens
                                join a in db.Xes on s.IDBienSo equals a.IDBienSo
                                where s.Deleted == false
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuThuTiens = phieuThuTiens.Where(s => s.Xe.TenChuXe.Contains(searchString));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuThuTiens.OrderBy(s => s.Xe.TenChuXe).ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetSearchValue(string search)
        {
            List<Motor> allsearch = db.PhieuThuTiens.Where(s => s.Xe.TenChuXe.Contains(search)).Select(x => new Motor
            {
                TenChuXe = x.Xe.TenChuXe,
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: PhieuThuTiens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(id);
            if (phieuThuTien == null)
            {
                return HttpNotFound();
            }
            return View(phieuThuTien);
        }

        // GET: PhieuThuTiens/Create
        public ActionResult Create()
        {

            var listXes = from s in db.Xes.ToList()
                                select new {
                                                IDBienSo = s.IDBienSo,
                                                TenChuXe_IDBienSo = s.TenChuXe + " - "+ s.IDBienSo,
                                            };
            ViewBag.IDBienSo = new SelectList(listXes, "IDBienSo", "TenChuXe_IDBienSo", listXes.First().TenChuXe_IDBienSo);
            return View();
        }
        public ActionResult GetInfoXe(int dropDownID)
        {                      
            return Json(db.Xes.Where(x => x.IDBienSo == dropDownID).Select(s=> new {
                TenChuXe = s.TenChuXe,
                DienThoai = s.DienThoai
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        // POST: PhieuThuTiens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhieuThu,IDBienSo,NgayThu,SoTienThu")] PhieuThuTien phieuThuTien)
        {
            
            if (ModelState.IsValid)
            {
                Xe xe = db.Xes.Find(phieuThuTien.IDBienSo);
                if (phieuThuTien.SoTienThu > xe.TienNo)
                {
                    ModelState.AddModelError("ErrorMessage", "Số tiền thu không được vượt quá số tiền đang nợ");
                    ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
                    return View(phieuThuTien);
                }
                else
                {
                    db.PhieuThuTiens.Add(phieuThuTien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
            return View(phieuThuTien);
        }

        // GET: PhieuThuTiens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(id);
            if (phieuThuTien == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
            return View(phieuThuTien);
        }

        // POST: PhieuThuTiens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhieuThu,IDBienSo,NgayThu,SoTienThu")] PhieuThuTien phieuThuTien, int SoTienThuCu)
        {
            if (ModelState.IsValid)
            {
                //Update tien no sau khi sua
                Helper.UpdateAfterPhieuThuTienUpdating(phieuThuTien, SoTienThuCu);
                db.Entry(phieuThuTien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBienSo = new SelectList(db.Xes, "IDBienSo", "TenChuXe", phieuThuTien.IDBienSo);
            return View(phieuThuTien);
        }

        public JsonResult DeleteConfirmation(int IDPhieu)
        {
            bool result = false;
            PhieuThuTien phieuThuTien = db.PhieuThuTiens.Find(IDPhieu);
            if (phieuThuTien != null)
            {
                phieuThuTien.Deleted = true;
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
