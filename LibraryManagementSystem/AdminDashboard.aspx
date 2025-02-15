<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="LibraryManagementSystem.AdminDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f6f9;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .dashboard-container {
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            width: 90%;
            max-width: 1100px;
            padding: 30px;
            box-sizing: border-box;
        }

        h1 {
            font-size: 2.2em;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 30px;
        }

        .dashboard-menu {
            background-color: #2c3e50;
            border-radius: 8px;
            padding: 20px;
            margin-bottom: 30px;
        }

        .dashboard-menu ul {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
        }

        .dashboard-menu ul li {
            margin: 10px 0;
            flex: 1 1 30%;
            max-width: 200px;
        }

        .dashboard-menu ul li a {
            display: block;
            padding: 15px 20px;
            background-color: #3498db;
            color: #fff;
            text-align: center;
            text-decoration: none;
            font-size: 18px;
            border-radius: 8px;
            transition: background-color 0.3s ease-in-out, transform 0.2s ease;
        }

        .dashboard-menu ul li a:hover {
            background-color: #2980b9;
            transform: scale(1.05);
        }

        .dashboard-content {
            padding: 20px;
            background-color: #ecf0f1;
            border-radius: 8px;
        }

        .dashboard-content h2 {
            margin-top: 0;
            color: #2c3e50;
        }

        .dashboard-content p {
            color: #34495e;
            font-size: 16px;
            line-height: 1.6;
        }

        #btnLogout {
            display: block;
            margin: 0 auto 20px;
            padding: 10px 20px;
            font-size: 16px;
            background-color: #e74c3c;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        #btnLogout:hover {
            background-color: #c0392b;
        }

        /* Responsive design */
        @media (max-width: 768px) {
            .dashboard-menu ul {
                flex-direction: column;
                align-items: center;
            }

            .dashboard-menu ul li {
                flex: 1 1 100%;
            }

            .dashboard-container {
                padding: 20px;
            }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <h1>Welcome To Library Management System</h1>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />

            <div class="dashboard-menu">
                <ul>
                    <li><a href="ManageStudents.aspx">Manage Students</a></li>
                    <li><a href="ManageBooks.aspx">Manage Books</a></li>
                    <li><a href="IssueBooks.aspx">Issue Books</a></li>
                    <li><a href="ReturnBook.aspx">Return Books</a></li>
                    <li><a href="ViewBorrowedBooks.aspx">View Borrowed Books</a></li>
                    <li><a href="SearchBooks.aspx">Search Books</a></li>
                </ul>
            </div>

            <div class="dashboard-content">
                <h2>Dashboard Overview</h2>
                <p>Select an option from the menu to perform an action.</p>
                <!-- Additional dashboard content like charts, summaries, etc., can be added here -->
            </div>
        </div>
    </form>
</body>

</html>
