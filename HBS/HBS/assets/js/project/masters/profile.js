$(document).ready(function () {
    appProfile.init();
});
var datatableResponse = "";
var aoData = "";

var appProfile = {
    init: function () {
        this.hideOnInitialLoad();
        this.bindEvents();
        this.bindDataTable();
    },
    hideOnInitialLoad: function () {
        //$('#patientInfo').hide();
        $('#panelContent').length > 0 ? ($('#patientInfo,#errorPanel').show(), $(window).scrollTop($('#patientInfo').offset().top), (window.setTimeout(function () {
            $("#errorPanel").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000))) : $('#patientInfo,#errorPanel').hide();
    },
    bindEvents: function () {

        /*$('#panelContent').length > 0 ? (window.setTimeout(function () {
            $("#errorPanel").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000)) : "";*/

        this.$addNewRecord = $('#addNewRecord');
        var addNewRecord = this.$addNewRecord;
        addNewRecord.on('click', this.addNewRecord);

        this.$btnCancel = $('#btnCancel');
        var btnCancel = this.$btnCancel;
        btnCancel.on('click', this.btnCancel);

        /*$(".select2").select2({
            placeholder: "Select a Province",
            allowClear: true
        });

        $('#ddlProvince').on('change', function () {
            //$('#<%=ddlProvince.ClientID%>').val($(this).val());
            $('#hidSelectedProvince').val('');
            var selectedProvince = $('#ddlProvince').val();
            if (selectedProvince > 0) {
                $('#hidSelectedProvince').val(selectedProvince);
            }
        });*/
        $('#ddlProvince').chosen().change(function () {
            $('#hidSelectedProvince').val('');
            var selectedProvince = $('#ddlProvince').val();
            if (selectedProvince > 0) {
                $('#hidSelectedProvince').val(selectedProvince);
            }
        });

        $('#errorPanel .close').on('click', function () {
            $('#errorPanel').hide();
        });
    },

    clearProfile: function () {
        $('#hidPrimaryKey').val('');
        $('#hidSelectedProvince').val('');
        $('#txtFirstname').val('');
        $('#txtLastname').val('');
        $('#txtDob').val('');
        $('#txtEmailId').val('');
        $('#txtContactNo').val('');
        $('#txtAppartment').val('');
        $('#txtStreet').val('');
        $('#txtCity').val('');
        //$('#ddlProvince').select2("val", "");
        $('#ddlProvince').val('').trigger('chosen:updated');
        $('#txtPostalCode').val('');
        $('#btnDelete').hide();
        $('#errorPanel').hide();
    },

    addNewRecord: function (e) {
        e.preventDefault();
        appProfile.clearProfile();
        $('#patientInfo').slideDown();
    },

    btnCancel: function (e) {
        e.preventDefault();
        appProfile.clearProfile();
        $('#patientInfo').slideUp();
    },

    bindDataTable: function () {
        $.ajax({
            type: "POST",
            url: "Profile.aspx/BindDataTable",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //datasource = data;
                datatableResponse = $('#dtPatient').DataTable({
                    "aaData": JSON.parse(data.d),
                    "pageLength": 5,
                    "responsive": true,
                    "lengthMenu": [[5, 25, 50, -1], [5, 25, 50, "All"]],
                    "dom": '<"html5buttons"B>lTfgitp',
                    "buttons": [
                    { extend: 'copy' },
                    { extend: 'csv' },
                    { extend: 'excel', title: 'PatientFile' },
                    { extend: 'pdf', title: 'PatientFile' },
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
                        { "data": "NAME" },
                        { "data": "EMAIL" },
                        { "data": "CONTACTNO" }
                    ]
                });
                $('#dtPatient tbody').on('click', 'tr', function () {
                    //console.log(datatableResponse.row(this).data());
                    aoData = datatableResponse.row(this).data();
                    appProfile.fillData(aoData);
                });
            },
            error: function (err) {
                alert(err);
            }
        });
    },

    fillData: function (aData) {
        //alert("Data " + aData.Name);
        appProfile.clearProfile();
        if (aData != "") {
            $('#patientInfo').slideDown();
            $('#hidPrimaryKey').val(aData.PATIENTID);
            $('#txtFirstname').val(aData.FIRSTNAME);
            $('#txtLastname').val(aData.LASTNAME);
            var date = aData.DOB.split(" ")[0];
            $('#txtDob').val(date.split('/')[1] + "-" + date.split('/')[0] + "-" + date.split('/')[2]);
            $('#txtEmailId').val(aData.EMAIL);
            $('#txtContactNo').val(aData.CONTACTNO);
            $('#txtAppartment').val(aData.ADDRESSLINEONE);
            $('#txtStreet').val(aData.ADDRESSLINETWO);
            $('#txtCity').val(aData.CITY);
            //$('#ddlProvince').select2("val", aData.PROVIENCEID);
            $('#ddlProvince').val(aData.PROVIENCEID).trigger('chosen:updated');
            $('#hidSelectedProvince').val($('#ddlProvince').val());
            $('#txtPostalCode').val(aData.POSTALCODE);
            if ($('#hidPrimaryKey').val() != "") {
                $('#btnDelete').show();
            }
        }
    },

    formatDate: function (date) {
        if (date != "") {
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

