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
    public partial class ViewBorrowedBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBorrowedBooks();
            }
        }
        private void LoadBorrowedBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Adjust the query based on your actual database schema
                    string query = @"
                        SELECT 
                            i.IssueId, 
                            b.Title AS Title, 
                            u.Username AS Username, 
                            i.IssueDate, 
                            i.ReturnDate
                        FROM 
                            IssuedBooks i
                        INNER JOIN 
                            Books b ON i.BookId = b.BookId
                        INNER JOIN 
                            Users u ON i.StudentId = u.id"; // Ensure these columns and tables exist

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);  // Fill the DataTable

                        if (dt.Rows.Count > 0)
                        {
                            gvBorrowedBooks.DataSource = dt;
                            gvBorrowedBooks.DataBind();
                        }
                        else
                        {
                            gvBorrowedBooks.DataSource = null;
                            gvBorrowedBooks.DataBind();
                            lblMessage.Text = "No borrowed books found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while loading borrowed books: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                gvBorrowedBooks.DataSource = null;
                gvBorrowedBooks.DataBind();
            }
        }
    }
}