using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace inventorymannagement.Controllers
{
    public class productController : Controller
    {
        public ActionResult ProductCategory(string id)
        {
            if (Session["userid"] != null)
            {

                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
                string mode = "CatListing";
                string proc = "PRODUCTINSERTUPDATE";
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(proc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mode", mode);
                        cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                    }
                }

                ViewBag.catdat = dt;


                // ✅ EDIT Logic
                ViewBag.id = id;
                if (ViewBag.id != null)
                {
                    string mode2 = "5"; // assume 5 = get category by id
                    DataTable dtt = new DataTable();

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmmd = new SqlCommand(proc, con))
                        {
                            cmmd.CommandType = CommandType.StoredProcedure;
                            cmmd.Parameters.AddWithValue("@mode", mode2);
                            cmmd.Parameters.AddWithValue("@cat_id", id);
                            using (SqlDataAdapter daa = new SqlDataAdapter(cmmd))
                            {
                                con.Open();
                                daa.Fill(dtt);
                                con.Close();
                            }

                            ViewBag.editdata = dtt;
                        }
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }

        [HttpPost]
        public JsonResult ProductCategory(FormCollection form)
        {
            string Category = form["Category"];
            string message = "";


            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string mode = "InsertCat";
                string proc = "PRODUCTINSERTUPDATE";

                using (SqlCommand cmd = new SqlCommand(proc, conn))
                {
                    // Add parameters to prevent SQL Injection
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cat_name", Category);
                    cmd.Parameters.AddWithValue("@mode", mode);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());

                    {
                        //conn.Open();
                        //cmd.ExecuteNonQuery();
                        //conn.Close();
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            message = reader["Message"].ToString(); // Read the custom message
                        }
                        conn.Close();
                    }
                }
                return Json(message, JsonRequestBehavior.AllowGet);

                //return Json(new { message = $"Sucess!" });
            }
        }

        [HttpPost]
        public JsonResult delete(string id)
        {
            string Category_id = id;

            // Get connection string
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string mode = "DeleteCat";
                string proc = "PRODUCTINSERTUPDATE";

                using (SqlCommand cmd = new SqlCommand(proc, conn))
                {
                    // Add parameters to prevent SQL Injection
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cat_id", Category_id);
                    cmd.Parameters.AddWithValue("@mode", mode);
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Json(new { message = $"Sucess!" });
            }
        }

        [HttpPost]
        public JsonResult UPDATE(FormCollection form)
        {
            string Category = form["Category"];
            string Category_id = form["Category_id"];  // 👈 Get ID from form

            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string mode = "UpdateCat";
                string proc = "PRODUCTINSERTUPDATE";

                using (SqlCommand cmd = new SqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cat_id", Category_id);     // ✅ Add cat_id
                    cmd.Parameters.AddWithValue("@cat_name", Category);       // ✅ Add cat_name
                    cmd.Parameters.AddWithValue("@mode", mode);               // ✅ Add mode

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return Json(new { message = $"Update Success!" });
        }

        [HttpGet]
        public ActionResult Addproduct()
        {
            if (Session["mobileNo"] != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
                string mode = "CatListing";
                string proc = "PRODUCTINSERTUPDATE";
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(proc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mode", mode);
                        cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                            conn.Close();
                        }
                    }
                }
                ViewBag.catdat = dt;
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            //return View();
        }

        [HttpGet]
        public JsonResult getCategoryCode(string cat_id)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
            string mode = "1";
            string proc = "PRODUCT_SECTION";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mode", mode);
                    cmd.Parameters.AddWithValue("@cat_id", cat_id);
                    using (SqlDataAdapter daa = new SqlDataAdapter(cmd))
                    {
                        con.Open();
                        daa.Fill(dt);
                        con.Close();
                    }
                    // Read product code from result
                    string productCode = "";
                    if (dt.Rows.Count > 0)
                    {
                        productCode = dt.Rows[0]["ProductCode"].ToString();
                    }

                    // Return it to the frontend as JSON

                    return Json(new { product_code = productCode }, JsonRequestBehavior.AllowGet);

                }
            }
        }

        [HttpPost]
        public JsonResult ProductInsert(FormCollection frm)
        {
            
            string CategoryName = frm["catname"];
            string ProductCode = frm["productcode"];
            string ProductName = frm["productname"];
            string Barcode = frm["barcode"];
            string Unit = frm["unit"];
            string rateStr = frm["rate"];
            string unitStr = frm["mrp"];

            // Convert string to decimal safely
            decimal Rate = 0;
            decimal MRP = 0;

            if (!string.IsNullOrEmpty(rateStr))
                Rate = Convert.ToDecimal(rateStr); // or decimal.Parse(rateStr)

            if (!string.IsNullOrEmpty(unitStr))
                MRP = Convert.ToDecimal(unitStr);



            string connString = ConfigurationManager.ConnectionStrings["SQlCONNECTION"].ConnectionString;
            string mode = "2";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string proc = "PRODUCT_SECTION";
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CATEGORYNAME", CategoryName);
                    cmd.Parameters.AddWithValue("@PRODUCTCODE", ProductCode);
                    cmd.Parameters.AddWithValue("@PRODUCTNAME", ProductName);
                    cmd.Parameters.AddWithValue("@BARCODE", Barcode);
                    cmd.Parameters.AddWithValue("@MRP", MRP);
                    cmd.Parameters.AddWithValue("@RATE", Rate);
                    cmd.Parameters.AddWithValue("@UNIT", Unit);
                    cmd.Parameters.AddWithValue("@mode", mode);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();
                    }
                }
            }

            return Json(new { message = $"welcome, {CategoryName}!" });
        }


    }
}


