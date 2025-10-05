using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace employeemannagementsystem.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult employeeform()
        {
            return View();
        }
        [HttpPost]
        public JsonResult empcode(string empname)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("employeeoperation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpName", empname);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                var value = dt.Rows[0][0]; // First row, first column
                return Json(value, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet); // Return null if no result
 
            //return Json(empname, JsonRequestBehavior.AllowGet);
        }
    }
}