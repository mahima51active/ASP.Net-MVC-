using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 


namespace RegistrationForm.Controllers
{
    public class FormController : Controller
    {
        // GET: Form
        public ActionResult Index()
        {
            return View();


        }
        public ActionResult Registrations(string id)
        {
            ViewBag.id = id;


            if (ViewBag.id != null)
            {
                int idD = Convert.ToInt32(ViewBag.id); // or use int.TryParse() for safety
                //int idD = ViewBag.id; // or use int.TryParse() for safety

                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM Registration WHERE Id = @Id";
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
        [HttpPost]
        public JsonResult Insert(FormCollection form)
        {
            string name = form["Name"];
            string email = form["Email"];
            string phone = form["Phone"];
            string password = form["Password"];
            string confirmPassword = form["ConfirmPassword"];
            string gender = form["Gender"];
            string country = form["Country"];
            string State = form["State"];
            string datetime = form["date"];
            //string file = form["file"];
            bool termsAccepted = form["TermsAccepted"] == "true" || form["TermsAccepted"] == "on";


            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Registration (Name, Email, Phone, Password, ConfirmPassword, Gender, Country,State,date,TermsAccepted) " +
                               "VALUES (@Name, @Email, @Phone, @Password, @ConfirmPassword, @Gender, @Country,@State, @date, @TermsAccepted)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@date", datetime);
                    cmd.Parameters.AddWithValue("@TermsAccepted", termsAccepted);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            // Do your logic here

            return Json(new { message = $"Welcome, {name}!" });
        }

        [HttpPost]
        public JsonResult update(FormCollection form)
        {

                string userid = form["userid"];
                string name = form["Name"];
                string email = form["Email"];
                string phone = form["Phone"];
                string password = form["Password"];
                string confirmPassword = form["ConfirmPassword"];
                string gender = form["Gender"];
                string country = form["Country"];
                string State = form["State"];
                string file = form["file"];
                string date = form["date"];
            bool termsAccepted = form["TermsAccepted"] == "true" || form["TermsAccepted"] == "on";


                // Get connection string
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                        string query = @"UPDATE Registration
                             SET Name = @Name,
                                 Email = @Email,
                                 Phone = @Phone,
                                 Password = @Password,
                                 ConfirmPassword = @ConfirmPassword,
                                 Gender = @Gender,
                                 Country = @Country,
                                 State = @State,
                                 date = @date,
                                 TermsAccepted = @TermsAccepted
                             WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL Injection
                        cmd.Parameters.AddWithValue("@Id", userid);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Country", country);
                        cmd.Parameters.AddWithValue("@State", State);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@TermsAccepted", termsAccepted);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            // Do your logic here

            return Json(new { message = $"Welcome, {name}!" });
        }
        public ActionResult Listing()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

             DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM Registration";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ViewBag.RegistrationData = dt;

            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM Registration WHERE Id = @Id";

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


