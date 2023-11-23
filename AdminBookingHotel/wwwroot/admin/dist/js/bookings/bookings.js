$(document).ready(function () {
    $(".selectpicker").selectpicker("render");
    var f1 = flatpickr(document.getElementById('fromDate'), {
        defaultDate: "today",
        dateFormat: "m/d/Y",

    });

    var f2 = flatpickr(document.getElementById('toDate'), {
        defaultDate: new Date().fp_incr(1),
        dateFormat: "m/d/Y",
    });
    "use strict";
    $("#bookingsTable").DataTable({
        "processing": true,
        "serverSide": true,
        "select": false,
        "filter": true,
        "ordering": true,
        "searching": true,
        "pageLength": 20,
        "lengthMenu": [20, 50, 100, 500],
        "dom": '<"row"<"col-md-12"<"row"<"col-md-6"B><"col-md-6"f> > ><"col-md-12"lrt> <"col-md-12"<"row"<"col-md-5"i><"col-md-7"p>>> >',
        "buttons": [
            { extend: 'excelHtml5', className: 'btn' },
            { extend: 'selectAll', className: 'btn' },
            { extend: 'selectNone', className: 'btn' },
        ],
        "language": {
            "emptyTable": "Không có dữ liệu nào",
            "search": "Tìm Kiếm:",
            "lengthMenu": "Hiển thị  _MENU_  dòng",
            "info": "Hiển thị _START_ đến _END_ trên tổng số _TOTAL_ kết quả",
            "zeroRecords": "Không tìm thấy kết quả phù hợp",
            "loadingRecords": "Đợi Chút Nha....",
            "processing": "Đợi Xíu Nha....",
            "infoEmpty": "Không có dữ liệu nào",
            "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>' },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            "sSearchPlaceholder": "Search...",
            "sLengthMenu": "Results :  _MENU_"
        },
        "ajax": {
            "url": "/WebBookings/LoadData",
            "dataSrc": "dataResult",
            "type": "post",
            "datatype": "json",
            "data": function (data) {

            }
        },
        "columnDefs": [
            {
                'targets': [0],
                'className': "select-checkbox",
            },
            {
                'targets': [1],
                'render': function (e) {
                    if (e == "2001-01-01T00:00:00" || e == "0001-01-01T00:00:00") return "";
                    else return moment(e).format("MM/DD/YYYY");
                },
            },
            {
                'targets': [5],
                'render': function (e) {
                    if (e == "2001-01-01T00:00:00" || e == "0001-01-01T00:00:00") return "";
                    else return moment(e).format("MM/DD/YYYY");
                },
            },
            {
                'targets': [6],
                'render': function (e) {
                    if (e == "2001-01-01T00:00:00" || e == "0001-01-01T00:00:00") return "";
                    else return moment(e).format("MM/DD/YYYY");
                },
            },
            {
                'targets': [8],
                'render': function (e) {
                    return e.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                },
            },
            {
                'targets': [9],
                'render': function (e, t, r, n) {
                    return 0 == e ? out = '<span class="badge badge-warning">Phòng Mới</span>'
                        : 1 == e ? out = '<span class="badge badge-success">Phòng Hot</span>'
                            : 2 == e && (out = '<span class="badge badge-danger">Phòng Giảm Giá</span>')
                },

            }
        ],
        select: {
            style: 'multi'
        },
        "columns": [
            {
                data: null,
                defaultContent: "",
                width: "5%"
            },
            { "data": "CreatedDate", "name": "CreatedDate", "autoWidth": true },
            { "data": "FullName", "name": "FullName", "autoWidth": true },
            { "data": "HotelName", "name": "HotelName", "autoWidth": true },
            { "data": "RoomName", "name": "RoomName", "autoWidth": true },
            { "data": "CheckIn", "name": "CheckIn", "autoWidth": true },
            { "data": "CheckOut", "name": "CheckOut", "autoWidth": true },
            { "data": "Quantity", "name": "Quantity", "autoWidth": true },
            { "data": "TotalAmount", "name": "TotalAmount", "autoWidth": true },
            { "data": "Status", "name": "Status", "autowidth": true },
            {
                "render": function (data, type, row, meta) {
                    return (
                        "<div class='btn-group'><button type='button' class='btn btn-primary btn-sm' onclick='return getSystemsByID(\"" + row.Id + "\")'>Edit</button></div>"
                    );
                },
                "width": "20%"
            }
        ]
    });
    var oTable = $('#bookingsTable').DataTable();
    $('#btnQuery').click(function () {
        oTable.draw();
    });
    $(document).on("hide.bs.modal", "#bookingsDetailModal", function () {
        oTable.draw();
    });
});

function getSystemsByID(Id) {
    $.ajax({
        url: "/WebBookings/Detail/",
        contentType: "application/json; charset=utf-8",
        data: { 'Id': Id },
        type: "GET",
        success: function (result) {
            $('#bookingsDetailModal').html(result);
            $('#bookingsDetailModal').modal('show');
        },
        error: function (errormessage) {
            Swal.fire("Error", errormessage.responseText, "error");
        }
    });
};