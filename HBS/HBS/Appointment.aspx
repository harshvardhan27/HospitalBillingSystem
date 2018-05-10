<%@ Page Title="Appointment" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="HBS.Appointment" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:HiddenField ID="hidPrimaryKey" runat="server" Value="" />
    <asp:HiddenField ID="hidSelectedDoctor" runat="server" Value="" />
    <asp:HiddenField ID="hidSelectedPatient" runat="server" Value="" />
    <asp:HiddenField ID="hidSelectedProcedure" runat="server" Value="" />
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>Book Patient Appointment</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Dashboard.aspx">Dashboard</a>
                </li>
                <li>
                    <span>Patient</span>
                </li>
                <li class="active">
                    <strong>Appointment</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Appointment Table</h5>
                        <div class="ibox-tools">
                            <span id="addNewRecord" title="Click here to add new patient"><i class="fa fa-plus"></i></span>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="dtAppointment">
                                <thead>
                                    <tr>
                                        <th>Patient Name</th>
                                        <th>Doctor Name</th>
                                        <th>Date</th>
                                        <th>Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>Patient Name</th>
                                        <th>Doctor Name</th>
                                        <th>Date</th>
                                        <th>Time</th>
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
        <div class="row" id="appointmentInfo" style="display: none;">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Appointment Information</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="lblPatient" runat="server" Text="Patient" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <select data-placeholder="Choose a Patient..." class="chosen-select" id="ddlPatient" runat="server">
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblDoctor" runat="server" Text="Doctor" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <select data-placeholder="Choose a Doctor..." class="chosen-select" id="ddlDoctor" runat="server">
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblProcedure" runat="server" Text="Procedure" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <select data-placeholder="Choose a Procedure..." class="chosen-select" multiple id="ddlProcedure" runat="server">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group datepickerControl" id="data_1">
                                    <asp:Label ID="lblAppointmentDate" runat="server" Text="Date" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="form-control" placeholder="Enter Appointment Date" ToolTip="Enter Apppointment Date" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblApointmentTime" runat="server" Text="Time" CssClass="col-sm-4 control-label"></asp:Label>
                                    <div class="input-group clockpicker" data-autoclose="true">
                                        <span class="input-group-addon">
                                            <span class="fa fa-clock-o"></span>
                                        </span>
                                        <asp:TextBox ID="txtAppointmentTime" runat="server" CssClass="form-control" placeholder="Enter Appointment Time" ToolTip="Enter Apppointment Time" MaxLength="5"></asp:TextBox>
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
    <script src="assets/js/project/masters/appointment.js"></script>
</asp:Content>
