using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace curd_operations.Controllers
{
    public class goodController : Controller
    {
        // GET: good
        public ActionResult Index(string ID)
        {
            ViewBag.id = ID;
            if (ViewBag.id != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM REGEST WHERE user_id = @USER_ID";
                    DataTable dt = new DataTable();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", ID);
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

            using(SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM REGEST";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            ViewBag.listingdata = dt;
            return View();
        }

        [HttpPost]
        public JsonResult insert(FormCollection frm)
        {
            string fullname = frm["fullname"];
            string email = frm["email"];
            string phone = frm["phone"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string pin = frm["pin"];
            string quali = frm["quali"];
            string university = frm["university"];
            string year = frm["year"];

            //Get connection string 
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {

                string query = "INSERT INTO REGEST (USER_FULLNAME, USER_EMAIL, USER_PHONE, USER_ADDRESS, USER_CITY, USER_STATE, USER_PIN, USER_QUALI, USER_UNIVERSITY, USER_YEAR) " +
                "VALUES (@FULLNAME, @EMAIL, @PHONE, @ADDRESS, @CITY, @STATE, @PIN, @QUALI, @UNIVERSITY, @YEAR)";


                using (SqlCommand  cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FULLNAME ", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@pin", pin);
                    cmd.Parameters.AddWithValue("@QUALI ", quali);
                    cmd.Parameters.AddWithValue("@university", university);
                    cmd.Parameters.AddWithValue("@year", year);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //message
            return Json(new { message = $"welcome, {fullname}!" });
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
            string pin = frm["pin"];
            string quali = frm["quali"];
            string university = frm["university"];
            string year = frm["year"];
            string id = frm["userid"];

            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"UPDATE REGEST 
                          SET USER_FULLNAME = @FULLNAME, 
                              USER_EMAIL = @EMAIL,   
                              USER_PHONE = @PHONE,   
                              USER_ADDRESS = @ADDRESS,   
                              USER_CITY = @CITY,    
                              USER_STATE = @STATE ,    
                              USER_PIN = @PIN,   
                              USER_QUALI = @QUALI,    
                              USER_UNIVERSITY = @UNIVERSITY,  
                              USER_YEAR = @YEAR 
                                where user_id = @user_id";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@pin", pin);
                    cmd.Parameters.AddWithValue("@QUALI ", quali);
                    cmd.Parameters.AddWithValue("@university", university);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@user_id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }


                return Json(new { message = $"welcome, {fullname}!" });
            }
        }

        [HttpPost]
        public ActionResult Delete(string USER_ID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM REGEST WHERE USER_ID = @USER_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@USER_ID", USER_ID);

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