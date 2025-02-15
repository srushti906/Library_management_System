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
    public partial class ReturnBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIssuedBooks();
                LoadReturnedBooks();
            }
        }
        protected void calReturnDate_SelectionChanged(object sender, EventArgs e)
        {
            // Set the selected date from the calendar to the textbox
            txtReturnDate.Text = calReturnDate.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void btnReturnBook_Click(object sender, EventArgs e)
        {
            int issueId = Convert.ToInt32(ddlIssuedBooks.SelectedValue);
            DateTime returnDate = Convert.ToDateTime(txtReturnDate.Text);

            // Return the book and save to the database
            ReturnIssuedBook(issueId, returnDate);

            // Refresh the GridView
            LoadReturnedBooks();
        }

        private void LoadIssuedBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IssueId, CONCAT(S.Name, ' - ', B.Title) AS IssuedBook FROM IssuedBooks I " +
                               "INNER JOIN Students S ON I.StudentId = S.StudentId " +
                               "INNER JOIN Books B ON I.BookId = B.BookId " +
                               "WHERE I.ReturnDate IS NULL";  // Show only books that haven't been returned
                SqlCommand cmd = new SqlCommand(query, conn);
                ddlIssuedBooks.DataSource = cmd.ExecuteReader();
                ddlIssuedBooks.DataTextField = "IssuedBook";
                ddlIssuedBooks.DataValueField = "IssueId";
                ddlIssuedBooks.DataBind();
            }
            ddlIssuedBooks.Items.Insert(0, new ListItem("--Select Issued Book--", "0"));
        }

        private void LoadReturnedBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT S.Name AS StudentName, B.Title AS BookTitle, I.IssueDate, I.ReturnDate FROM IssuedBooks I " +
                               "INNER JOIN Students S ON I.StudentId = S.StudentId " +
                               "INNER JOIN Books B ON I.BookId = B.BookId " +
                               "WHERE I.ReturnDate IS NOT NULL";  // Show only books that have been returned
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvReturnedBooks.DataSource = dt;
                gvReturnedBooks.DataBind();
            }
        }

        private void ReturnIssuedBook(int issueId, DateTime returnDate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // 1. Update the IssuedBooks table to set the ReturnDate
                    string updateIssuedBooksQuery = "UPDATE IssuedBooks SET ReturnDate = @ReturnDate WHERE IssueId = @IssueId";
                    SqlCommand cmdUpdateIssuedBooks = new SqlCommand(updateIssuedBooksQuery, conn, transaction);
                    cmdUpdateIssuedBooks.Parameters.AddWithValue("@ReturnDate", returnDate);
                    cmdUpdateIssuedBooks.Parameters.AddWithValue("@IssueId", issueId);
                    cmdUpdateIssuedBooks.ExecuteNonQuery();

                    // 2. Get the BookId from IssuedBooks table
                    string getBookIdQuery = "SELECT BookId FROM IssuedBooks WHERE IssueId = @IssueId";
                    SqlCommand cmdGetBookId = new SqlCommand(getBookIdQuery, conn, transaction);
                    cmdGetBookId.Parameters.AddWithValue("@IssueId", issueId);
                    int bookId = Convert.ToInt32(cmdGetBookId.ExecuteScalar());

                    // 3. Update the Books table to mark the book as available
                    string updateBooksQuery = "UPDATE Books SET IsAvailable = 1 WHERE BookId = @BookId";
                    SqlCommand cmdUpdateBooks = new SqlCommand(updateBooksQuery, conn, transaction);
                    cmdUpdateBooks.Parameters.AddWithValue("@BookId", bookId);
                    cmdUpdateBooks.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback transaction on error
                    transaction.Rollback();
                    lblMessage.Text = "An error occurred while returning the book: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}