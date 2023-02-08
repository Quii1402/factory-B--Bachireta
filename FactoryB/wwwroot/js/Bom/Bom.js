//GET ITEM
var Bom = function () {
    var dt;
    let initialTable = true;

    var initDatatable = function () {
        dt = $("#tbBom").DataTable({
            searchDelay: 500,
            processing: true,
            searching: true,
            ajax: {
                url: "../Bom/GetBom",
                type: "POST",
                data: "",
                dataSrc: "",

            },
            columns: [
                { data: 'product_item' },
                { data: 'parts_item' },
                { data: 'type_item' },
                { data: 'box_type' },
                { data: 'qty_per_box' },
                { data: 'status' },
                { data: 'last_modify_by' },
                { data: 'last_modify_at' },
                {
                    data: {
                        Product_Item: "product_item",
                        Parts_Item: "parts_item",
                        Type_Item: "type_item",
                        Box_Type: "box_type",
                        Qty_Per_Box: "qty_per_box",
                        Status: "status",
                        Last_modify_by: "last_modify_by",
                        Last_modify_at: "last_modify_at",

                    }
                }

            ],
            columnDefs: [
                { "defaultContent": "-", "targets": "_all" },
             
              
                {
                    targets: [7],
                    render: function (data) {
                        if (data == null)
                            return "";
                        return moment(data).format('YYYY-MM-DD HH:mm:ss');
                    }
                },
                {
                    targets: [5],
                    visible: false,
                },       
                {
                    targets: -1,
                    orderable: false,
                    className: 'text-end',
                    render: function (data, type, row) {
                        console.log(data);
                        return `
                            <button onclick="ShowModalEdit('${data.product_item                      
                            }','${data.parts_item
                            }','${data.type_item
                            }','${data.box_type
                            }','${data.qty_per_box
                            }','${data.status
                            }','${data.last_modify_by
                            }','${data.last_modify_at

                            }');"

                                title="Cập nhật"
                                class="btn btn-icon btn-active-light-danger btn-sm">
                                <span class="svg-icon svg-icon-muted svg-icon-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                                <path opacity="0.3" fill-rule="evenodd" clip-rule="evenodd" d="M2 4.63158C2 3.1782 3.1782 2 4.63158 2H13.47C14.0155 2 14.278 2.66919 13.8778 3.04006L12.4556 4.35821C11.9009 4.87228 11.1726 5.15789 10.4163 5.15789H7.1579C6.05333 5.15789 5.15789 6.05333 5.15789 7.1579V16.8421C5.15789 17.9467 6.05333 18.8421 7.1579 18.8421H16.8421C17.9467 18.8421 18.8421 17.9467 18.8421 16.8421V13.7518C18.8421 12.927 19.1817 12.1387 19.7809 11.572L20.9878 10.4308C21.3703 10.0691 22 10.3403 22 10.8668V19.3684C22 20.8218 20.8218 22 19.3684 22H4.63158C3.1782 22 2 20.8218 2 19.3684V4.63158Z" fill="black"></path>
                                <path d="M10.9256 11.1882C10.5351 10.7977 10.5351 10.1645 10.9256 9.77397L18.0669 2.6327C18.8479 1.85165 20.1143 1.85165 20.8953 2.6327L21.3665 3.10391C22.1476 3.88496 22.1476 5.15129 21.3665 5.93234L14.2252 13.0736C13.8347 13.4641 13.2016 13.4641 12.811 13.0736L10.9256 11.1882Z" fill="black"></path><path d="M8.82343 12.0064L8.08852 14.3348C7.8655 15.0414 8.46151 15.7366 9.19388 15.6242L11.8974 15.2092C12.4642 15.1222 12.6916 14.4278 12.2861 14.0223L9.98595 11.7221C9.61452 11.3507 8.98154 11.5055 8.82343 12.0064Z" fill="black"></path>
                                </svg></span>
                            </button>
                            <a href="javascript:;" onclick='removeItem("${data.product_item}");' class="btn btn-icon btn-active-light-danger btn-sm"><span class="svg-icon svg-icon-1"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                            <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="black"/>
                            <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="black"/>
                            <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="black"/>
                            </svg></span>
                            </a>`;
                    }
                }
            ],

            lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "All"]],
            buttons: ["copy", "excel", "pdf"],
            initComplete: function () {
                $('#tbBom').DataTable().buttons().container().appendTo("#dt_tools");
            }
        });
    }

    var HandleSearchDatatable = function () {
        $('#input_search').keyup(function (e) {
            $('#tbBom').DataTable().search(e.target.value).draw();
        });
    };
    var handleUpload = function () {
        const uploadBtn = $('#btn_upload_icm');
        uploadBtn.click(function (e) {
            $('#upl-item-code-manager-modal').modal('show');
        });
    }

    return {
        init: function () {
            initDatatable();
            HandleSearchDatatable();        
            Update();           
            handleUpload();
        }
    }

}();
//On document loaded
KTUtil.onDOMContentLoaded(function () {
    Bom.init();

    var dropzoneInit = new Dropzone("#kt_file_upload_icm", {
        url: "../Bom/UploadBom", // Set the url for your upload script location
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
                $('#upl-item-code-manager-modal').modal('hide');
                Dropzone.forElement('#kt_file_upload_icm').removeAllFiles(true);
            }
            $('#tbBom').DataTable().ajax.reload();
        },
    });

    $("#btn_insert").click(function () {
        var $this = $(this);
        dropzoneInit.enqueueFiles(dropzoneInit.getFilesWithStatus(Dropzone.ADDED));
        $this.attr("data-kt-indicator", "on");
        $this.prop("disabled", true);
    });

})

