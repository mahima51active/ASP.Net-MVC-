using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inventorymannagement.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetloginOtp(string mobileNo)
        {
            string randomNo = RandomString(4);
            Session["otp"] = randomNo;
            Session["mobileNo"] = mobileNo;
            string rtnText = "";
            try
            {
                rtnText = "True-OTP Sent Successfully-Please Enter this OTP " + randomNo;
                return Json(rtnText, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                rtnText = "False-Please enter valid mobile no";
                return Json(rtnText, JsonRequestBehavior.AllowGet);
            }
        }
        private string RandomString(int size)
        {
            string input = "1234567890";
            Random random = new Random();
            string otp = "";

            for (int i = 0; i < size; i++)
            {
                int index = random.Next(input.Length);
                otp += input[index];
            }

            return otp;
        }

        public ActionResult Verification()
        {
            if (Session["mobileNo"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }
        Random rand = new Random();


        [HttpPost]
        public JsonResult VerifyOTP(string otp)
        {
            string rtnText = "";
            string a = Session["otp"].ToString();
            string b = Session["mobileNo"].ToString();
            string mode = "1";
            if (otp == a)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    //string query = "select * from user_regestration where mobileno = @mobileno";
                    string procedureName = "AUTHENTICATION";

                    DataTable dt = new DataTable();

                    //using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // ✅ Specify stored procedure
                        cmd.Parameters.AddWithValue("@mobileno", b);
                        cmd.Parameters.AddWithValue("@mode", mode);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // ✅ use cmd here
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        Session["userid"] = Convert.ToInt32(dt.Rows[0]["userid"]);

                        rtnText = "True-OTP Verified successfully!";
                        return Json(rtnText, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        rtnText = "New-OTP Verified successfully!";
                        return Json(rtnText, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                rtnText = "False-Please enter valid OTP";
                return Json(rtnText, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult register()
        {
            string FLAG = "1";
            if (Session["mobileno"] != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    //string query = "SELECT * FROM COUNTRIES";
                    string procedurename = "COSTCI";
                    using (SqlCommand cmd = new SqlCommand(procedurename, conn))
                    {
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FLAG", FLAG);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        {
                            da.Fill(dt);
                        }
                    }
                }
                ViewBag.COUNTRY = dt;


                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }


        }

        public JsonResult GetStates(int countryId)
        {
            string FLAG = "2";
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
            List<SelectListItem> states = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "select * from[dbo].[STATES] where COUNTRY_ID = @COUNTRY_ID";
                string ProcedureName = "COSTCI";
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand(ProcedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COUNTRY_ID", countryId);
                    cmd.Parameters.AddWithValue("@FLAG", FLAG);
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
            string FLAG = "3";
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
            List<SelectListItem> cities = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "select * from [dbo].[CITIES] where STATE_ID = @state_id";
                string procedurename = "COSTCI";
               
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand(procedurename, conn))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@state_id", stateId);
                    cmd.Parameters.AddWithValue("@FLAG", FLAG);
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
        public JsonResult Insert(FormCollection form)
        {
            string name = form["Name"];
            string email = form["Email"];
            string phone = form["Phone"];
            string country = form["Country"];
            string state = form["State"];
            string city = form["City"];
            string gender = form["Gender"];
            string mode = "2";


            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "INSERT INTO user_regestration (username, Email, Mobileno, Country, State, City, gender) " +
                //               "VALUES (@Name, @Email, @Phone, @Country, @State, @City, @Gender) " +
                //               " SELECT SCOPE_IDENTITY();";
                string procedurename = "AUTHENTICATION";


                //using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlCommand cmd = new SqlCommand(procedurename, conn))
                {
 
                    // Add parameters to prevent SQL Injection
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@mode", mode);
                    

                    conn.Open();
                    object result = cmd.ExecuteScalar(); // Executes only once
                    if (result != null)
                    {
                        Session["userid"] = Convert.ToInt32(result); // Save user ID in session
                    }

                }
            }
            // Do your logic here

            return Json(new { message = $"Welcome, {name}!" });
        }

        public ActionResult Logout()
        {
            // Clear all session variables
            Session.Clear();

            // Optional: Abandon the current session
            Session.Abandon();

            // Optional: Remove authentication cookie if using Forms Authentication
            // FormsAuthentication.SignOut();

            // Redirect to Login page
            return RedirectToAction("login", "login");
        }
    }
}