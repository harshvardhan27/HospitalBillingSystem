$(document).ready(function () {
    appBill.init();
});
var datatableResponse = "";
var aoData = "";

var appBill = {
    init: function () {
        this.bindEvents();
        this.bindDataTable();
    },

    bindEvents: function () {


    },

    bindDataTable: function () {
        $.ajax({
            type: "POST",
            url: "Bill.aspx/BindDataTable",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //datasource = data;
                datatableResponse = $('#dtAppointment').DataTable({
                    "aaData": JSON.parse(data.d),
                    "pageLength": 5,
                    "responsive": true,
                    "lengthMenu": [[5, 25, 50, -1], [5, 25, 50, "All"]],
                    "dom": '<"html5buttons"B>lTfgitp',
                    "buttons": [
                    { extend: 'copy' },
                    { extend: 'csv' },
                    { extend: 'excel', title: 'AppointmentFile' },
                    { extend: 'pdf', title: 'AppointmentFile' },
                    {
                        extend: 'print',
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');

                            $(win.document.body).find('table')
                                    .addClass('compact')
                                    .css('font-size', 'inherit');
                        }
                    }],
                    "columns": [
                        { "data": "PATIENT_NAME" },
                        { "data": "DOCTOR_NAME" },
                        { "data": "APPOINTMENT_DATE" },
                        { "data": "APPOINTMENT_TIME" },
                        { "data": "APPOINTMENT_ID", "render": function (data, type, full, meta) { return '<a href="Invoice.aspx?billid=' + data + '" title="Click here to view bill.">View Bill</a>&nbsp;&nbsp;<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>'; } },
                    ]
                });
            },
            error: function (err) {
                alert(err);
            }
        });
    }
}

