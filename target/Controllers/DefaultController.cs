using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace target.Controllers
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
                    string query = "SELECT * FROM FORM WHERE userid = @userid";
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
                string query = "SELECT * FROM FORM";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            ViewBag.listingdata = dt;
            return View();
        }

        [HttpPost]

        public JsonResult insert(FormCollection frm)
        {
            //string userid = frm["userid"];
            string employeename = frm["employeename"];
            string email = frm["email"];
            string phone = frm["phone"];
            string course = frm["course"];
            string dob = frm["dob"];
            string gender = frm["gender"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string country = frm["country"];
            string message = frm["message"];

            //Get connection string 
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO FORM(EMPLOYEENAME, EMAIL, PHONE, COURSE, DOB, GENDER, ADDRESS, CITY, STATE, COUNTRY, MESSAGE)" +
                    "VALUES(@EMPLOYEENAME, @EMAIL, @PHONE, @COURSE, @DOB, @GENDER, @ADDRESS, @CITY, @STATE, @COUNTRY, @MESSAGE)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("EmployeeName", employeename);
                    cmd.Parameters.AddWithValue("Email", email);
                    cmd.Parameters.AddWithValue("Phone", phone);
                    cmd.Parameters.AddWithValue("Course", course);
                    cmd.Parameters.AddWithValue("Dob", dob);
                    cmd.Parameters.AddWithValue("Gender", gender);
                    cmd.Parameters.AddWithValue("Address", address);
                    cmd.Parameters.AddWithValue("City", city);
                    cmd.Parameters.AddWithValue("State", state);
                    cmd.Parameters.AddWithValue("Country", country);
                    cmd.Parameters.AddWithValue("Message", message);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return Json(new { message = $"welcome, {employeename}!" });
            }
        }

        [HttpPost]

        public JsonResult update(FormCollection frm)
        {
            string userid = frm["userid"];
            string employeename = frm["employeename"];
            string email = frm["email"];
            string phone = frm["phone"];
            string course = frm["course"];
            string dob = frm["dob"];
            string gender = frm["gender"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string country = frm["country"];
            string message = frm["message"];


            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"UPDATE form
                          SET EMPLOYEENAME = @EMPLOYEENAME, 
                              EMAIL = @EMAIL,        
                              COURSE = @COURSE,    
                              PHONE = @PHONE,
                              GENDER = @GENDER,
                              DOB = @DOB,
                              ADDRESS = @ADDRESS,
                              CITY = @CITY,
                              STATE = @STATE,
                              COUNTRY = @COUNTRY,
                              MESSAGE = @MESSAGE
                                WHERE USERID = @USERID";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("EmployeeName", employeename);
                    cmd.Parameters.AddWithValue("Email", email);
                    cmd.Parameters.AddWithValue("Phone", phone);
                    cmd.Parameters.AddWithValue("Course", course);
                    cmd.Parameters.AddWithValue("Dob", dob);
                    cmd.Parameters.AddWithValue("Gender", gender);
                    cmd.Parameters.AddWithValue("Address", address);
                    cmd.Parameters.AddWithValue("City", city);
                    cmd.Parameters.AddWithValue("State", state);
                    cmd.Parameters.AddWithValue("Country", country);
                    cmd.Parameters.AddWithValue("Message", message);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }


                return Json(new { message = $"welcome, {employeename}!" });
            }
        }

        [HttpPost]
        public ActionResult Delete(string USERID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM FORM WHERE USERID = @USERID";

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