using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace better.Controllers
{
    public class goodController : Controller
    {
        // GET: good
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult insert(FormCollection frm)
        {
            //string userid = frm["userid"];
            string fullname = frm["fullname"];
            string email = frm["email"];
            string phone = frm["phone"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];

            //get connections string
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO SECTIONS (FULLNAME, EMAIL, PHONE, ADDRESS, CITY, STATE)" +
                               "VALUES(@FULLNAME, @EMAIL, @PHONE, @ADDRESS, @CITY, @STATE)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                    cmd.Parameters.AddWithValue("@EMAIL", email);
                    cmd.Parameters.AddWithValue("@PHONE", phone);
                    cmd.Parameters.AddWithValue("@ADDRESS", address);
                    cmd.Parameters.AddWithValue("@CITY", city);
                    cmd.Parameters.AddWithValue("@STATE", state);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return Json(new { message = $"welcome, {fullname}" });
        }
    }
}
   