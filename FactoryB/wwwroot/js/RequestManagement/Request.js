var dt;

var DeleteRequest = function (id) {
    Swal.fire({
        title: "Xác nhận xoá",
        text: "Bạn có chắc chắn muốn xoá yêu cầu " + id + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Hủy bỏ',
        confirmButtonText: 'Xác nhận'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                data: {
                    id: id,
                },
                url: "../RequestList/DeleteRequest",
                success: function (response) {
                    ToastrAlertTopRight(response.type, response.message);
                    if (response.type == "success") {
                        $('#table_request').DataTable().ajax.reload();
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

var RequestManagement = function () {
    var InitDataTable = function () {
        dt = $("#table_request").DataTable({
            searchDelay: 500,
            processing: true,
            ajax: {
                url: "../RequestList/GetRequest",
                type: "POST",
                dataSrc: "",
                error: function (error) {
                    // to see what the error is
                    ToastrAlertTopRight("error", error);
                }
            },
            columns: [
                { data: "id_request" },
                { data: "line_code" },
                { data: "item_code" },
                { data: "request_qty" },
                { data: "read_qty" },
                { data: "status_name" },
                { data: "last_modify_by" },
                { data: "last_modify_at" },
                { data: "id_request" },
            ],
            columnDefs: [
                {
                    targets: [-2],
                    render: function (data) {
                        if (data == null)
                            return "";
                        return moment(data).format('YYYY-MM-DD HH:mm:ss');
                    }
                },

                {
                    targets: [-1],// Disable ordering on column 6 (actions)
                    orderable: false,
                    render: function (data) {
                        return `                          
                            <a href="javascript:;" onclick='DeleteRequest("${data}");' class="btn btn-icon btn-active-light-danger btn-sm"><span class="svg-icon svg-icon-1"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                            <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="black"/>
                            <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="black"/>
                            <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="black"/>
                            </svg></span>
                            </a>`;
                    }
                },
            ],
            lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "All"]],
            buttons: ["copy", "excel", "pdf"],
            initComplete: function () {
                dt.buttons().container().appendTo("#dt_tools");
            }
        });
        setInterval(function () {
            dt.ajax.reload();
        },
            15000);
    };


    var HandleSearchDatatable = function () {
        $('#input_search').keyup(function (e) {
            dt.search(e.target.value).draw();
        });
    };

    //upload file excel
    var HandleAddButton = function () {
        $('#btn_modal_add_request').on('click', function () {
            $('#upl-request-list-modal').modal('show');
        });
    }

    //thêm từng request
    var HandleInsertButton = function () {
        $('#btn_modal_insert_request').on('click', function () {
            $('#kt-modal-insert').modal('show');
        });
    }

    // Filter Datatable
    var HandleFilter = function () {
        // Select filter options
        const filterForm = document.querySelector('[data-table-request-filter="form"]');
        const filterButton = $('#btn_filter');
        const resetButton = $('#btn_reset_filter');
        const selectOptions = filterForm.querySelectorAll('select');

        // Filter datatable on submit
        filterButton.on('click', function () {
            var filterString = '';

            // Get filter values
            selectOptions.forEach((item, index) => {
                if (item.value && item.value !== '') {
                    if (index !== 0) {
                        filterString += ' ';
                    }

                    // Build filter value options
                    filterString += item.value;
                }
            });

            // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
            dt.search(filterString).draw();
        });

        // Reset datatable
        resetButton.on('click', function () {
            // Reset filter form
            selectOptions.forEach((item, index) => {
                // Reset Select2 dropdown --- official docs reference: https://select2.org/programmatic-control/add-select-clear-items
                $(item).val(null).trigger('change');
            });

            // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
            dt.search('').draw();
        });
    }

    return {
        // Public functions  
        Init: function () {
            InitDataTable();
            HandleSearchDatatable();
            HandleFilter();
            HandleAddButton();
            HandleInsertButton();
        }
    }
}();


function ChangeLine() {
    $("#inp-product-item-modal-insert").val("");
    $("#inp-part-item-modal-insert").val("");
    var line = $('#select2-select_line-container').text();

    if (line == "") {
        SweetAlert("error", "Hãy nhập chuyền")
    }
    else {
        $.ajax({
            type: "POST",
            data: { line_code: line },
            url: "../ProductSchedule/GetProductItem",
            success: function (data) {
                $("#inp-product-item-modal-insert").val(data[0].product_item);
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });
    }
}

function changeTypeItem(typeItem) {
    var product_item = $('#inp-product-item-modal-insert').val();
    var typeItemval = typeItem.value;

    $.ajax({
        type: "POST",
        data: { type_item: typeItem.value, product_item: product_item },
        url: "../RequestList/GetBomModelByProductItem",
        success: function (data) {
            $("#inp-part-item-modal-insert").val(data[0].parts_item);
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

var insertNewPartInfo = function () {
    $('#btn-insert').click(function () {
        let line = $('#select2-select_line-container').text();
        let itemCode = $('#inp-part-item-modal-insert').val();

        let request_qty = $('#inp-qty-modal-insert').val();

        if (line == "") {
            SweetAlert("error", "Hãy nhập chuyền")
        }
        else if (itemCode == "") {
            SweetAlert("error", "Hãy nhập mã linh kiện")
        }
        else {
            $.ajax({
                type: "POST",
                data: { line_code: line, item_code: itemCode, request_qty: request_qty },
                url: "../RequestList/InsertRequest",
                success: function (response) {
                    //SweetAlert(response.type, response.message);
                    $('#kt-modal-insert').modal('hide');
                    SweetAlert(response.type, response.message)
                    dt.ajax.reload();
                },
                failure: function (response) {
                    console.log(response);
                },
                error: function (response) {
                    console.log(response);
                }
            });
            closeInsertModal();
        }
    });

}

var closeInsertModal = function () {
    $('#kt-modal-insert').modal('hide');
}

KTUtil.onDOMContentLoaded((function () {
    RequestManagement.Init();
    insertNewPartInfo();
    closeInsertModal();
    var dropzoneInit = new Dropzone("#kt_file_upload_pl", {
        url: "../RequestList/Upload", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 1,
        maxFilesize: 20, // MB
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
            const rs = JSON.parse(result.xhr.response);
            $("#btn_insert").attr("data-kt-indicator", "off");
            $("#btn_insert").prop("disabled", false);
            if (rs.type = "success") {
                $('#upl-request-list-modal').modal('hide');
                Dropzone.forElement('#kt_file_upload_pl').removeAllFiles(true);
                var json_bytes = JSON.parse(rs.message);

                var binaryString = window.atob(json_bytes);
                var binaryLen = binaryString.length;
                var bytes = new Uint8Array(binaryLen);
                for (var i = 0; i < binaryLen; i++) {
                    var ascii = binaryString.charCodeAt(i);
                    bytes[i] = ascii;
                }

                var blob = new Blob([bytes], { type: "application/octet-stream" });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                var fileName = "upload_request_result.xlsx";
                link.download = fileName;
                link.click();
                dt.ajax.reload();
            } else {
                SweetAlert(rs.type, rs.message);
            }

        },
    });

    $("#btn_add").click(function () {
        var $this = $(this);

        dropzoneInit.enqueueFiles(dropzoneInit.getFilesWithStatus(Dropzone.ADDED));
        $this.attr("data-kt-indicator", "on");
        $this.prop("disabled", true);
    });

}));
