using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using QuanLyGaraOto.BUS;
using QuanLyGaraOto.Filters;
using QuanLyGaraOto.Reports;
using QuanLyGaraOto.Reports.DataSetDoanhSoTableAdapters;

namespace QuanLyGaraOto.Controllers
{
    [Authorize]
    [AdminFilter]
    public class BaoCaoDSController : Controller
    {

        // GET: BaoCao
        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Report(FormCollection collection)
        {
            DateTime date1 = Convert.ToDateTime(collection["dtp1"].ToString());
            DateTime date2 = Convert.ToDateTime(collection["dtp2"].ToString());
            return RedirectToAction("Export", new { dt1 = date1, dt2 = date2 });
        }
        public ActionResult Export(DateTime dt1, DateTime dt2)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportDoanhSo.rpt")));
            DataSetDoanhSo doanhSoDataSet = new DataSetDoanhSo();
            BaoCaoDoanhSoTableAdapter baoCaoDoanhSoTableAdapter = new BaoCaoDoanhSoTableAdapter();
            baoCaoDoanhSoTableAdapter.Fill(doanhSoDataSet.BaoCaoDoanhSo, dt1, dt2);
            rd.SetDataSource(doanhSoDataSet);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BaoCaoDoanhSo.pdf");

        }
    }
}