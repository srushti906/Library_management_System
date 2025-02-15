<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="LibraryManagementSystem.Signup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
    <link href="C:\Users\91990\source\repos\LMS\LMS\styles.css" rel="stylesheet" type="text/css" />

 <style>
      body {
    font-family: 'Roboto', sans-serif;
    background-color: #f5f5f5;
    margin: 0;
    padding: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
}

.container {
    background-color: #ffffff;
    border-radius: 10px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
    max-width: 400px;
    width: 100%;
    padding: 30px;
    box-sizing: border-box;
    text-align: center;
}

h2 {
    margin-bottom: 20px;
    color: #333333;
}

.form-container {
    display: flex;
    flex-direction: column;
    gap: 15px;
}

label {
    margin-bottom: 5px;
    color: #555555;
    text-align: left;
}

.input-field {
    width: 100%;
    padding: 10px;
    border: 1px solid #cccccc;
    border-radius: 5px;
    font-size: 16px;
    box-sizing: border-box;
    transition: border-color 0.3s ease;
}

.input-field:focus {
    border-color: #4caf50;
    outline: none;
    box-shadow: 0 0 5px rgba(76, 175, 80, 0.5);
}

.message {
    color: #e53935;
    margin-bottom: 15px;
}

.btn {
    padding: 10px 20px;
    background-color: #4caf50;
    color: #ffffff;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s ease, transform 0.2s ease;
}

.btn:hover {
    background-color: #43a047;
    transform: translateY(-2px);
}

p {
    color: #666666;
}

p a {
    color: #4caf50;
    text-decoration: none;
    transition: color 0.3s ease;
}

p a:hover {
    color: #388e3c;
}


 </style>
</head>
   
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Signup</h2>
            <div class="form-container">
                <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field"></asp:TextBox>

                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-field"></asp:TextBox>

                <label for="txtConfirmPassword">Confirm Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="input-field"></asp:TextBox>

                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field"></asp:TextBox>

                <asp:Button ID="btnSignup" runat="server" Text="Sign Up" OnClick="btnSignup_Click" CssClass="btn" />
             <p>Already have an account? <a href="Login.aspx">Login here</a></p>
            </div>
        </div>
    </form>
</body>
</html>

