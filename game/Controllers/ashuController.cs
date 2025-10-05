using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace game.Controllers
{
    public class ashuController : Controller
    {
       // GET: ashu
        public ActionResult Index(string userid)
        {
            ViewBag.id = userid;
            if (ViewBag.id != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM USERS WHERE userid = @userid";
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
                string query = "SELECT * FROM USERS order by USERID desc";
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
            string name = frm["name"];
            string email = frm["email"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string gender = frm["gender"];
            string message = frm["message"];


            //Get connection string 
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {

                string query = "INSERT INTO USERS ( NAME, EMAIL, ADDRESS, CITY, STATE, GENDER, MESSAGE) " +
                "VALUES ( @NAME, @EMAIL, @ADDRESS, @CITY, @STATE, @GENDER, @MESSAGE)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@NAME ", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@message", message);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //message
            return Json(new { message = $"welcome, {userid}!" });
        }

        [HttpPost]
        public ActionResult Delete(string USERID)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM USERS WHERE USERID = @USERID";

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
            string name = frm["name"];
            string email = frm["email"];
            string address = frm["address"];
            string city = frm["city"];
            string state = frm["state"];
            string userid = frm["userid"];
            string gender = frm["gender"];
            string message = frm["message"];


            string connString = ConfigurationManager.ConnectionStrings["SQLCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"UPDATE USERS
                          SET NAME = @NAME, 
                              EMAIL = @EMAIL,      
                              ADDRESS = @ADDRESS,   
                              CITY = @CITY,    
                              STATE = @STATE,
                              GENDER = @GENDER,
                              MESSAGE = @MESSAGE
                                where userid = @userid";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NAME", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@message", message);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }


                return Json(new { message = $"welcome, {name}!" });
            }
        }

    }
} 