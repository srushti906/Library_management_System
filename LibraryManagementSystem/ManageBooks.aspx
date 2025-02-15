<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageBooks.aspx.cs" Inherits="LibraryManagementSystem.ManageBooks" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Books</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #FDF9F9;
            margin: 0;
            padding: 0;
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

        .grid-view {
            margin-top: 20px;
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }

        .grid-view th,
        .grid-view td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .grid-view th {
            background-color: #2c3e50;
            color: #ecf0f1;
            text-transform: uppercase;
            font-size: 14px;
        }

        .grid-view tr:hover {
            background-color: #f1f1f1;
        }

        .grid-view .btn {
            font-size: 12px;
            padding: 5px 10px;
        }

        .grid-view .btn:hover {
            transform: none;
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
            <h2>Manage Books</h2>
            <asp:HiddenField ID="hfBookID" runat="server" />
            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" ForeColor="Red" />
            <div class="form-container">
                <div class="form-group">
                    <label for="txtBookTitle">Book Title</label>
                    <asp:TextBox ID="txtBookTitle" runat="server" CssClass="input-field" MaxLength="100" placeholder="Enter book title" />
                </div>
                <div class="form-group">
                    <label for="txtAuthor">Author</label>
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="input-field" MaxLength="100" placeholder="Enter author" />
                </div>
                <div class="form-group">
                    <label for="txtISBN">ISBN</label>
                    <asp:TextBox ID="txtISBN" runat="server" CssClass="input-field" MaxLength="13" placeholder="Enter ISBN" />
                </div>
                <asp:Button ID="btnAddBook" runat="server" Text="Add Book" CssClass="btn" OnClick="btnAddBook_Click" />
                <asp:Button ID="btnUpdateBook" runat="server" Text="Update Book" CssClass="btn" OnClick="btnUpdateBook_Click" Visible="false" />
                <asp:Button ID="btnDeleteBook" runat="server" Text="Delete Book" CssClass="btn" OnClick="btnDeleteBook_Click" Visible="false" />
                <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnSelectedIndexChanged="gvBooks_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="BookID" HeaderText="Book ID" ReadOnly="True" />
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Author" HeaderText="Author" />
                        <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
