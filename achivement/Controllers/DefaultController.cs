using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace achivement.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult marksheet(string id)
        {
            ViewBag.id = id;
            if (ViewBag.id != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT STUD_ID,FULLNAME,EMAIL,PHONE,GENDER,FORMAT(DOB,'yyyy-MM-dd') as DOB,ADDRESS,STATE, CITY,PINCODE from detail WHERE STUD_Id = @STUD_Id";
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@STUD_Id", id);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                    }

                    ViewBag.editdat = dt;
                }

                //this is comment section 

            }
            return View();
        }

        public JsonResult add(FormCollection fr)
        {
            string fullname = fr["fullname"];
            string email = fr["Email"];
            string phone = fr["Phone"];
            string dob = fr["dob"];
            string address = fr["address"];
            string State = fr["State"];
            string city = fr["city"];
            string pin = fr["pin"];
            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO detail (fullname, Email, Phone, Dob ,Address,state,city, pincode ) " +
                               "VALUES (@fullname, @Email, @Phone, @Dob ,@Address, @state, @city, @pincode )";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Dob", dob);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@pincode", pin);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            // Do your logic here

            return Json(new { message = $"Welcome, {fullname}!" });
        }
        public ActionResult listing()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM DETAIL";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                ViewBag.listing = dt;
            }

            //this is comment section 
            return View();
        }

        public JsonResult update(FormCollection fr)
        {
            string userid = fr["userid"];
            string fullname = fr["fullname"];
            string email = fr["Email"];
            string phone = fr["Phone"];
            string dob = fr["dob"];
            string address = fr["address"];
            string State = fr["State"];
            string city = fr["city"];
            string pin = fr["pin"];
            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE DETAIL SET " +
                                    "FULLNAME = @FULLNAME," +
                                    "EMAIL = @EMAIL,     " +
                                    "PHONE = @PHONE,     " +
                                    "DOB = @DOB,         " +
                                    "ADDRESS = @ADDRESS, " +
                                    "STATE = @STATE,     " +
                                    "CITY = @CITY,       " +
                                    "PINCODE = @PINCODE  " +
                                    "WHERE STUD_ID = @STUD_ID";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL Injection

                    cmd.Parameters.AddWithValue("@STUD_ID", userid);
                    cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                    cmd.Parameters.AddWithValue("@EMAIL", email);
                    cmd.Parameters.AddWithValue("@PHONE", phone);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@ADDRESS", address);
                    cmd.Parameters.AddWithValue("@STATE", State);
                    cmd.Parameters.AddWithValue("@CITY", city);
                    cmd.Parameters.AddWithValue("@PINCODE", pin);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            // Do your logic here

            return Json(new { message = $"Welcome, {fullname}!" });
        }
        public ActionResult Delete(int id)
        {

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM DETAIL WHERE STUD_Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No record found." });
                    }
                }
            }
        }
    }
}