using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace target5.Controllers
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
                    string query = "SELECT * FROM STUDENTS WHERE USERID = @USERID";
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
                string query = "SELECT * FROM STUDENTS";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            ViewBag.listingdata = dt;
            return View();
        }

        public JsonResult insert(FormCollection frm)
        {
            //string userid = frm["userid"];
            string fullname = frm["fullname"];
            string email = frm["email"];
            string phone = frm["phone"];
            string course = frm["course"];
            string gender = frm["gender"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string country = frm["country"];
            string comment = frm["comment"];

            //Get connection string 
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO STUDENTS(FULLNAME, EMAIL, PHONE, COURSE, GENDER, ADDRESS, CITY, STATE, COUNTRY, COMMENT)" +
                    "VALUES(@FULLNAME, @EMAIL, @PHONE, @COURSE, @GENDER, @ADDRESS, @CITY, @STATE, @COUNTRY, @COMMENT)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Course", course);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@Comment", comment);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return Json(new { message = $"welcome, {fullname}!" });
            }
        }

        [HttpPost]

        public JsonResult update(FormCollection frm)
        {
            string userid = frm["userid"];
            string fullname = frm["fullname"];
            string email = frm["email"];
            string phone = frm["phone"];
            string course = frm["course"];
            string gender = frm["gender"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string country = frm["country"];
            string comment = frm["comment"];


            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"UPDATE STUDENTS
                          SET fullname = @fullname, 
                              EMAIL = @EMAIL,        
                              COURSE = @COURSE,    
                              PHONE = @PHONE,
                              GENDER = @GENDER,
                              ADDRESS = @ADDRESS,
                              CITY = @CITY,
                              STATE = @STATE,
                              COUNTRY = @COUNTRY,
                              comment = @comment
                                WHERE USERID = @USERID";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("fullname", fullname);
                    cmd.Parameters.AddWithValue("Email", email);
                    cmd.Parameters.AddWithValue("Phone", phone);
                    cmd.Parameters.AddWithValue("Course", course);
                    cmd.Parameters.AddWithValue("Gender", gender);
                    cmd.Parameters.AddWithValue("Address", address);
                    cmd.Parameters.AddWithValue("City", city);
                    cmd.Parameters.AddWithValue("State", state);
                    cmd.Parameters.AddWithValue("Country", country);
                    cmd.Parameters.AddWithValue("comment", comment);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }


                return Json(new { message = $"welcome, {userid}!" });
            }
        }

        [HttpPost]
        public ActionResult Delete(string USERID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM STUDENTS WHERE USERID = @USERID";

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