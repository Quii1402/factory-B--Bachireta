function SweetAlert(type, content) {
    Swal.fire({
        text: content,
        icon: type,
        buttonsStyling: false,
        confirmButtonText: "Ok",
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function SweetAlertYesNo(title, content, type, yesContent) {
    Swal.fire({
        title: title,
        text: content,
        icon: type,//'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: yesContent
    }).then((result) => {
        return result.isConfirmed;
    })
}