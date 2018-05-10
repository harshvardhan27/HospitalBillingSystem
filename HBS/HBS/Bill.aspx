<%@ Page Title="Bill" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="Bill.aspx.cs" Inherits="HBS.Payment.Bill" ClientIDMode="Static"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>Create Bill</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Dashboard.aspx">Dashboard</a>
                </li>
                <li>
                    <span>Payment</span>
                </li>
                <li class="active">
                    <strong>Bill</strong>
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
                                        <th>View</th>
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
                                        <th>View</th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/project/masters/bill.js"></script>
</asp:Content>
