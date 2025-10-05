using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace achivesuccess.Controllers
{
    public class greatController : Controller
    {
        // GET: great
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

        [HttpPost]
        public JsonResult update(FormCollection frm)
        {
            string fullname = frm["fullname"];
            string email = frm["email"];
            string phone = frm["phone"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];

            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            { 

                string query = @"UPDATE SECTIONS 
                                SET FULLNAME = @FULLNAME,
                                EMAIL = @EMAIL,
                                PHONE = @PHONE,
                                ADDRESS = @ADDRESS,
                                CITY = @CITY, 
                                STATE = @STATE WHERE USERID = @USERID"; 

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@fullname",  fullname);
                    cmd.Parameters.AddWithValue("@EMAIL", email);
                    cmd.Parameters.AddWithValue("@PHONE", phone);
                    cmd.Parameters.AddWithValue("@ADDRESS", address);

                }
                    
            
        }

        public ActionResult listing()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM SECTIONS";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ViewBag.list = dt;
                return View();
        }

        [HttpPost]
        public ActionResult delete(string USERID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM SECTIONS WHERE USERID = @USERID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@USERID", USERID);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        return Json(new { success = true });
                    }

                    else
                    {
                        return Json(new { success = false, message = "no record found !" });
                    }
                }
            }
        }
    }
}