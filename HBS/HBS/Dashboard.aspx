<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HBS.Dashboard" ClientIDMode="Static"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
             <h2>Welcome to Medanata +</h2>
            <ol class="breadcrumb">
                <li class="active">
                    <a href="dashboard.aspx">Dashboard</a>
                </li>
                <!--<li class="active">
                    <strong>Breadcrumb</strong>
                </li>-->
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content">
    </div>
</asp:Content>
