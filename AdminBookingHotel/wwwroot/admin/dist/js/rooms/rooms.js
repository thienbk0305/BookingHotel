﻿$(document).ready(function () {
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
    $("#roomsTable").DataTable({
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
            "url": "/WebRooms/LoadData",
            "dataSrc": "dataResult",
            "type": "post",
            "datatype": "json",
            "data": function (data) {

            }
        },
        select: {
            style: 'multi'
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
                    else return moment(e).format("MM/DD/YYYY HH:mm");
                },
            },
            {
                'targets': [4],
                'render': function (data) {
                    out = '';
                    badgeColorArray = ['success', 'danger', 'primary', 'secondary'];
                    data = data.replace('[', '').replace(']', '').replaceAll('"', '')
                    if (!data.includes(',')) {
                        out = '<span class="badge outline-badge-info">' + data + '</span>';
                    } else {
                        tempArr = data.split(',')
                        tempArr.forEach((element) => {
                            rnd = Math.floor(Math.random() * badgeColorArray.length);
                            badgeColor = 'outline-badge-' + badgeColorArray[rnd];
                            out += '<span class="badge ' + badgeColor + '">' + element + ' </span>'
                        })
                    }
                    return out;
                }
            },
            {
                'targets': [5],
                'render': function (data) {
                    out = '';
                    badgeColorArray = ['info', 'warning'];
                    // badgeColorArray = ['info','warning','success','danger','primary','secondary'];
                    data = data.replace('[', '').replace(']', '').replaceAll('"', '')
                    if (!data.includes(',')) {
                        out = '<span class="badge outline-badge-info">' + data + '</span>';
                    } else {
                        tempArr = data.split(',')
                        tempArr.forEach((element) => {
                            rnd = Math.floor(Math.random() * badgeColorArray.length);
                            badgeColor = 'outline-badge-' + badgeColorArray[rnd];
                            out += '<span class="badge ' + badgeColor + '">' + element + ' </span>'
                        })
                    }
                    return out;
                }
            },
            {
                'targets': [6],
                'render': function (e, t, r, n) {
                    var out;
                    if (e === true) {
                        out = '<span class="badge badge-success"> Active </span>'
                    } else out = '<span class="badge badge-warning"> InActive </span>';
                    return out;
                }
            },
            {
                'targets': [7],
                'render': function (e, t, r, n) {
                    var out = ''
                    if (e !== null) {
                        out = '<img src="https://localhost:7204/Avatar/' + e + '" width="100px" height="100px"</>'
                    }
                    return out;
                }
            }
        ],
        "columns": [
            {
                data: null,
                defaultContent: "",
                width: "5%"
            },
            { "data": "SysDate", "name": "SysDate", "autoWidth": true },
            { "data": "RoomName", "name": "RoomName", "autoWidth": true },
            { "data": "RoomSize", "name": "RoomSize", "autoWidth": true },
            { "data": "RoomType", "name": "RoomType", "autoWidth": true },
            { "data": "RoomHuman", "name": "RoomHuman", "autoWidth": true },
            { "data": "Active", "name": "Active", "autoWidth": true },
            { "data": "ImgCode", "name": "ImgCode", "autoWidth": true },
            {
                "render": function (data, type, row, meta) {
                    return (
                        "<div class='btn-group'><button type='button' class='btn btn-primary btn-sm' onclick='return getRoomsByID(\"" + row.Id + "\")'>Edit</button></div>" +
                        "<div class='btn-group'><button type='button' class='btn btn-primary btn-sm' onclick='return deleteRoomsByID(\"" + row.Id + "\")'>Delete</button></div>"
                    );
                },
                "width": "20%"
            }
        ]
    });
    var oTable = $('#roomsTable').DataTable();
    $('#btnQuery').click(function () {
        oTable.draw();
    });
    $(document).on("hide.bs.modal", "#RoomsDetailModal", function () {
        oTable.draw();
    });
    $("#btnAddNew").click(function () {
        var e = $("#RoomsDetailModal").data("url");
        $.get(e, function (e) {
            $("#RoomsDetailModal").html(e), $("#RoomsDetailModal").modal("show")
        })
    });
});

function getRoomsByID(Id) {
    $.ajax({
        url: "/WebRooms/Detail/",
        contentType: "application/json; charset=utf-8",
        data: { 'Id': Id },
        type: "GET",
        success: function (result) {
            $('#RoomsDetailModal').html(result);
            $('#RoomsDetailModal').modal('show');
        },
        error: function (errormessage) {
            Swal.fire("Error", errormessage.responseText, "error");
        }
    });
};

function deleteRoomsByID(id) {
    return $.ajax({
        url: "/WebRooms/Delete/" + id,
        type: "DELETE",
        success: function (e) {
        },
        error: function (e) {
            swal("Error", e.responseText, "error")
        }
    }), !1
}