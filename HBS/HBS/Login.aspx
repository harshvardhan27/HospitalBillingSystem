<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HBS.Login" Title="Login" ClientIDMode="Static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <title>Medanta + | Login</title>

    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/font-awsome/css/font-awesome.css" rel="stylesheet" />

    <!-- Toastr style -->
    <link href="assets/css/plugins/toastr/toastr.min.css" rel="stylesheet" />

    <!-- Gritter -->
    <link href="assets/js/plugins/gritter/jquery.gritter.css" rel="stylesheet" />

    <link href="assests/css/animate.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
</head>
<body class="gray-bg">
    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>
                <h1 class="logo-name">M+</h1>
            </div>
            <h3>Welcome to Medanta+</h3>
            <p>dedicated to life</p>
            <form class="m-t" role="form" autocomplete="off" runat="server">
                <div class="row panel panel-danger" runat="server" id="errorPanel" visible="false">
                    <div class="panel-heading">
                        <span id="panelHead" runat="server"></span>
                        <button aria-hidden="true" class="close" type="button">×</button>
                    </div>
                    <div class="panel-body">
                        <p id="panelContent" runat="server"></p>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" MaxLength="10"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary block full-width m-b" OnClick="btnLogin_Click" />
                </div>
            </form>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="assets/js/jquery-3.1.1.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/project/masters/login.js"></script>
</body>
</html>
