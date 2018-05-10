$(document).ready(function () {
    appLogin.init();
});

var appLogin = {
    init: function () {
        this.hideOnInitialLoad();
        this.bindEvents();
    },
    hideOnInitialLoad: function () {

    },
    bindEvents: function () {

        $('#panelContent').length > 0 ? (window.setTimeout(function() {
            $("#errorPanel").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove(); 
            });
        }, 4000)) : "";

        $('#errorPanel .close').on('click', function () {
            $('#errorPanel').hide();
        });
    }
}

