using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Form(string userid)
        {
            ViewBag.id = userid;
            if (ViewBag.id != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM DETAILS WHERE userid = @userid";
                    DataTable dt = new DataTable();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                    }
                    ViewBag.edit = dt;
                }
            }

            ViewBag.name = "Ashish";
            ViewData["Fullname"] = "Ashish Ashish";

            return View();
        }

        public ActionResult listing()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM DETAILS";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            ViewBag.listingdata = dt;
            return View();
        }

        [HttpPost]
        public JsonResult insert(FormCollection frm)
        {
            string userid = frm["userid"];
            string fullname = frm["fullname"];
            string email = frm["email"];
            string city = frm["city"];
            string state = frm["state"];
            string gender = frm["gender"];
            string password = frm["password"];
            string course = frm["course"];
            string phone = frm["phone"];


            //Get connection string 
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {

                string query = "INSERT INTO DETAILS (FULLNAME, EMAIL, CITY, STATE, GENDER, PHONE, COURSE, PASSWORD ) " +
                "VALUES ( @FULLNAME, @EMAIL, @CITY, @STATE, @GENDER, @PHONE, @COURSE, @PASSWORD )";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@course", course);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gender", gender);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //message
            return Json(new { message = $"welcome, {fullname}!" });
        }

        [HttpPost]
        public ActionResult Delete(string USERID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM DETAILS WHERE USERID = @USERID";

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
                        return Json(new { success = false, message = "no record found!" });
                    }
                }
            }
        }

        [HttpPost]

        public JsonResult update(FormCollection frm)
        {
            string fullname = frm["fullname"];
            string email = frm["email"];
            string city = frm["city"];
            string state = frm["state"];
            string userid = frm["userid"];
            string gender = frm["gender"];
            string password = frm["password"];
            string course = frm["course"];
            string phone = frm["phone"];


            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"UPDATE DETAILS
                          SET FULLNAME = @FULLNAME, 
                              EMAIL = @EMAIL,        
                              CITY = @CITY,    
                              STATE = @STATE,
                              password = @password,
                              course = @course,
                              phone = @phone,
                              GENDER = @GENDER
                                where userid = @userid";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@course", course);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@gender", gender);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }


                return Json(new { message = $"welcome, {fullname}!" });
            }
        }
    }
}