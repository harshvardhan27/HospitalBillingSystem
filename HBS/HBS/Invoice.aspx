<%@ Page Title="Invoice" Language="C#" MasterPageFile="~/Application.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="HBS.Invoice" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>Invoice</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Dashboard.aspx">Dashboard</a>
                </li>
                <li>
                    <span>Payment</span>
                </li>
                <li class="active">
                    <strong>Invoice</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="ibox-content p-xl">
            <div class="row">
                <div class="col-sm-6">
                    <h5>From:</h5>
                    <address>
                        <strong>Medanta +</strong><br>
                    </address>
                </div>

                <div class="col-sm-6 text-right">
                    <h4>Invoice No.</h4>
                    <h4 class="text-navy">
                        <asp:Label ID="lblInvoiceNo" runat="server" Text=""></asp:Label></h4>
                    <span>To:</span>
                    <address>
                        <strong>
                            <asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label></strong><br>
                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label><br>
                        <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>,
                        <asp:Label ID="lblProvince" runat="server" Text=""></asp:Label>,&nbsp;<asp:Label ID="lblPostalCode" runat="server" Text=""></asp:Label><br>
                    </address>
                    <p>
                        <span><strong>Invoice Date:&nbsp;</strong><asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label></span>
                    </p>
                </div>
            </div>
            <div class="table-responsive m-t">
                <asp:Table ID="tblBillDetails" runat="server" CssClass="table invoice-table">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Procedure List</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="text-right">Unit Price</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
            <table class="table invoice-total">
                <tbody>
                    <tr>
                        <td><strong>Sub Total :</strong></td>
                        <td>
                            <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td><strong>TAX :</strong></td>
                        <td>
                            <asp:Label ID="lblTax" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td><strong>TOTAL :</strong></td>
                        <td>
                            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
                    </tr>
                </tbody>
            </table>
            <div class="text-right" id="payment" runat="server" visible="false">
                <asp:Button ID="btnPayment" runat="server" Text="Mark as Paid" CssClass="btn btn-primary" OnClick="btnPayment_Click" />
            </div>
            <div class="text-right" id="paymentNote" runat="server" visible="false">
                <p class="text-danger"><strong>** Bill has been paid.</strong></p>
            </div>
        </div>
    </div>
</asp:Content>
