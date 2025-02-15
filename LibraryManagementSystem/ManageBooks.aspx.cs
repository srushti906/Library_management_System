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
    public partial class ManageBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
            }
        }
        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            string title = txtBookTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string isbn = txtISBN.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(isbn))
            {
                lblMessage.Text = "All fields are required.";
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Books (Title, Author, ISBN) VALUES (@Title, @Author, @ISBN)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "Book added successfully.";
                LoadBooks();
                ClearForm();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvBooks.SelectedRow;
            txtBookTitle.Text = row.Cells[1].Text;
            txtAuthor.Text = row.Cells[2].Text;
            txtISBN.Text = row.Cells[3].Text;

            btnAddBook.Visible = false;
            btnUpdateBook.Visible = true;
            btnDeleteBook.Visible = true;
        }

        protected void btnUpdateBook_Click(object sender, EventArgs e)
        {
            GridViewRow row = gvBooks.SelectedRow;
            int bookID = Convert.ToInt32(row.Cells[0].Text);

            string title = txtBookTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string isbn = txtISBN.Text.Trim();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE Books SET Title = @Title, Author = @Author, ISBN = @ISBN WHERE BookID = @BookID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.Parameters.AddWithValue("@BookID", bookID);
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "Book updated successfully.";
                LoadBooks();
                ClearForm();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void btnDeleteBook_Click(object sender, EventArgs e)
        {
            GridViewRow row = gvBooks.SelectedRow;
            int bookID = Convert.ToInt32(row.Cells[0].Text);

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM Books WHERE BookID = @BookID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookID);
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "Book deleted successfully.";
                LoadBooks();
                ClearForm();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }

        private void LoadBooks()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Books";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvBooks.DataSource = dt;
                        gvBooks.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }

        private void ClearForm()
        {
            txtBookTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtISBN.Text = string.Empty;
            btnAddBook.Visible = true;
            btnUpdateBook.Visible = false;
            btnDeleteBook.Visible = false;
        }

        protected void @true(object sender, GridViewEditEventArgs e)
        {

        }

        protected void ture(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}