var table;

var removeLocation = function (locaton_code) {
    Swal.fire({
        title: "Xác nhận xoá",
        text: "Bạn có chắc chắn muốn xóa không? ",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Hủy bỏ',
        confirmButtonText: 'Xác nhận'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "../Location/DeleteLocation",
                type: "DELETE",
                data: {
                    location_code: locaton_code
                },

                success: function (response) {
                    ToastrAlertTopRight(response.type, response.message);
                    if (response.type == "success") {
                        $('#kt_datatable_location').DataTable().ajax.reload();
                    }
                },
                failure: function (response) {
                    console.log(response);
                },
                error: function (response) {
                    console.log(response);
                }
            });
        }
    })
}

var ShowModalEdit = function (locationCode, description) {
    $('#kt-modal-edit').modal('show');
    $('#lbl-location-code').text(locationCode);

    $('#lbl-description-modal-edit').text((description == "null") ? " " : description);
    $('#inp-description-modal-edit').val((description == "null") ? "" : description);

}

var closeEditModal = function () {
    $('#kt-modal-edit').modal('hide');
}


//Class definition
var ViewLocations = function () {
    // Shared variables
    var dt;
    // Private functions
    var initDatatable = function () {
        dt = $("#kt_datatable_location").DataTable({
            searchDelay: 500,
            processing: true,
            ajax: {
                url: "../Location/GetLocationCode",
                type: "POST",
                dataSrc: "",
                error: function (error) {
                    // to see what the error is
                    console.log(error);
                }
            },
            columns: [
                { data: "location_code" },
                { data: "description" },
                { data: "last_modify_by" },
                { data: "last_modify_at" },
                {
                    data: {
                        location_code: "location_code",

                        description: "description",

                    }
                }

            ],
            columnDefs: [
                { "defaultContent": "-", "targets": "_all" },
                {
                    targets: [0, 1],
                    width: 150
                },

                {
                    targets: [-2],
                    render: function (data) {
                        if (data == null)
                            return "";
                        return moment(data).format('YYYY-MM-DD HH:mm:ss');
                    }
                },
                {
                    targets: -1,
                    data: null,
                    orderable: false,
                    className: 'text-end',
                    render: function (data, type, row) {
                        return `
                            <a href="javascript:;" onclick='ShowModalEdit("${data.location_code}","${data.description}");' class="btn btn-icon btn-active-light-info btn-sm"><span class="svg-icon svg-icon-1"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                            <path opacity="0.3" fill-rule="evenodd" clip-rule="evenodd" d="M2 4.63158C2 3.1782 3.1782 2 4.63158 2H13.47C14.0155 2 14.278 2.66919 13.8778 3.04006L12.4556 4.35821C11.9009 4.87228 11.1726 5.15789 10.4163 5.15789H7.1579C6.05333 5.15789 5.15789 6.05333 5.15789 7.1579V16.8421C5.15789 17.9467 6.05333 18.8421 7.1579 18.8421H16.8421C17.9467 18.8421 18.8421 17.9467 18.8421 16.8421V13.7518C18.8421 12.927 19.1817 12.1387 19.7809 11.572L20.9878 10.4308C21.3703 10.0691 22 10.3403 22 10.8668V19.3684C22 20.8218 20.8218 22 19.3684 22H4.63158C3.1782 22 2 20.8218 2 19.3684V4.63158Z" fill="black"/>
                            <path d="M10.9256 11.1882C10.5351 10.7977 10.5351 10.1645 10.9256 9.77397L18.0669 2.6327C18.8479 1.85165 20.1143 1.85165 20.8953 2.6327L21.3665 3.10391C22.1476 3.88496 22.1476 5.15129 21.3665 5.93234L14.2252 13.0736C13.8347 13.4641 13.2016 13.4641 12.811 13.0736L10.9256 11.1882Z" fill="black"/>
                            <path d="M8.82343 12.0064L8.08852 14.3348C7.8655 15.0414 8.46151 15.7366 9.19388 15.6242L11.8974 15.2092C12.4642 15.1222 12.6916 14.4278 12.2861 14.0223L9.98595 11.7221C9.61452 11.3507 8.98154 11.5055 8.82343 12.0064Z" fill="black"/>
                            </svg></span>
                            </a>
                            <a href="javascript:;" onclick='removeLocation("${data.location_code}");' class="btn btn-icon btn-active-light-danger btn-sm"><span class="svg-icon svg-icon-1"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                            <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="black"/>
                            <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="black"/>
                            <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="black"/>
                            </svg></span>
                            </a>
                        `;

                    },
                },
            ],
            lengthMenu: [[5, 20, -1], [5, 15, "All"]],
            buttons: ["copy", "excel", "pdf"],
            initComplete: function () {
                dt.buttons().container().appendTo("#dt_tools");
            }
        });

        table = dt.$;
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = function () {
        const filterSearch = document.querySelector('[data-kt-location-table-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            dt.search(e.target.value).draw();
        });
    }

    var HandleAddButton = function () {
        $('#btn_modal_add_location').on('click', function () {
            $('#upl-location-list-modal').modal('show');
        });
    }
    function initEvent() {
        const updateBtn = document.getElementById('btn-update');
        updateBtn.addEventListener('click', function () {

            let description = $('#inp-description-modal-edit').val();

            $.ajax({
                type: "POST",
                data: { location_code: $('#lbl-location-code').text(), description: $('#inp-description-modal-edit').val() },
                url: "../Location/UpdateLocation",
                success: function (response) {
                    //SweetAlert(response.type, response.message);
                    $('#kt-modal-edit').modal('hide');
                    SweetAlert(response.type, response.message);

                    $("#kt_datatable_location").DataTable().ajax.reload();
                },
                failure: function (response) {
                    console.log(response);
                },
                error: function (response) {
                    console.log(response);
                }
            });
            closeEditModal();

        });
    }
    // Public methods
    return {
        init: function () {
            initDatatable();
            handleSearchDatatable();
            HandleAddButton();
            initEvent();

        }
    }
}();



