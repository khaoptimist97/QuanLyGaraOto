using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyGaraOto.BUS
{
    public class BaoCaoDoanhThu_BUS
    {
        public DataTable BaoCao(DateTime date1, DateTime date2)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyGaraOtoContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ShowBaoCaoDoanhSo(@date1,@date2)", conn);
            cmd.Parameters.AddWithValue("@date1", date1);
            cmd.Parameters.AddWithValue("@date2", date2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

    }
}