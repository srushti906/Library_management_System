<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueBooks.aspx.cs" Inherits="LibraryManagementSystem.IssueBooks" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Books</title>
    <style>
    body {
        font-family: 'Arial', sans-serif;
        background-color: #FDF9F9;
        margin: 0;
        padding: 0;
    }

    .dashboard-container {
        max-width: 1200px;
        margin: 0 auto;
        padding: 20px;
        background-color: #ffffff;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        margin-top: 60px; /* To accommodate navbar */
    }

    h2 {
        text-align: center;
        color: #333;
        margin-bottom: 20px;
        font-size: 24px;
    }

    .navbar {
        background-color: #2c3e50;
        padding: 15px;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        z-index: 1000;
    }

    .navbar-container {
        max-width: 1200px;
        margin: 0 auto;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .navbar-logo {
        color: #fff;
        font-size: 24px;
        font-weight: bold;
        text-decoration: none;
    }

    .navbar-menu ul {
        list-style: none;
        padding: 0;
        margin: 0;
        display: flex;
        gap: 20px;
    }

    .navbar-menu ul li {
        display: inline;
    }

    .navbar-menu ul li a {
        color: #ffffff;
        text-decoration: none;
        font-size: 16px;
        padding: 8px 15px;
        display: inline-block;
        transition: background-color 0.3s ease;
    }

    .navbar-menu ul li a:hover {
        background-color: #34495e;
    }

    .form-container {
        padding: 20px;
        background-color: #ffffff;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        border-radius: 10px;
    }

    label {
        font-size: 16px;
        color: #34495e;
    }

    .input-field,
    .asp\:DropDownList,
    .asp\:TextBox {
        width: 100%;
        padding: 10px;
        margin-bottom: 20px;
        border: 1px solid #bdc3c7;
        border-radius: 4px;
        font-size: 16px;
        background-color: #f9f9f9;
    }

    .btn {
        padding: 12px;
        background-color: #2980b9;
        color: #fff;
        border: none;
        border-radius: 4px;
        font-size: 16px;
        cursor: pointer;
        width: 100%;
        transition: background-color 0.3s ease;
    }

    .btn:hover {
        background-color: #3498db;
    }

    .message {
        margin-bottom: 20px;
        color: #e74c3c;
    }

    .table-container {
        margin-top: 30px;
    }

    .table {
        width: 100%;
        border-collapse: collapse;
    }

    .table th, 
    .table td {
        padding: 15px;
        border-bottom: 1px solid #bdc3c7;
        text-align: left;
    }

    .table th {
        background-color: #f1c40f;
        font-weight: bold;
        color: #34495e;
    }

    .table tr:nth-child(even) {
        background-color: #f9f9f9;
    }

    .table tr:hover {
        background-color: #ecf0f1;
    }

    /* Responsive Design */
    @media (max-width: 768px) {
        .navbar-menu ul {
            flex-direction: column;
            align-items: flex-start;
        }

        .navbar-menu ul li {
            margin-bottom: 10px;
        }
        .calendar-container {
            margin-bottom: 20px;
        }
    }
</style>
</head>
<body>
     <header class="navbar">
     <div class="navbar-container">
         <a href="AdminDashboard.aspx" class="navbar-logo">&#128218; Library Management</a>
         <nav class="navbar-menu">
             <ul>
                 <li><a href="ManageStudents.aspx">Manage Students</a></li>
                 <li><a href="ManageBooks.aspx">Manage Books</a></li>
                 <li><a href="IssueBooks.aspx">Issue Books</a></li>
                 <li><a href="ReturnBook.aspx">Return Books</a></li>
                 <li><a href="ViewBorrowedBooks.aspx">View Borrowed Books</a></li>
                 <li><a href="SearchBooks.aspx">Search Books</a></li>
             </ul>
         </nav>
     </div>
 </header>
    <form id="form1" runat="server">
      
        <div class="dashboard-container">
            <h2>Issue Book</h2>
            <div class="form-container">
            <label for="ddlStudent">Select Student:</label>
            <asp:DropDownList ID="ddlStudent" runat="server" CssClass="input-field"></asp:DropDownList>
            
            <label for="ddlBook">Select Book:</label>
            <asp:DropDownList ID="ddlBook" runat="server" CssClass="input-field"></asp:DropDownList>
            
            <label for="txtIssueDate">Issue Date:</label>
            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="input-field" placeholder="YYYY-MM-DD"></asp:TextBox>
              <div class="calendar-container">
     <asp:Calendar ID="calIssueDate" runat="server" OnSelectionChanged="calIssueDate_SelectionChanged" />
           </div>
            <asp:Button ID="btnIssueBook" runat="server" Text="Issue Book" CssClass="btn" OnClick="btnIssueBook_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

              </div>
        </div>
            <div class="dashboard-container">
                <h2>Issued Books</h2>
                <asp:GridView ID="gvIssuedBooks" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="IssueId" HeaderText="Issue ID" />
                        <asp:BoundField DataField="BookId" HeaderText="Book ID" />
                        <asp:BoundField DataField="StudentId" HeaderText="Student ID" />
                        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="ReturnDate" HeaderText="Return Date" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>
            
        </div>
    </form>
</body>
</html>