// On document ready
KTUtil.onDOMContentLoaded(function () {
    ViewLocations.init();
    var dropzoneInit = new Dropzone("#kt_file_upload_pl", {
        url: "../Location/Upload", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 1,
        maxFilesize: 10, // MB
        autoQueue: false,
        addRemoveLinks: true,
        accept: function (file, done) {
            if (file.name == "wow.jpg") {
                done("Naha, you don't.");
            } else {
                done();
            }
        },
        success: function (result) {
            //const rs = JSON.parse(result.xhr.response);
            //$("#btn_insert").attr("data-kt-indicator", "off");
            //$("#btn_insert").prop("disabled", false);
            //if (rs.type = "success") {
            //    $('#upl-request-list-modal').modal('hide');
            //    Dropzone.forElement('#kt_file_upload_pl').removeAllFiles(true);
            //    var json_bytes = JSON.parse(rs.message);

            //    var binaryString = window.atob(json_bytes);
            //    var binaryLen = binaryString.length;
            //    var bytes = new Uint8Array(binaryLen);
            //    for (var i = 0; i < binaryLen; i++) {
            //        var ascii = binaryString.charCodeAt(i);
            //        bytes[i] = ascii;
            //    }

            //    var blob = new Blob([bytes], { type: "application/octet-stream" });
            //    var link = document.createElement('a');
            //    link.href = window.URL.createObjectURL(blob);
            //    var fileName = "location-list-template.xlsx";
            //    link.download = fileName;
            //    link.click();
            //    dt.ajax.reload();
            //} else {
            //    SweetAlert(rs.type, rs.message);
            //}
            const rs = JSON.parse(result.xhr.response);

            $("#btn_insert").attr("data-kt-indicator", "off");
            $("#btn_insert").prop("disabled", false);

            SweetAlert(rs.type, rs.message);

            if (rs.type == "success") {
                $('#upl-request-list-modal').modal('hide');
                Dropzone.forElement('#kt_file_upload_pl').removeAllFiles(true);
            }
            $('#kt_datatable_location').DataTable().ajax.reload();
        },
    });

    $("#btn_insert").click(function () {
        var $this = $(this);

        dropzoneInit.enqueueFiles(dropzoneInit.getFilesWithStatus(Dropzone.ADDED));
        $this.attr("data-kt-indicator", "on");
        $this.prop("disabled", true);
    });
});

