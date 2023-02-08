var Product = function () {
    var dt;

    var initDatatable = function () {
        dt = $("#table_product").DataTable({
            searchDelay: 500,
            processing: true,
            searching: true,
            ajax: {
                url: "../ProductSchedule/GetProduct",
                type: "POST",
                data: "",
                dataSrc: "",

            },
            columns: [
                { data: 'id' },
                { data: 'line_code' },
                { data: 'product_item' },
                { data: 'seq' },
                { data: 'status' },
                {
                    data: {
                        id: 'id'
                    } 
                }

            ],
            columnDefs: [
                { "defaultContent": "-", "targets": "_all" },
                {
                    targets: -1,
                    orderable: false,
                    className: 'text-end',
                    width: '200px',
                    render: function (data, type, row) {
                        //console.log(data);
                        return `
                            <button class ="btn btn-primary p-2" onclick="complete(${data.id});"">
                                Finished
                            </button>
                            <button class ="btn btn-success p-2" onclick="product(${data.id});"">
                                Product
                            </button>
                            `;
                    }
                }
            ],
        });
    }

    var handleUpload = function () {
        const uploadBtn = $('#btn_modal_add_product');
        uploadBtn.click(function (e) {
            $('#upl-product-list-modal').modal('show');
        });
    }

    return {
        init: function () {
            initDatatable();
            handleUpload();
        }
    }

}();

KTUtil.onDOMContentLoaded(function () {
    Product.init();
    var dropzoneInit = new Dropzone("#kt_file_upload_pl", {
        url: "../ProductSchedule/Upload", // Set the url for your upload script location
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
            const rs = JSON.parse(result.xhr.response);

            $("#btn_insert").attr("data-kt-indicator", "off");
            $("#btn_insert").prop("disabled", false);

            SweetAlert(rs.type, rs.message);

            if (rs.type == "success") {
                $('#upl-product-list-modal').modal('hide');
                Dropzone.forElement('#kt_file_upload_pl').removeAllFiles(true);
            }
            $('#table_product').DataTable().ajax.reload();
        },
    });

    $("#btn_insert").click(function () {
        var $this = $(this);

        dropzoneInit.enqueueFiles(dropzoneInit.getFilesWithStatus(Dropzone.ADDED));
        $this.attr("data-kt-indicator", "on");
        $this.prop("disabled", true);
    });

});

function complete(id) {
    $.ajax({
        type: "POST",
        data: {
            id: id
        },
        url: "../ProductSchedule/UpdateStatusComplete",
        success: function (response) {
            SweetAlert(response.type, response.message)
            $('#table_product').DataTable().ajax.reload();
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function product(id) {
    $.ajax({
        type: "POST",
        data: {
            id: id
        },
        url: "../ProductSchedule/UpdateStatusProduct",
        success: function (response) {
            SweetAlert(response.type, response.message)
            $('#table_product').DataTable().ajax.reload();
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

//Search
$(document).ready(function () {
    var oTable = $('#table_product').DataTable();
    $('#search-btn').click(function () {
        oTable.columns(1).search($("#sel_line").val()).draw();
        oTable.columns(2).search($("#inp-product-item").val()).draw();
       
    });

    //linecode
    $("#inp-line").keypress(function (e) {
        // You can use $(this) here, since this once again refers to your text input            
        if (e.which === 1) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });

    // product_item
    $("#inp-product-item").keypress(function (f) {
        // You can use $(this) here, since this once again refers to your text input            
        if (f.which === 2) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });   
});

