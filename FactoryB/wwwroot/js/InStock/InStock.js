var start = moment("2011-08-04T00:00:00");
var end = moment();

//Class definition
var ViewInstock = function () {
    // Shared variables
    var dt;
    let intialTable = true;
    // Private functions
    var initDatatable = function () {
        dt = $("#tbInstock").DataTable({
            searchDelay: 500,
            processing: true,
            ajax: null,
            columns: [
                { data: 'id_instock' },
                { data: 'item_code' },
                { data: 'location_code' },
                { data: 'po_no' },
                { data: 'po_item' },
                { data: 'lot_no' },
                { data: 'delivery_no' },
                { data: 'qty' },
                { data: 'instock_by' },
                { data: 'instock_at' },

            ],
            columnDefs: [
               
                { "defaultContent": "-", "targets": "_all" },
                {
                    targets: [0, 1, 3, 4, 5, 6],
                    width: 20
                },
                
                {
                    targets: [-1],
                    render: function (data) {
                        if (data == null)
                            return "";
                        return moment(data).format('YYYY-MM-DD HH:mm:ss');
                    }
                },
            ],
            scrollCollapse: true,
            fixedColumns: {
                left: 2,
                right: 1
            },
            order: [[0, 'asc']],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            buttons: ["copy", "excel", "pdf", "colvis"]
        });

        table = dt.$;
    }
        // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()

    var handleSearchDatatable = function () {
        const filterSearch = document.querySelector('[data-kt-out-history-table-detail-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            dt.search(e.target.value).draw();
        });
    }
    var handleGetData = function () {
        var oTable = $('#tbInstock').DataTable();
        let getDataBtn = $("#search-btn");
        getDataBtn.click(function (e) {

            getDataBtn.attr("data-kt-indicator", "on");
            getDataBtn.prop("disabled", true);

            if (intialTable) {
                dt.ajax.url({
                    url: "../InStock/GetInStock",
                    type: "POST",
                    data: function (d) {
                        d.date_range = $("#kt_daterangepicker").val();
                        d.item_code = $('#inp-itemcode').val();
                        d.location_code = $('#inp-locationcode').val();                       
                        d.qty = $('#inp-qty').val();
                     
                       
                    },
                    columnDefs: [

                    ],
                    dataSrc: "",
                    dataFilter: function (response) {
                        // this to see what exactly is being sent back
                        console.log(response);
                        return response
                    },
                    error: function (error) {
                        // to see what the error is
                        //console.log(error);
                        ToastrAlertTopRight("error", error);

                        getDataBtn.attr("data-kt-indicator", "off");
                        getDataBtn.prop("disabled", false);
                    }
                }).load(function () {
                    getDataBtn.attr("data-kt-indicator", "off");
                    getDataBtn.prop("disabled", false);
                });

                intialTable = false;
            } else {
                dt.ajax.reload(function () {
                    getDataBtn.attr("data-kt-indicator", "off");
                    getDataBtn.prop("disabled", false);
                });
            }
            dt.buttons().container().appendTo("#dt_tools_detail");
            oTable.columns(1).search($("#inp-itemcode").val()).draw();
            oTable.columns(2).search($("#inp-locationcode").val()).draw();
            oTable.columns(7).search($("#inp-qty").val()).draw();

            //ITEMCODE
            $("#inp-itemcode").keypress(function (e) {
                // You can use $(this) here, since this once again refers to your text input            
                if (e.which === 1) {
                    e.preventDefault(); // Prevent form submit
                    oTable.search($(this).val()).draw();
                }
            });

            // LOCATION
            $("#inp-locationcode").keypress(function (f) {
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
    }
    // Public methods
    return {
        init: function () {
            initDatatable();
            handleGetData();
            handleSearchDatatable();

            function cb(start, end) {
                $("#kt_daterangepicker").html(start.format("MMMM D, YYYY") + " - " + end.format("MMMM D, YYYY"));
            }

            $("#kt_daterangepicker").daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    "Tất cả": [moment("2011-08-04T00:00:00"), moment()],
                    "Hôm nay": [moment(), moment()],
                    "Hôm qua": [moment().subtract(1, "days"), moment().subtract(1, "days")],
                    "Trong 7 ngày": [moment().subtract(6, "days"), moment()],
                    "Trong 30 ngày": [moment().subtract(29, "days"), moment()],
                    "Tháng này": [moment().startOf("month"), moment().endOf("month")],
                    "Tháng trước": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")]
                },
                locale: {
                    format: "YYYY-MM-DD"
                }
            }, cb);

            cb(start, end);
        }
    }
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    ViewInstock.init();
});
