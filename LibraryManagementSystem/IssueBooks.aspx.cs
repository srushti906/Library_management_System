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
    public partial class IssueBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
                LoadBooks();
                LoadIssuedBooks();
            }
        }
        protected void calIssueDate_SelectionChanged(object sender, EventArgs e)
        {
            // Set the selected date from the calendar to the textbox
            txtIssueDate.Text = calIssueDate.SelectedDate.ToString("yyyy-MM-dd");
        }
        private void LoadStudents()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT StudentId, Name FROM Students";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlStudent.DataSource = reader;
                        ddlStudent.DataTextField = "Name";
                        ddlStudent.DataValueField = "StudentId";
                        ddlStudent.DataBind();
                    }
                    ddlStudent.Items.Insert(0, new ListItem("--Select Student--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading students: " + ex.Message;
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
                    string query = "SELECT BookId, Title FROM Books";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlBook.DataSource = reader;
                        ddlBook.DataTextField = "Title";
                        ddlBook.DataValueField = "BookId";
                        ddlBook.DataBind();
                    }
                    ddlBook.Items.Insert(0, new ListItem("--Select Book--", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading books: " + ex.Message;
            }
        }

        private void LoadIssuedBooks()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM IssuedBooks";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvIssuedBooks.DataSource = dt;
                        gvIssuedBooks.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading issued books: " + ex.Message;
            }
        }

        protected void btnIssueBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlStudent.SelectedIndex == 0)
                {
                    lblMessage.Text = "Please select a student.";
                    return;
                }

                if (ddlBook.SelectedIndex == 0)
                {
                    lblMessage.Text = "Please select a book.";
                    return;
                }

                DateTime issueDate;
                if (!DateTime.TryParse(txtIssueDate.Text, out issueDate))
                {
                    lblMessage.Text = "Invalid issue date format. Use YYYY-MM-DD.";
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the book is already issued
                    string checkQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE BookId = @BookId AND ReturnDate IS NULL";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@BookId", ddlBook.SelectedValue);
                        int isBookIssued = (int)checkCmd.ExecuteScalar();

                        if (isBookIssued > 0)
                        {
                            lblMessage.Text = "This book is already issued.";
                            return;
                        }
                    }

                    // Issue the book
                    string query = "INSERT INTO IssuedBooks (BookId, StudentId, IssueDate) VALUES (@BookId, @StudentId, @IssueDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookId", ddlBook.SelectedValue);
                        cmd.Parameters.AddWithValue("@StudentId", ddlStudent.SelectedValue);
                        cmd.Parameters.AddWithValue("@IssueDate", issueDate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Book issued successfully.";
                            ClearForm();
                            LoadIssuedBooks();
                        }
                        else
                        {
                            lblMessage.Text = "Error issuing book.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error issuing book: " + ex.Message;
            }
        }

        private void ClearForm()
        {
            ddlStudent.SelectedIndex = 0;
            ddlBook.SelectedIndex = 0;
            txtIssueDate.Text = string.Empty;
        }
    }
}