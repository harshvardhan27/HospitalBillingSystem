$(document).ready(function () {
    appAppointment.init();
});
var datatableResponse = "";
var aoData = "";

var appAppointment = {
    init: function () {
        this.hideOnInitialLoad();
        this.bindEvents();
        this.bindDataTable();
    },
    hideOnInitialLoad: function () {
        $('#panelContent').length > 0 ? ($('#appointmentInfo,#errorPanel').show(), $(window).scrollTop($('#appointmentInfo').offset().top), (window.setTimeout(function () {
            $("#errorPanel").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000))) : $('#appointmentInfo,#errorPanel').hide();
    },
    bindEvents: function () {

        this.$addNewRecord = $('#addNewRecord');
        var addNewRecord = this.$addNewRecord;
        addNewRecord.on('click', this.addNewRecord);

        this.$btnCancel = $('#btnCancel');
        var btnCancel = this.$btnCancel;
        btnCancel.on('click', this.btnCancel);

        $('#ddlPatient').chosen().change(function () {
            $('#hidSelectedPatient').val('');
            var selectedPatient = $('#ddlPatient').val();
            if (selectedPatient > 0) {
                $('#hidSelectedPatient').val(selectedPatient);
            }
        });

        $('#ddlDoctor').chosen().change(function () {
            $('#hidSelectedDoctor').val('');
            var selectedDoctor = $('#ddlDoctor').val();
            if (selectedDoctor > 0) {
                $('#hidSelectedDoctor').val(selectedDoctor);
            }
        });

        $('#ddlProcedure').chosen().change(function () {
            $('#hidSelectedProcedure').val('');
            var selectedProcedure = $('#ddlProcedure').val();
            /*if (selectedProcedure > 0) {
                $('#hidSelectedProcedure').val(selectedProcedure);
            }*/
            $('#hidSelectedProcedure').val(selectedProcedure);
            //alert($('#hidSelectedProcedure').val());
        });



        $('#errorPanel .close').on('click', function () {
            $('#errorPanel').hide();
        });
    },

    clearAppointment: function () {
        $('#hidPrimaryKey').val('');
        $('#hidSelectedPatient').val('');
        $('#hidSelectedDoctor').val('');
        $('#hidSelectedProcedure').val('');
        $('#ddlPatient').val('').trigger('chosen:updated');
        $('#ddlDoctor').val('').trigger('chosen:updated');
        $('#ddlProcedure').val('').trigger('chosen:updated');
        $('#txtAppointmentDate').val('');
        $('#txtAppointmentTime').val('');
        $('#btnDelete').hide();
        $('#errorPanel').hide();
    },

    addNewRecord: function (e) {
        e.preventDefault();
        appAppointment.clearAppointment();
        $('#appointmentInfo').slideDown();
    },

    btnCancel: function (e) {
        e.preventDefault();
        appAppointment.clearAppointment();
        $('#appointmentInfo').slideUp();
    },

    bindDataTable: function () {
        $.ajax({
            type: "POST",
            url: "Appointment.aspx/BindDataTable",
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
                    ]
                });
                $('#dtAppointment tbody').on('click', 'tr', function () {
                    aoData = datatableResponse.row(this).data();
                    appAppointment.fillData(aoData);
                });
            },
            error: function (err) {
                alert(err);
            }
        });
    },

    fillData: function (aData) {
        appAppointment.clearAppointment();
        if (aData !== "") {
            $('#appointmentInfo').slideDown();
            $('#hidPrimaryKey').val(aData.APPOINTMENT_ID);
            $('#ddlPatient').val(aData.PATIENT_ID).trigger('chosen:updated');
            $('#hidSelectedPatient').val($('#ddlPatient').val());
            $('#ddlDoctor').val(aData.DOCTOR_ID).trigger('chosen:updated');
            $('#hidSelectedDoctor').val($('#ddlDoctor').val());
            //$('#ddlProcedure').val(['2','9','4']).trigger('chosen:updated');
            $('#ddlProcedure').val(appAppointment.getProcedures(aData)).trigger('chosen:updated');
            $('#hidSelectedProcedure').val($('#ddlProcedure').val());
            $('#txtAppointmentDate').val(aData.APPOINTMENT_DATE);
            $('#txtAppointmentTime').val(aData.APPOINTMENT_TIME);
            if ($('#hidPrimaryKey').val() !== "") {
                $('#btnDelete').show();
            }
        }
    },

    getProcedures: function (aData) {
        if (aData != null) {
            var selectedProcedureList = aData.PROCEDURE_ID;
            var str_ProcedureArray = selectedProcedureList.split(',');
            for (var i = 0; i < str_ProcedureArray.length; i++) {
                str_ProcedureArray[i] = str_ProcedureArray[i].replace(/^\s*/, "").replace(/\s*$/, "");
            }
        }       
        return str_ProcedureArray;
    },

    formatDate: function (date) {
        if (date !== "") {
            var day, month, year, sdate;
            if ($(date).conatins('-')) {
                day = date.split('-')[2];
                month = date.split('-')[1];
                year = date.split('-')[0];
                sdate = day + "-" + month + "-" + year;
                return sdate;
            }
            else if ($(date).contains('/')) {
                day = date.split('/')[1];
                month = date.split('/')[0];
                year = date.split('/')[2];
                sdate = day + "-" + month + "-" + year;
                return sdate;
            }
        }
        else { return sdate = " "; }
    }
}

