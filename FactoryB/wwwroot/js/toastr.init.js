////toastr.options = {
////    "closeButton": true,
////    "debug": false,
////    "newestOnTop": false,
////    "progressBar": false,
////    "positionClass": "toast-top-full-width",
////    "preventDuplicates": false,
////    "onclick": null,
////    "showDuration": 300,
////    "hideDuration": 1000,
////    "timeOut": 5000,
////    "extendedTimeOut": 1000,
////    "showEasing": "swing",
////    "hideEasing": "linear",
////    "showMethod": "fadeIn",
////    "hideMethod": "fadeOut"
////}

function ToastrAlert(type, content) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toastr-top-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    switch (type) {
        case "info": toastr["info"](moment().format("YYYY-MM-DD hh:mm:ss") + " - IF: " + content);
            break;
        case "warning": toastr["warning"](moment().format("YYYY-MM-DD hh:mm:ss") + " - WN: " + content);
            break;
        case "error": toastr["error"](moment().format("YYYY-MM-DD hh:mm:ss") + " - NG: " + content);
            break;
        case "success": toastr["success"](moment().format("YYYY-MM-DD hh:mm:ss") + " - OK: " + content);
            break;
    }
}

function ToastrAlertTopRight(type, content) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toastr-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    switch (type) {
        case "info": toastr["info"](moment().format("YYYY-MM-DD hh:mm:ss") + " - IF: " + content);
            break;
        case "warning": toastr["warning"](moment().format("YYYY-MM-DD hh:mm:ss") + " - WN: " + content);
            break;
        case "error": toastr["error"](moment().format("YYYY-MM-DD hh:mm:ss") + " - NG: " + content);
            break;
        case "success": toastr["success"](moment().format("YYYY-MM-DD hh:mm:ss") + " - OK: " + content);
            break;
    }
}

function ToastrAlertUnderDevelopment() {
    toastr["info"]("This feature is under development");
}