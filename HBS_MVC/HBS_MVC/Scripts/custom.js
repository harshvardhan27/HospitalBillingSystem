$(document).ready(function () {
    $('.input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd-mm-yyyy'
    });

    $('.chosen-select').chosen({
        width: "100%",
        allow_single_deselect: true
    });

    $('.clockpicker').clockpicker();

});