$(document).ready(function () {
    $('.selectpicker').selectpicker();
    var f1 = flatpickr(document.getElementById('checkIn'), {
        defaultDate: "today",
        dateFormat: "m/d/Y",

    });

    var f2 = flatpickr(document.getElementById('checkOut'), {
        defaultDate: new Date().fp_incr(1),
        dateFormat: "m/d/Y",
    });
});



$("#updateBtn").click(function () {
    if ($("#txt_fullname").val() == '') {
        Swal.fire({
            icon: 'error',
            title: 'Lỗi',
            text: 'Vui Lòng Nhập Họ Tên'
        });
        return false;
    };

    if ($("#txt_email").val() == '') {
        Swal.fire({
            icon: 'error',
            title: 'Lỗi',
            text: 'Vui Lòng Nhập Email'
        });
        return false;
    };

    if ($("#txt_phone").val() == '') {
        Swal.fire({
            icon: 'error',
            title: 'Lỗi',
            text: 'Vui Lòng Nhập Số Điện Thoại'
        });
        return false;
    };
    if ($("#modelId").val() == "") { id = 0 }
    else { id = $("#modelId").val() }

    var checkIn = $("#checkIn").val();
    var checkOut = $("#checkOut").val();
    var txt_fullname = $("#txt_fullname").val();
    var txt_email = $("#txt_email").val();
    var txt_phone = $("#txt_phone").val();
    var quantity = $("#modelquantity").val();
    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);

    model = {
        "customer": {
            "cusFullName": txt_fullname,
            "cusEmail": txt_email,
            "cusPhone": txt_phone
        },
        "orderItems": [
            {
                "bookingId": id,
                "quantity": quantity,
                "checkIn": checkInDate,
                "checkOut": checkOutDate
            }
        ]
    }
    $.ajax({
        url: '/Booking/Update',
        type: 'POST',
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $.blockUI({
                message: '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-loader spin ml-2"><line x1="12" y1="2" x2="12" y2="6"></line><line x1="12" y1="18" x2="12" y2="22"></line><line x1="4.93" y1="4.93" x2="7.76" y2="7.76"></line><line x1="16.24" y1="16.24" x2="19.07" y2="19.07"></line><line x1="2" y1="12" x2="6" y2="12"></line><line x1="18" y1="12" x2="22" y2="12"></line><line x1="4.93" y1="19.07" x2="7.76" y2="16.24"></line><line x1="16.24" y1="7.76" x2="19.07" y2="4.93"></line></svg>',
                fadeIn: 800,
                overlayCSS: {
                    backgroundColor: '#1b2024',
                    opacity: 0.8,
                    zIndex: 1200,
                    cursor: 'wait'
                },
                css: {
                    border: 0,
                    color: '#fff',
                    zIndex: 1201,
                    padding: 0,
                    backgroundColor: 'transparent'
                }
            });
        },
        complete: function () {
            $.unblockUI();
        },
        success: function (response) {
            Swal.fire({
                title: "Thanh toán thành công!",
                confirmButtonText: "OK",
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    window.location.href = "/Booking/Confirmation?id=" + response
                }
            });
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Thanh Toán không thành công'
            });
        }
    })
});