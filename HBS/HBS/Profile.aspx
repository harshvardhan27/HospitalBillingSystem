<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="HBS.Profile" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:HiddenField ID="hidPrimaryKey" runat="server" Value="" />
    <asp:HiddenField ID="hidSelectedProvince" runat="server" Value="" />
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>Create Patient Profile</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Dashboard.aspx">Dashboard</a>
                </li>
                <li>
                    <span>Patient</span>
                </li>
                <li class="active">
                    <strong>Profile</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Patient Table</h5>
                        <div class="ibox-tools">
                            <span id="addNewRecord" title="Click here to add new patient"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="dtPatient">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Email Id</th>
                                        <th>Contact No</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>Name</th>
                                        <th>Email Id</th>
                                        <th>Contact No</th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row panel panel-danger" runat="server" id="errorPanel" visible="false">
            <div class="panel-heading">
                <span id="panelHead" runat="server"></span>
                <button aria-hidden="true" class="close" type="button">×</button>
            </div>
            <div class="panel-body">
                <p id="panelContent" runat="server"></p>
            </div>
        </div>
        <div class="row" id="patientInfo" style="display: none;">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Patient Information</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="lblFirstname" runat="server" Text="Firstname" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtFirstname" runat="server" CssClass="form-control" placeholder="Enter Firstname" ToolTip="Enter Firstname" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblLastname" runat="server" Text="Lastname" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control" placeholder="Enter Lastname" ToolTip="Enter Lastname" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group datepickerControl" id="data_1">
                                    <asp:Label ID="lblDob" runat="server" Text="Dob" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtDob" runat="server" CssClass="form-control" placeholder="Enter Date of birth" ToolTip="Enter date of birth" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblEmailId" runat="server" Text="Email Id" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" placeholder="Enter Email id" ToolTip="Enter Email id" MaxLength="25"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblContactNo" runat="server" Text="Contact No" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Enter Contact No" ToolTip="Enter Contact No" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="lblAppartment" runat="server" Text="Appartment" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtAppartment" runat="server" CssClass="form-control" placeholder="Enter Appartment" ToolTip="Enter Appartment" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblStreet" runat="server" Text="Street" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" placeholder="Enter Street" ToolTip="Enter Street" TextMode="MultiLine" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblCity" runat="server" Text="City" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Enter City" ToolTip="Enter City" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblProvince" runat="server" Text="Province" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <select data-placeholder="Choose a Province..." class="chosen-select" id="ddlProvince" runat="server">
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" placeholder="Enter Postal Code" ToolTip="Enter Postal Code" MaxLength="7"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <div class="col-sm-8 col-sm-offset-4">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" Style="display: none;" OnClick="btnDelete_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-white" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/project/masters/profile.js"></script>
</asp:Content>
