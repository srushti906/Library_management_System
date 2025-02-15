using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagementSystem
{
    public partial class SearchBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();  // Open the connection

                    string query = @"
                        SELECT BookId, Title, Author, ISBN
                        FROM Books
                        WHERE Title LIKE @search OR Author LIKE @search OR ISBN LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text.Trim() + "%");

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);  // Fill the DataTable

                            if (dt.Rows.Count > 0)
                            {
                                gvResults.DataSource = dt;
                                gvResults.DataBind();
                                lblMessage.Text = ""; // Clear any previous error messages
                            }
                            else
                            {
                                gvResults.DataSource = null;
                                gvResults.DataBind();
                                lblMessage.Text = "No books found.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                gvResults.DataSource = null;
                gvResults.DataBind();
            }
        }
    }
}