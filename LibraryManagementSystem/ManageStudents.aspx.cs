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
    public partial class ManageStudents : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["LMS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }
        private void LoadStudents()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students", conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvStudents.DataSource = dt;
                    gvStudents.DataBind();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Students (Name, Address, PhoneNumber, Class) VALUES (@Name, @Address, @PhoneNumber, @Class)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Class", txtClass.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadStudents();
                    ClearFields();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Students SET Name = @Name, Address = @Address, PhoneNumber = @PhoneNumber, Class = @Class WHERE StudentID = @StudentID", conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", hfStudentID.Value);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Class", txtClass.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadStudents();
                    ClearFields();
                    ToggleButtons(false);
                }
            }
        }

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvStudents.Rows[index];

            if (e.CommandName == "Edit")
            {
                hfStudentID.Value = row.Cells[0].Text;
                txtName.Text = row.Cells[1].Text;
                txtAddress.Text = row.Cells[2].Text;
                txtPhoneNumber.Text = row.Cells[3].Text;
                txtClass.Text = row.Cells[4].Text;

                ToggleButtons(true);
            }
            else if (e.CommandName == "Delete")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE StudentID = @StudentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", row.Cells[0].Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        LoadStudents();
                    }
                }
            }
        }

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtClass.Text = string.Empty;
        }

        private void ToggleButtons(bool isEditing)
        {
            btnAdd.Visible = !isEditing;
            btnUpdate.Visible = isEditing;
            btnCancel.Visible = isEditing;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ToggleButtons(false);
        }

        protected void @true(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void @true(object sender, GridViewEditEventArgs e)
        {

        }
    }
}