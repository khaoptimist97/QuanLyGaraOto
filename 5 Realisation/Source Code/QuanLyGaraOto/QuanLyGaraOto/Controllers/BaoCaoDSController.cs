using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using QuanLyGaraOto.BUS;
using QuanLyGaraOto.Filters;
using QuanLyGaraOto.Reports;
using QuanLyGaraOto.Reports.DataSetDoanhSoTableAdapters;
using QuanLyGaraOto.ViewModel;

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
            try
            {
                DateTime date1 = Convert.ToDateTime(collection["dtp1"].ToString());
                DateTime date2 = Convert.ToDateTime(collection["dtp2"].ToString());
                return RedirectToAction("ReportDoanhSo", new { dt1 = date1, dt2 = date2 });
            }
            catch (FormatException)
            {
                ModelState.AddModelError("ErrorMessage", "Vui lòng nhập đúng định dạng dd/MM/yyyy");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("ErrorMessage", "Vui lòng nhập đúng định dạng dd/MM/yyyy");
                return View();
            }
        }
//        public ActionResult Export(DateTime dt1, DateTime dt2)
//        {
//            //ReportDocument rd = new ReportDocument();
//            //rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportDoanhSo.rpt")));
//            //DataSetDoanhSo doanhSoDataSet = new DataSetDoanhSo();
//            //BaoCaoDoanhSoTableAdapter baoCaoDoanhSoTableAdapter = new BaoCaoDoanhSoTableAdapter();
//            //baoCaoDoanhSoTableAdapter.Fill(doanhSoDataSet.BaoCaoDoanhSo, dt1, dt2);
//            //rd.SetDataSource(doanhSoDataSet);
//            //Response.Buffer = false;
//            //Response.ClearContent();
//            //Response.ClearHeaders();
//            string tenNewFile = "BaoCao_" + dt1.Month + "_" + dt1.Year + "_to_" + dt2.Month + "_" + dt2.Year + ".pdf";
//            //Get Files Name in Default Saving Folder (C:\\ReportGaraOto) and Compare to new file wheather it exists or not
//            try
//            {
//                string[] files = Directory.GetFiles("C:\\ReportGaraOto");
//                if (files.Count() != 0)
//                {
//                    foreach (string file in files)
//                    {
//                        string fileName = Path.GetFileName(file);
//                        if (fileName == tenNewFile)
//                        {
//                            ModelState.AddModelError("ErrorMessage", "File đã tồn tại, vui lòng vào đường dẫn C:\\ReportGaraOto để xem báo cáo từ tháng " + dt1.Month + "/" + dt1.Year + "đến tháng " + dt2.Month + "/" + dt2.Year);
//                            return View("Report");
//                        }
//                    }
//                }
                
//            }
//            catch(Exception ex)
//            {
//                ModelState.AddModelError("ErrorMessage", ex.Message);
//                return View("Report");
//            }
////            //
////            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
////            stream.Seek(0, SeekOrigin.Begin);
////            return File(stream, "application/pdf", tenNewFile);
////S

//        }
        public ActionResult CreateDefaultFolder()
        {
            string rootDirectory = "C:\\ReportGaraOto";
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
                ViewBag.Success = "Thành công";
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "Thư mục đã tồn tại!!");
                return View("Report");
            }
            return View("Report");

        }
        public ActionResult ReportDoanhSo(DateTime dt1, DateTime dt2)
        {
            SetLocalReport(dt1,dt2);

            return View();
        }

        private void SetLocalReport(DateTime dt1, DateTime dt2)
        {
            //Khởi tạo report
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(500);
            reportViewer.Height = Unit.Percentage(500);
            //Đổ dữ liệu vào dataset
            DataSetDoanhSo doanhSoDataSet = new DataSetDoanhSo();
            BaoCaoDoanhSoTableAdapter baoCaoDoanhSoTableAdapter = new BaoCaoDoanhSoTableAdapter();
            baoCaoDoanhSoTableAdapter.Fill(doanhSoDataSet.BaoCaoDoanhSo, dt1, dt2);
            //Set datasource
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BaoCaoDoanhSo.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetDoanhSo",doanhSoDataSet.Tables[0]));
            //
            ViewBag.ReportViewer = reportViewer;
        }

        
    }
}