//GET ITEM
var Item = function () {
    var dt;
    let initialTable = true;

    var initDatatable = function () {
        dt = $("#tbItem").DataTable({
            searchDelay: 500,
            processing: true,
            searching: true,
            ajax: {
                url: "/Item/GetItem",
                type: "POST",
                data: "",
                dataSrc: "",
                
            },
            columns: [
                { data: 'item_code' },
                { data: 'item_text' },
                { data: 'status' },
                { data: 'description' },
                { data: 'qty_per_box' },
                { data: 'last_modify_by' },
                { data: 'last_modify_at' },
                {
                    data: {
                        Item_code: "item_code",
                        Item_text: "item_text",
                        Status: "status",
                        Qty_per_box: "qty_per_box",
                        Description: "description",
                        Last_modify_by: "last_modify_by",
                      

                    }
                }

            ],
            columnDefs: [
                { "defaultContent": "-", "targets": "_all" },
                //{
                //    targets: [1],
                //    width: "20%"
                //},
                {
                    targets: [2],
                    visible: false,
                },
                {
                    targets: [-1],
                    width: "15%"
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
                    orderable: false,
                    className: 'text-end',
                    render: function (data, type, row) {
                        console.log(data);
                        return `
                            <button onclick="ShowModalEdit('${data.item_code
                            }','${data.item_text
                            }','${data.description
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
                            <a href="javascript:;" onclick='removeItem("${data.item_code}");' class="btn btn-icon btn-active-light-danger btn-sm"><span class="svg-icon svg-icon-1"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
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
                $('#tbItem').DataTable().buttons().container().appendTo("#dt_tools");
            }
        });
    }

    var HandleSearchDatatable = function () {
        $('#input_search').keyup(function (e) {
            $('#tbItem').DataTable().search(e.target.value).draw();
        });
    };

    // var HandleAddButton = function () {
    //    $('#btn_modal_add_request').on('click', function () {
    //        $('#upl-request-list-modal').modal('show');
    //    });
    //}

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
          /*  insertNewPartInfo();*/
            Update();
            //HandleAddButton();
            handleUpload();
        }
    }

}();


//On document loaded
KTUtil.onDOMContentLoaded(function () {
    Item.init();

    var dropzoneInit = new Dropzone("#kt_file_upload_icm", {
        url: "../Item/UploadItem", // Set the url for your upload script location
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
            $('#tbItem').DataTable().ajax.reload();
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
var ShowModalEdit = function (Item_Code, Item_Text, Description, Qty_per_box) {  
    $('#kt-modal-edit').modal('show');
    document.getElementById("ID-main").innerHTML = Item_Code;
    $('#inp-itemcode-edit').val((Item_Code == "null") ? "" : Item_Code);
    $('#lbl-itemtext-edit').text((Item_Text == "null") ? "" : Item_Text);
    $('#inp-itemtext-edit').val((Item_Text == "null") ? "" : Item_Text);
    $('#lbl-description-edit').text((Description == "null") ? "" : Description);
    $('#inp-description-edit').val((Description == "null") ? "" : Description);
    $('#lbl-qty-edit').text((Qty_per_box == "null") ? "" : Qty_per_box);
    $('#inp-qty-edit').val((Qty_per_box == "null") ? "" : Qty_per_box);
}
//SHOW MODAL INSERT
var showModalInsert = function () {
    $('#kt-modal-insert input[type="text"]').prop('checked', false);
    $('#kt-modal-insert').modal('show');
    $('#inp-itemcode-insert').val('');
    $('#inp-itemtext-insert').val('');
    $('#inp-description-insert').val('');
    $('#inp-qty-insert').val('');
}
// close model
var closeEditModal = function () {
    $('#kt-modal-edit').modal('hide');
}

//var closeInsertModal = function () {
//    $('#kt-modal-insert').modal('hide');
//}

////Thêm 
//var insertNewPartInfo = function () {
//    const insertBtn = document.getElementById('btn-insert');
//    insertBtn.addEventListener('click', function () {


//        let Item_Code = $('#inp-itemcode-insert').val();
//        let Item_Text = $('#inp-itemtext-insert').val();
//        let Description = $('#inp-description-insert').val();
//        let Qty_Per_Box = $('#inp-qty-insert').val();
       
     
//        if (Item_Code == "") {
//            SweetAlert("error", "Hãy nhập mã hàng")
//        }
//        else if (Item_Text == "") {
//            SweetAlert("error", "Hãy nhập mã văn bản")
//        }
//        else {
//            $.ajax({
//                type: "POST",
//                data: {
//                    item_code: Item_Code, item_text: Item_Text,
//                    description: Description, qty_per_box: Qty_Per_Box,

//                },
//                url: "../Item/InsertItem",
//                success: function (response) {
//                    //SweetAlert(response.type, response.message);

//                    $('#kt-modal-edit').modal('hide');
//                    SweetAlert(response.type, response.message)
//                    $('#tbItem').DataTable().ajax.reload();
//                },
//                failure: function (response) {
//                    debugger
//                    console.log(response);
//                },
//                error: function (response) {
//                    debugger
//                    console.log(response);
//                }
//            });

//            closeInsertModal();
//        }
//    })
//}

//Update
var Update = function () {
    const UpdateBtn = document.getElementById('btn-update');
    UpdateBtn.addEventListener('click', function () {
        let Item_Code = $('#inp-itemcode-edit').val();
        let Item_Text = $('#inp-itemtext-edit').val();
        let Description = $('#inp-description-edit').val();
        let Qty_Per_Box = $('#inp-qty-edit').val();

        if (Item_Text == "") {
            SweetAlert("error", "Kiểm tra lại mã hàng")
        }
        else if (Item_Text == "") {
            SweetAlert("error", "Kiểm tra lại mã hàng")
        }
        else {
          $.ajax({
                type: "PUT",
                data: {
                    item_code: Item_Code, item_text: Item_Text,
                    description: Description, qty_per_box: Qty_Per_Box,
                  
                },
                url: "../Item/UpdateItem",
                success: function (response) {

                    //SweetAlert(response.type, response.message);
                    $('#kt-modal-edit').modal('hide');
                    SweetAlert(response.type, response.message)
                    $('#tbItem').DataTable().ajax.reload();
                },
                failure: function (response) {

                    console.log(response);
                },
                error: function (response) {

                    console.log(response);
                }
              });
            closeEditModal();
              }
    })
}
// Delete
var removeItem = function (Item_Code) {
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
                    item_code: Item_Code

                },

                url: "../Item/DeleteItem",
                success: function (response) {
                    ToastrAlertTopRight(response.type, response.message);
                    if (response.type == "success") {
                        $('#tbItem').DataTable().ajax.reload();
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

////Refresh
//$("#btn_refresh").on("click", function () {
//    //table.ajax.reload(null, false);
//    location.reload(true);
//});

//search
//$('input#inp-itemcode').on('keyup click', function () { $('#tbItem').DataTable().column(0).search($('#inp-itemcode').val()).draw(); });
//$('input#inp-itemtext').on('keyup click', function () { $('#tbItem').DataTable().column(1).search($('#inp-itemtext').val()).draw(); });
//$('input#inp-qty').on('keyup click', function () { $('#tbItem').DataTable().column(6).search($('#inp-qty').val()).draw(); });

$(document).ready(function () {
    var oTable = $('#tbItem').DataTable();
    $('#search-btn').click(function () {
        oTable.columns(0).search($("#inp-itemcode").val()).draw();
        oTable.columns(1).search($("#inp-itemtext").val()).draw();
        oTable.columns(4).search($("#inp-qty").val()).draw();
    });

    //ITEMCODE
    $("#inp-itemcode").keypress(function (e) {
        // You can use $(this) here, since this once again refers to your text input            
        if (e.which === 1) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });

    // LOCATION
    $("#inp-itemtext").keypress(function (f) {
        // You can use $(this) here, since this once again refers to your text input            
        if (f.which === 2) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });

    // QTY
    $("#inp-qty").keypress(function (e) {
        // You can use $(this) here, since this once again refers to your text input            
        if (e.which === 3) {
            e.preventDefault(); // Prevent form submit
            oTable.search($(this).val()).draw();
        }
    });
});