//SHOW MODAL EDIT
var ShowModalEdit = function (product_item, parts_item, type_item, box_type, qty_per_box, description) {
    $('#kt-modal-edit').modal('show');
    document.getElementById("ID-product-item").innerHTML = product_item;
    document.getElementById("ID-parts-item").innerHTML = parts_item;


    $('#inp-product-item-edit').val((product_item == "null") ? "" : product_item);
    $('#inp-parts-item-edit').val((parts_item == "null") ? "" : parts_item);

    $('#lbl-type-item-edit').text((type_item == "null") ? "" : type_item);
    $('#inp-type-item-edit').val((type_item == "null") ? "" : type_item);
    $('#lbl-box-type-edit').text((box_type == "null") ? "" : box_type);
    $('#inp-box-type-edit').val((box_type == "null") ? "" : box_type);
    $('#lbl-qty-per-box-edit').text((qty_per_box == "null") ? "" : qty_per_box);
    $('#inp-qty-per-box-edit').val((qty_per_box == "null") ? "" : qty_per_box);

}
// close model
var closeEditModal = function () {
    $('#kt-modal-edit').modal('hide');
}

//Update
var Update = function () {
    const UpdateBtn = document.getElementById('btn-update');
    UpdateBtn.addEventListener('click', function () {
        let Product_item = $('#inp-product-item-edit').val();
        let Parts_Item = $('#inp-parts-item-edit').val();
        let Type_Item = $('#inp-type-item-edit').val();
        let Box_Type = $('#inp-box-type-edit').val();
        let Qty_Per_Box = $('#inp-qty-per-box-edit').val();
      

        $.ajax({
            type: "PUT",
            data: {
                product_item: Product_item, parts_item: Parts_Item, type_item: Type_Item,
                box_type: Box_Type, qty_per_box: Qty_Per_Box,
               

            },
            url: "../Bom/Updatebom",
            success: function (response) {

                //SweetAlert(response.type, response.message);
                $('#kt-modal-edit').modal('hide');
                SweetAlert(response.type, response.message)
                $('#tbBom').DataTable().ajax.reload();
            },
            failure: function (response) {

                console.log(response);
            },
            error: function (response) {

                console.log(response);
            }
        });
        closeEditModal();              
    })
}


// Delete
var removeItem = function (Product_Item) {
    Swal.fire({
        title: "Xác nhận xoá",
        text: "Bạn có chắc chắn muốn xóa mã này không ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Xác nhận'
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                data: {
                    product_item: Product_Item,
                 

                },

                url: "../Bom/DeleteBom",
                success: function (response) {
                    ToastrAlertTopRight(response.type, response.message);
                    if (response.type == "success") {
                        $('#tbBom').DataTable().ajax.reload();
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


//Search
$(document).ready(function () {
    var oTable = $('#tbBom').DataTable();
    $('#search-btn').click(function () {
        oTable.columns(0).search($("#inp-product-item").val()).draw();
        oTable.columns(1).search($("#inp-parts-item").val()).draw();
        oTable.columns(2).search($("#inp-type-item").val()).draw();
    });

    //ITEMCODE
    $("#inp-product-item").keypress(function (e) {
        // You can use $(this) here, since this once again refers to your text input            
        if (e.which === 1) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });

    // LOCATION
    $("#inp-parts-item").keypress(function (f) {
        // You can use $(this) here, since this once again refers to your text input            
        if (f.which === 2) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });

    // QTY
    $("#inp-type-item").keypress(function (e) {
        // You can use $(this) here, since this once again refers to your text input            
        if (e.which === 3) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });
});