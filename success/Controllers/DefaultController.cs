using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace success.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult form(string id)
        {
            ViewBag.id = id;


            if (ViewBag.id != null)
            {
                int idD = Convert.ToInt32(ViewBag.id); // or use int.TryParse() for safety
                //int idD = ViewBag.id; // or use int.TryParse() for safety

                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * ,FORMAT(dob,'yyyy-MM-dd') as date  FROM form WHERE FRM_ID = @Id";
                    DataTable dt = new DataTable();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", idD);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                    }

                    ViewBag.Editdata = dt;
                }
            }

            //this is comment section 
            return View();
        }
            
        //http post
        public JsonResult Insert(FormCollection frm)
        {
            string fullname = frm["fullname"];
            string email = frm["email"];
            string phone = frm["phone"];
            string gender = frm["gender"];
            string dob = frm["dob"];
            string address = frm["address"];
            string state = frm["state"];
            string city = frm["city"];
            string pincode = frm["pincode"];


            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO form (fullname, Email, phone, gender ,dob, address, State, city, pincode) " +
                               "VALUES (@fullname, @Email, @Phone, @gender ,@dob, @address,@State, @city, @pincode)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@pincode", pincode);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }

            return Json(new { message = $"welcome" });
        }

        public ActionResult listing()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM FORM";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ViewBag.form = dt;

            return View();
        }

        public ActionResult Delete(int id)
        {

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE  FROM FORM WHERE FRM_Id = @FRM_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FRM_ID", id);

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

        public JsonResult update(FormCollection form)
        {

            string userid = form["userid"];
            string fullname = form["fullname"];
            string email = form["Email"];
            string phone = form["Phone"];
            string gender = form["gender"];
            string dob = form["dob"];
            string address = form["address"];
            string state = form["state"];
            string city = form["city"];
            string pincode = form["pincode"];
            bool termsAccepted = form["TermsAccepted"] == "true" || form["TermsAccepted"] == "on";


            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE FORM SET FULLNAME	= @FULLNAME," +
                                "     EMAIL		= @EMAIL,	" +
                                "     PHONE		= @PHONE,	" +
                                "     GENDER    = @GENDER   " +
                                "     DOB		= @DOB,		" +
                                "     ADDRESS	= @ADDRESS,	" +
                                "     STATE		= @STATE,	" +
                                "     CITY		= @CITY,	" +
                                "	 PINCODE	= @PINCODE,	" +
                                "    WHERE FRM_ID	= @FRM_ID";
                 
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@FRM_ID", userid);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@pincode", pincode);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            // Do your logic here

            return Json(new { message = $"Welcome, {fullname}!" });
        }
    }
}

