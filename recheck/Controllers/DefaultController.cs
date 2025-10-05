using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace recheck.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult form(string userid)
        {
            ViewBag.id = userid;
            if (ViewBag.id != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT FORMAT(dob, 'yyyy-MM-dd') AS DOB1, * FROM STUDENT WHERE userid = @userid";
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
            return View();
        }

        public ActionResult listing()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT *, FORMAT(dob, 'dd-MM-yyyy') AS DOB1 FROM STUDENT ORDER BY USERID DESC";
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
            string address = frm["address"];
            string phone = frm["phone"];
            string dob = frm["dob"];
            string course = frm["course"];
            string gender = frm["gender"];


            //Get connection string 
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {

                string query = "INSERT INTO Student (FULLNAME, EMAIL, ADDRESS, DOB, COURSE, PHONE, GENDER) " +
                "VALUES (@FULLNAME, @EMAIL, @ADDRESS, @DOB, @COURSE, @PHONE, @GENDER)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@COURSE", course);
                    cmd.Parameters.AddWithValue("@PHONE", phone);
                    cmd.Parameters.AddWithValue("@GENDER", gender);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //message
            return Json(new { message = $"welcome, {userid}!" });
        }

        [HttpPost]

        public JsonResult update(FormCollection frm)
        {
            string fullname = frm["fullname"];
            string email = frm["email"];
            string address = frm["address"];
            string userid = frm["userid"];
            string dob = frm["dob"];
            string course = frm["course"];
            string phone = frm["phone"];
            string gender = frm["gender"];


            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"UPDATE STUDENT
                          SET FULLNAME = @FULLNAME, 
                              EMAIL = @EMAIL,      
                              ADDRESS = @ADDRESS,   
                              DOB = @DOB,
                              COURSE = @COURSE,
                              PHONE = @PHONE,
                              GENDER = @GENDER
                                where userid = @userid";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@COURSE", course);
                    cmd.Parameters.AddWithValue("@PHONE", phone);
                    cmd.Parameters.AddWithValue("@GENDER", gender);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }


                return Json(new { message = $"welcome, {fullname}!" });
            }
        }

        [HttpPost]
        public ActionResult Delete(string USERID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM STUDENT WHERE USERID = @USERID";

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
    }
}