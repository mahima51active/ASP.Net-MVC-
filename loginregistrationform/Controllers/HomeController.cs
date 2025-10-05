using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace loginregistrationform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    //string query = "SELECT * FROM USER_REGISTRATION";
                    string query = @"SELECT r.*,c.COUNTRY_NAME,s.STATE_NAME,cc.CITY_NAME FROM USER_REGISTRATION r 
                        left join COUNTRIES c on c.COUNTRY_ID= r.country
                        left join STATES s on s.STATE_ID = r.state
                        left join CITIES cc on cc.CITY_ID= r.city";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                ViewBag.RegistrationData = dt;

                return View();

            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM USER_REGISTRATION WHERE USERID = @USERID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@USERID", id);

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

        public ActionResult About(string id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
                ViewBag.id = id;
                if (ViewBag.id!=null)
                {
                    int idD = Convert.ToInt32(ViewBag.id); // or use int.TryParse() for safety
                                                           //int idD = ViewBag.id; // or use int.TryParse() for safety
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        string query = "SELECT * FROM USER_REGISTRATION WHERE USERID = @USERID";
                        DataTable dtt = new DataTable();

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@USERId", id);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                            {
                                conn.Open();
                                da.Fill(dtt);
                                conn.Close();
                            }
                        }

                        ViewBag.Editdata = dtt;
                    }
                }

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM COUNTRIES";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                ViewBag.COUNTRY = dt;

                return View();
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public JsonResult GetStates(int countryId)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
            List<SelectListItem> states = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "select * from[dbo].[STATES] where COUNTRY_ID = @COUNTRY_ID";
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@COUNTRY_ID", countryId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                    {
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    states.Add(new SelectListItem
                    {
                        Value = row["STATE_ID"].ToString(),
                        Text = row["STATE_NAME"].ToString()
                    });
                }

                return Json(states, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetCities(int stateId)
        {

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
            List<SelectListItem> cities = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "select * from [dbo].[CITIES] where STATE_ID = @state_id";
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@state_id", stateId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                    {
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    cities.Add(new SelectListItem
                    {
                        Value = row["CITY_ID"].ToString(),
                        Text = row["CITY_NAME"].ToString()
                    });
                }
                return Json(cities, JsonRequestBehavior.AllowGet);
            }

        }
       
        [HttpPost]
        public ActionResult About(FormCollection form, HttpPostedFileBase txtimage)
        {
            string userid = form["userid"];
            string fullname = form["fullname"];
            string email = form["email"];
            string phone = form["phone"];
            string password = form["password"];
            string gender = form["gender"];
            string country = form["country"];
            string state = form["state"];
            string city = form["city"];
            string imgnewname = "";
            string rtnText = "";

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (string.IsNullOrEmpty(userid) || userid == "0") // ✅ Insert New
                    {
                        string insertQuery = @"
                    INSERT INTO USER_REGISTRATION 
                    (FULLNAME, EMAIL, PHONE, PASSWORD, GENDER, COUNTRY, STATE, CITY, IMAGE_NAME)
                    VALUES 
                    (@FULLNAME, @EMAIL, @PHONE, @PASSWORD, @GENDER, @COUNTRY, @STATE, @CITY, @IMAGE_NAME);
                    SELECT SCOPE_IDENTITY();";

                        imgnewname = ""; // default empty

                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                            cmd.Parameters.AddWithValue("@EMAIL", email);
                            cmd.Parameters.AddWithValue("@PHONE", phone);
                            cmd.Parameters.AddWithValue("@PASSWORD", password);
                            cmd.Parameters.AddWithValue("@GENDER", gender);
                            cmd.Parameters.AddWithValue("@COUNTRY", country);
                            cmd.Parameters.AddWithValue("@STATE", state);
                            cmd.Parameters.AddWithValue("@CITY", city);
                            cmd.Parameters.AddWithValue("@IMAGE_NAME", imgnewname);

                            conn.Open();
                            object insertedId = cmd.ExecuteScalar();
                            conn.Close();

                            userid = insertedId.ToString();
                        }
                    }

                    // ✅ If image uploaded (insert or update), save and update it
                    if (txtimage != null && txtimage.ContentLength > 0)
                    {
                        string ext = Path.GetExtension(txtimage.FileName).ToLower();
                        var allowedTypes = new[] { ".jpg", ".jpeg", ".png" };

                        if (!allowedTypes.Contains(ext))
                        {
                            return Json("False-Only JPG, JPEG, PNG allowed", JsonRequestBehavior.AllowGet);
                        }

                        string uploadPath = Server.MapPath("~/profileImg/");
                        if (!Directory.Exists(uploadPath))
                            Directory.CreateDirectory(uploadPath);

                        imgnewname = userid + "_ProfileImg" + ext;
                        string fullPath = Path.Combine(uploadPath, imgnewname);
                        txtimage.SaveAs(fullPath);

                        string updateImageQuery = "UPDATE USER_REGISTRATION SET IMAGE_NAME = @IMAGE_NAME WHERE USERID = @USERID";
                        using (SqlCommand cmd = new SqlCommand(updateImageQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@IMAGE_NAME", imgnewname);
                            cmd.Parameters.AddWithValue("@USERID", userid);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    // ✅ If it's an update (userid exists)
                    if (!string.IsNullOrEmpty(userid) && userid != "0")
                    {
                        string updateQuery = @"
                                UPDATE USER_REGISTRATION
                                SET FULLNAME = @FULLNAME,
                                    EMAIL = @EMAIL,
                                    PHONE = @PHONE,
                                    PASSWORD = @PASSWORD,
                                    GENDER = @GENDER,
                                    COUNTRY = @COUNTRY,
                                    STATE = @STATE,
                                    CITY = @CITY
                                WHERE USERID = @USERID";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                            cmd.Parameters.AddWithValue("@EMAIL", email);
                            cmd.Parameters.AddWithValue("@PHONE", phone);
                            cmd.Parameters.AddWithValue("@PASSWORD", password);
                            cmd.Parameters.AddWithValue("@GENDER", gender);
                            cmd.Parameters.AddWithValue("@COUNTRY", country);
                            cmd.Parameters.AddWithValue("@STATE", state);
                            cmd.Parameters.AddWithValue("@CITY", city);
                            cmd.Parameters.AddWithValue("@USERID", userid);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    rtnText = "True-Registration successful";
                }

                return Json(rtnText, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("False-Error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}



