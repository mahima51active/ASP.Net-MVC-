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
using static System.Collections.Specialized.BitVector32;

namespace freshform.Controllers
{
    public class formController : Controller
    {
        // GET: form
        public ActionResult report(string id)
        {
            ViewBag.id = id;
            if(ViewBag.id!=null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM STUDENT_DETAIL WHERE STUDENT_ID=@Id";
                    DataTable dt = new DataTable();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                        ViewBag.update = dt;
                    }
                }
             }


                    return View();
        }

        public ActionResult listing()
        {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "select * from STUDENT_DETAIL";                    
                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                    ViewBag.listing = dt;
                }

            //this is comment section 
            return View();
        }


        [HttpPost]
        public JsonResult Insert(FormCollection frm)
        {
 
           string firstname = frm["firstname"];
           string lastname  = frm["lastname"];
           string dob       = frm["dob"];
           string gender    = frm["gender"];
           string adhar     = frm["adhar"];
           string phone     = frm["phone"];
           string address   = frm["address"];
           string email     = frm["email"];
           string city      = frm["city"];
           string state     = frm["state"];
           string zip       = frm["zip"];
           string ssc       = frm["ssc"];
           string hsc       = frm["hsc"];
           string ug        = frm["ug"];
           string pg        = frm["pg"];
           string course    = frm["course"];
           string session   = frm["session"];
            string hobbies = "";


        // Get connection string
        string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = 
                "INSERT INTO student_detail(FIRST_NAME, LAST_NAME, DATE_BIRTH, GENDER, ADHAR_NUM, PHONE_NUM, EMAIL, ADDRESS, CITY, STATE,ZIP_CODE, SSC_MARKS, HSC_MARKS, UG_MARKS, PG_MARKS, COURSE, SESSION, HOBBIES)"+
                "values(@FIRST_NAME, @LAST_NAME, @DATE_BIRTH, @GENDER, @ADHAR_NUM, @PHONE_NUM, @EMAIL, @ADDRESS, @CITY, @STATE,@ZIP_CODE, @SSC_MARKS, @HSC_MARKS, @UG_MARKS, @PG_MARKS, @COURSE, @SESSION, @HOBBIES)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FIRST_NAME", firstname);
                    cmd.Parameters.AddWithValue("@LAST_NAME",  lastname);
                    cmd.Parameters.AddWithValue("@DATE_BIRTH",dob);
                    cmd.Parameters.AddWithValue("@GENDER",gender);
                    cmd.Parameters.AddWithValue("@ADHAR_NUM",adhar);
                    cmd.Parameters.AddWithValue("@PHONE_NUM",phone);
                    cmd.Parameters.AddWithValue("@EMAIL",email);
                    cmd.Parameters.AddWithValue("@ADDRESS",address);
                    cmd.Parameters.AddWithValue("@CITY", city);
                    cmd.Parameters.AddWithValue("@STATE", state);
                    cmd.Parameters.AddWithValue("@ZIP_CODE", zip);
                    cmd.Parameters.AddWithValue("@SSC_MARKS", ssc);
                    cmd.Parameters.AddWithValue("@HSC_MARKS", hsc);
                    cmd.Parameters.AddWithValue("@UG_MARKS", ug);
                    cmd.Parameters.AddWithValue("@PG_MARKS", pg);
                    cmd.Parameters.AddWithValue("@COURSE", course);
                    cmd.Parameters.AddWithValue("@SESSION", session);
                    cmd.Parameters.AddWithValue("@HOBBIES", hobbies);
                    conn.Open();                               
                    cmd.ExecuteNonQuery();

                }
            }
            // Do your logic here

            return Json(new { message = $"Welcome, {firstname}!" });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM STUDENT_DETAIL WHERE STUDENT_ID=@Id";

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