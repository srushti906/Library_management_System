<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LibraryManagementSystem.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .container {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
            padding: 40px;
            max-width: 400px;
            width: 100%;
            box-sizing: border-box;
        }

        h2 {
            text-align: center;
            margin-bottom: 30px;
            color: #37474f;
            font-size: 24px;
        }

        .form-container {
            display: flex;
            flex-direction: column;
            gap: 15px;
        }

        label {
            margin-bottom: 5px;
            color: #546e7a;
            font-weight: bold;
            font-size: 14px;
        }

        .input-field {
            width: 100%;
            padding: 10px;
            border: 1px solid #cfd8dc;
            border-radius: 5px;
            font-size: 1em;
            box-sizing: border-box;
            transition: border-color 0.3s ease;
        }

        .input-field:focus {
            border-color: #42a5f5;
            outline: none;
            box-shadow: 0 0 5px rgba(66, 165, 245, 0.5);
        }

        .message {
            color: #e53935;
            margin-bottom: 15px;
            text-align: center;
            font-size: 14px;
        }

        .btn {
            padding: 12px;
            border: none;
            border-radius: 5px;
            font-size: 1em;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
            text-align: center;
        }

        .btn-primary {
            background-color: #42a5f5;
            color: #fff;
            font-size: 16px;
        }

        .btn-primary:hover {
            background-color: #1e88e5;
            transform: scale(1.02);
        }

        p {
            margin-top: 15px;
            text-align: center;
            font-size: 14px;
        }

        p a {
            color: #42a5f5;
            text-decoration: none;
            font-weight: bold;
        }

        p a:hover {
            text-decoration: underline;
        }

        /* Responsive Design */
        @media (max-width: 500px) {
            .container {
                padding: 20px;
            }

            h2 {
                font-size: 20px;
            }

            .btn {
                font-size: 14px;
                padding: 10px;
            }
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>Login</h2>
        <form id="form1" runat="server">
            <div class="form-container">
                <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field"></asp:TextBox>

                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-field"></asp:TextBox>

                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />

                <p>Don't have an account? <a href="Signup.aspx">Sign up here</a></p>
            </div>
        </form>
    </div>
</body>
</html>

