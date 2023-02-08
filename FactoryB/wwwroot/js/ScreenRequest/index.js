$(document).ready(function () {
 
    InitTable();

    GetAllLine();

    ChangeFloor();
});
var selectedVal = null;
var selectedFloor = null;
function InitTable() {
    var table = $("#datatable").DataTable({
        ajax: {
            url: "/ScreenRequest/GetRequest",
            type: "POST",
            //data: {
            //    'line': $('#sel-line').val()
            //},
            //dataFilter: function (response) {
            ////     this to see what exactly is being sent back
            //    console.log(response);
            //    return response
            //},
            data: function (d) {
                d.line = $('#sel-line').val();
                d.floor = $('#sel-floor').val();
            },
            dataSrc: "",
            error: function (error) {
                //// to see what the error is
                //console.log(error);
                //ToastrAlert("error", error);
            }
        },
        //scrollY: "400px",
        paging: false,
        info: false,
        //data: json,
        columns: [
            {
                data: "line_code"
                , render: function (data) {
                    return `<p class="font-weight-bolder text-white mb-0 " style="font-size:30px; padding-left:20px">${data ?? ""}</p>`;
                }
            },
            {
                data: "item_code"
                , render: function (data) {
                    return `<p class="font-weight-bolder text-white mb-0" style="font-size:30px;">${data}</p>`;
                }
            },
            {
                data: { location_code: "location_code", qty: "qty", status_name:"status_name" },
                render: function (data) {
                    if (data.status_name == "B2F") {
                        return `<p class="font-weight-bolder text-white mb-0" style="font-size:30px;"></p>`
                    } else {
                        return `<p class="font-weight-bolder text-white mb-0" style="font-size:30px;">${data.location_code ?? ""}</p>`;
                    }
                 
                }
            },
            {
                data: "request_qty"
                , render: function (data) {
                    return `<p class="font-weight-bolder text-white mb-0  text-right" style="font-size:30px">${data ?? ""}</p>`;
                }
            },
            {
                data: "read_qty"
                , render: function (data) {
                    return `<p class="font-weight-bolder text-white mb-0  text-right" style="font-size:30px">${data ?? ""}</p>`;
                }
            },
            {
                data: { item_text: "item_text", po_no: "po_no", po_item: "po_item", delivery_no: "delivery_no",lot_no:"lot_no", qty: "qty" },
                render: function (data) {
                    return `<p class="mb-0 ">${data.type_item ?? ""}<br/>${data.po_no ?? ""}(${data.qty ?? "0"})</p>`;
                }
            },
            {
                data: "status_name"
                , render: function (data) {
                    return `<p class="text-white mb-0 " style="font-size:30px">${data ?? ""}</p>`;
                }
            }
        ],
        columnDefs: [
            { width: 100, targets: [0] },
            { width: 300, targets: [1, 2] },
            { width: 150, targets: [3,4] },
            { width: 250, targets: [5] },
            { width: 150, targets: [6] },
        ],
        searching: false,
        sort: false,
        language: {
            paginate: {
                previous: "<i class='mdi mdi-chevron-left'>",
                next: "<i class='mdi mdi-chevron-right'>"
            }
        },
        drawCallback: function () {
            $(".dataTables_paginate > .pagination").addClass("pagination-rounded");
        },
        colReorder: true
    });

    setInterval(function () {
        table.ajax.reload();
    },
        5000);

    $(document.body).on("change",
        "#sel-line",
        function () {
            table.ajax.reload();
        });
    $(document.body).on("change",
        "#sel-floor",
        function () {
            table.ajax.reload();
        });

}

function GetAllLine() {
    var line = {};

    $.ajax({
        type: "POST",
        data: line,
        url: "/ScreenRequest/GetAllLine",
        success: function (response) {
            $('#sel-line').empty();
            $('#sel-line').append(new Option('Tất cả', '', false, false));
          
            $.each(response, function (i, item) {
                var newOption = new Option(item.line_code, item.line_code, false, false);
                $('#sel-line').append(newOption);
            });
          
            if (selectedVal) {
                $("#sel-line").val(selectedVal).trigger('change');
            }

            $("#sel-line").on("change", function () {
                selectedVal = $(this).val();
                
            });
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function ChangeFloor()
{
    if (selectedVal) {
        $("#sel-floor").val(selectedFloor).trigger('change');
    }

    $("#sel-floor").on("change", function () {
        selectedFloor = $(this).val();

    });
}

function ToggleFullscreen(elem) {
    elem = elem || document.documentElement;

    if (!document.fullscreenElement && !document.mozFullScreenElement &&
        !document.webkitFullscreenElement && !document.msFullscreenElement) {
        if (elem.requestFullscreen) {
            elem.requestFullscreen();
        } else if (elem.msRequestFullscreen) {
            elem.msRequestFullscreen();
        } else if (elem.mozRequestFullScreen) {
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullscreen) {
            elem.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
        }
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
    }
}
