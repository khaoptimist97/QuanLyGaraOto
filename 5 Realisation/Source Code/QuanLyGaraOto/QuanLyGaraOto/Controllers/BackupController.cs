using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
namespace QuanLyGaraOto.Controllers
{
    public class BackupController : Controller
    {
       
        // GET: Backup
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Backup()
        {

            //string conString = System.Configuration.ConfigurationManager.ConnectionStrings["data source=DESKTOP-U2T7G9U\\SQLEXPRESS;initial catalog=QLGaraOto;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework"].ToString();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyGaraOtoContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Exec BK_CTPSProcedure";
            
            cmd.ExecuteNonQuery(); 
            conn.Close();

            return View();
          
        }
    }
}